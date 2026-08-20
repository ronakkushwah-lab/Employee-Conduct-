import json
import calendar
from django.contrib.auth import authenticate, login
from django.contrib.auth.hashers import check_password, make_password
from django.contrib.auth.mixins import LoginRequiredMixin
from django.http.response import HttpResponseRedirect
from django.contrib.auth import update_session_auth_hash
from django.shortcuts import redirect, render, get_object_or_404
from django.views import generic
from django.views.generic import View
from django.contrib.auth.hashers import  make_password
from administration.models import Task, notification, holiday, MTask
from leave.forms import LeaveCreationForm
from leave.models import Leave, BalanceLeaves
from manager_leave.models import BalanceLeave
from managers.models import Manager, EmployeeNotification
from payroll.models import Salary
from regularization.forms import RegularizationCreationForm
from regularization.models import Regularization
from resign.forms import ResignCreationForm
from resign.models import Resign
from account.models import CompanyStaff
from .helpers.enum import attendance_type
from .helpers.helper import getgriddatapaginated, strfdelta, ajax_response, show_message_once
from .models import Employee, Attendance, Entries
from django.db import IntegrityError
from django.contrib import messages
from django.views.generic import TemplateView, CreateView, ListView
from .models import Employee, Department, Designation
from .forms import DepartmentForm, DesignationForm, EntryCreationForm
from django.urls import reverse_lazy, reverse
from datetime import datetime, timedelta
from django.utils import timezone
from django.http import JsonResponse
from django.shortcuts import render, get_object_or_404
from django.http import HttpResponse
from django.contrib.auth.mixins import LoginRequiredMixin, UserPassesTestMixin
from django.contrib.auth.models import User
from django.views.generic import (
    ListView,
    DetailView,
    CreateView,
    UpdateView,
    DeleteView
)
from .models import Post
from django.contrib.staticfiles.views import serve
from django.db.models import Q
from django.contrib.auth import get_user_model


User = get_user_model()


def _attendance_month_context(attendance_queryset, request):
    today = timezone.localdate()
    try:
        selected_year = int(request.GET.get('year') or today.year)
        selected_month = int(request.GET.get('month') or today.month)
        if selected_month < 1 or selected_month > 12:
            raise ValueError
    except (TypeError, ValueError):
        selected_year = today.year
        selected_month = today.month

    month_start_date = datetime(selected_year, selected_month, 1).date()
    _, days_in_month = calendar.monthrange(selected_year, selected_month)
    month_end_date = datetime(selected_year, selected_month, days_in_month).date()
    month_start = timezone.make_aware(datetime.combine(month_start_date, datetime.min.time()), timezone.get_current_timezone())
    month_end = timezone.make_aware(datetime.combine(month_end_date + timedelta(days=1), datetime.min.time()), timezone.get_current_timezone())

    month_records = list(
        attendance_queryset.filter(check_in__gte=month_start, check_in__lt=month_end).order_by('check_in')
    )

    records_by_day = {}
    table_rows = []
    present_days = set()
    for record in month_records:
        local_check_in = timezone.localtime(record.check_in) if timezone.is_aware(record.check_in) else record.check_in
        local_check_out = timezone.localtime(record.check_out) if record.check_out and timezone.is_aware(record.check_out) else record.check_out
        date_key = local_check_in.date()
        present_days.add(date_key)
        records_by_day.setdefault(date_key, record)
        working_hours = ''
        if record.check_in and record.check_out:
            working_hours = strfdelta(record.check_out - record.check_in, "{hours}:{minutes}")
        table_rows.append({
            'date': local_check_in.strftime('%d %b %Y'),
            'day': local_check_in.strftime('%A'),
            'status': 'Present',
            'status_class': 'present',
            'check_in': local_check_in.strftime('%I:%M %p'),
            'check_out': local_check_out.strftime('%I:%M %p') if local_check_out else '-',
            'working_hours': working_hours or '-',
            'source': getattr(record, 'source', '') or 'manual',
        })

    calendar_days = []
    first_weekday = month_start_date.weekday()
    sunday_offset = (first_weekday + 1) % 7
    for _ in range(sunday_offset):
        calendar_days.append(None)

    for day_num in range(1, days_in_month + 1):
        current_date = datetime(selected_year, selected_month, day_num).date()
        if current_date in present_days:
            status = 'present'
            label = 'Present'
        else:
            status = 'not-marked'
            label = 'Not Marked'
        calendar_days.append({
            'day': day_num,
            'date': current_date,
            'status': status,
            'label': label,
            'is_today': current_date == today,
            'record': records_by_day.get(current_date),
        })

    while len(calendar_days) % 7 != 0:
        calendar_days.append(None)

    calendar_weeks = [
        calendar_days[index:index + 7]
        for index in range(0, len(calendar_days), 7)
    ]
    previous_month = selected_month - 1
    previous_year = selected_year
    if previous_month == 0:
        previous_month = 12
        previous_year -= 1
    next_month = selected_month + 1
    next_year = selected_year
    if next_month == 13:
        next_month = 1
        next_year += 1

    marked_days = len(present_days)
    return {
        'attendance_month_name': calendar.month_name[selected_month],
        'attendance_month': selected_month,
        'attendance_year': selected_year,
        'attendance_calendar_weeks': calendar_weeks,
        'attendance_table_rows': table_rows,
        'attendance_summary': {
            'present': marked_days,
            'absent': 0,
            'late': 0,
            'not_marked': days_in_month - marked_days,
            'total_days': days_in_month,
        },
        'attendance_previous': {'month': previous_month, 'year': previous_year},
        'attendance_next': {'month': next_month, 'year': next_year},
        'attendance_weekdays': ['SUN', 'MON', 'TUE', 'WED', 'THU', 'FRI', 'SAT'],
    }


def employee_profile_view(request,company_id, company_staff_id):
    try:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
    except CompanyStaff.DoesNotExist:
        messages.error(request, 'Company staff not found.')
        return redirect('accounts:login')
    
    profile = Employee.objects.filter(user=company_staff).first()
    if not profile:
        # Only show this message once per session to avoid duplicates
        if not request.session.get('employee_profile_warning_shown', False):
            messages.error(request, 'Employee profile not found. Please contact administrator to complete your profile setup.')
            request.session['employee_profile_warning_shown'] = True
        return render(request, 'employee/my-profile.html', {
            'profile': None,
            'company_id': company_id, 
            'company_staff_id': company_staff_id
        })
    
    if company_id:
        if request.method == "POST":
            try:
                data = dict(request.POST.copy())
                for field, value in data.items():
                    if field != 'csrfmiddlewaretoken' and field != 'employee_image' and field != 'cropped-image-input':
                        if hasattr(profile, field):
                            # Format phone numbers to +91 XXXXX XXXXX format
                            if field in ['employee_phone', 'employee_emergency_primary_phone1']:
                                phone_value = value[0].strip()
                                # Remove all non-digit characters except +
                                digits = ''.join(filter(str.isdigit, phone_value.replace('+91', '')))
                                # Ensure exactly 10 digits
                                if len(digits) == 10:
                                    formatted_phone = f"+91 {digits[:5]} {digits[5:]}"
                                    setattr(profile, field, formatted_phone)
                                else:
                                    # If not 10 digits, keep original but will be validated by frontend
                                    setattr(profile, field, phone_value)
                            else:
                                setattr(profile, field, value[0])

                # Handle cropped image (base64) or regular file upload
                cropped_image_data = request.POST.get('cropped-image-input', '')
                if cropped_image_data and cropped_image_data.startswith('data:image'):
                    import base64
                    from django.core.files.base import ContentFile
                    from django.utils.text import slugify
                    import uuid
                    
                    try:
                        # Remove data URL prefix
                        format, imgstr = cropped_image_data.split(';base64,')
                        ext = format.split('/')[-1]
                        
                        # Decode base64 image
                        image_file = ContentFile(base64.b64decode(imgstr), name=f"{slugify(profile.employee_first_name)}_{uuid.uuid4().hex[:8]}.{ext}")
                        profile.employee_image = image_file
                    except Exception as e:
                        messages.error(request, f'Error processing cropped image: {str(e)}')
                elif 'employee_image' in request.FILES:
                    profile.employee_image = request.FILES['employee_image']

                profile.save()
                messages.success(request, 'Profile updated successfully!')
                # Redirect after POST to prevent form resubmission on refresh
                return redirect('employee_profile', company_id=company_id, company_staff_id=company_staff_id)
            except Exception as e:
                messages.error(request, f'Error updating profile: {str(e)}')
                # Redirect even on error to prevent form resubmission
                return redirect('employee_profile', company_id=company_id, company_staff_id=company_staff_id)
        
        return render(request, 'employee/my-profile.html', {
            'profile': profile,
            'company_id': company_id, 
            'company_staff_id': company_staff_id
        })
    
    return render(request, 'employee/my-profile.html', {
        'profile': profile,
        'company_id': company_id, 
        'company_staff_id': company_staff_id
    })


def upload_profile_image(request, company_id, company_staff_id):
    """Quick profile image upload endpoint for topbar"""
    if request.method != 'POST':
        return JsonResponse({'error': 'Method not allowed'}, status=405)
    
    try:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
    except CompanyStaff.DoesNotExist:
        return JsonResponse({'error': 'Company staff not found'}, status=404)
    
    profile = Employee.objects.filter(user=company_staff).first()
    if not profile:
        return JsonResponse({'error': 'Employee profile not found'}, status=404)
    
    if 'employee_image' not in request.FILES:
        return JsonResponse({'error': 'No image file provided'}, status=400)
    
    try:
        profile.employee_image = request.FILES['employee_image']
        profile.save()
        return JsonResponse({
            'success': True,
            'message': 'Profile updated successfully.',
            'image_url': profile.employee_image.url if profile.employee_image else None
        })
    except Exception as e:
        return JsonResponse({'error': f'Error uploading image: {str(e)}'}, status=500)


def remove_profile_image(request, company_id, company_staff_id):
    """Employee profile photo hataane ke liye – Remove Profile button."""
    if request.method != 'POST':
        messages.error(request, 'Invalid request.')
        return redirect('employee_profile', company_id=company_id, company_staff_id=company_staff_id)
    try:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
    except CompanyStaff.DoesNotExist:
        messages.error(request, 'Company staff not found.')
        return redirect('accounts:login')
    profile = Employee.objects.filter(user=company_staff).first()
    if not profile:
        messages.error(request, 'Employee profile not found.')
        return redirect('employee_profile', company_id=company_id, company_staff_id=company_staff_id)
    try:
        if profile.employee_image:
            try:
                profile.employee_image.delete(save=False)
            except Exception:
                pass
        profile.employee_image = ''
        profile.save(update_fields=['employee_image'])
        messages.success(request, 'Profile photo removed successfully.')
    except Exception as e:
        try:
            profile.employee_image = ''
            profile.save()
            messages.success(request, 'Profile photo removed successfully.')
        except Exception as e2:
            messages.error(request, f'Could not remove profile photo: {str(e2)}')
    return redirect('employee_profile', company_id=company_id, company_staff_id=company_staff_id)


class EmployeeUpdateView(UpdateView):
    model = Employee
    template_name = 'employee/my-profile.html'
    fields = '__all__'
    context_object_name = 'employee_update'


def EmployeeDashboardView(request, company_id, company_staff_id):
    ctx = {}
    today = timezone.now().date()
    tomorrow = today + timedelta(1)

    try:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
    except CompanyStaff.DoesNotExist:
        messages.error(request, 'Company staff not found.')
        return redirect('accounts:login')

    employee = Employee.objects.filter(user=company_staff).first()

    if not employee:
        if not request.session.get('employee_profile_warning_shown', False):
            messages.error(request, 'Employee profile not found. Please contact administrator to complete your profile setup.')
            request.session['employee_profile_warning_shown'] = True
        ctx['attendance'] = None
        ctx['company_id'] = company_id
        ctx['company_staff_id'] = company_staff_id
        ctx['is_check_in'] = attendance_type.check_in.value
        ctx['hours_num'] = '0:0:0'
        ctx['my_leaves'] = []
        ctx['my_tasks'] = []
        ctx['my_notifications'] = []
        return render(request, "employee/index.html", ctx)

    att = Attendance.objects.filter(
        Q(check_in__gte=today) & Q(check_in__lt=tomorrow) & Q(employee=employee)
    ).first()
    ctx['attendance'] = att
    ctx['company_id'] = company_id
    ctx['company_staff_id'] = company_staff_id
    ctx['employee'] = employee
    if att:
        try:
            if att.check_out and att.check_in:
                time_diff = att.check_out - att.check_in
                ctx['hours_num'] = strfdelta(time_diff, "{hours}:{minutes}:{seconds}")
            else:
                ctx['hours_num'] = '0:0:0'
        except Exception:
            ctx['hours_num'] = '0:0:0'
        ctx['is_check_in'] = attendance_type.check_out.value
        ctx['is_complete_attendance'] = bool(att.check_in and att.check_out)
    else:
        ctx['hours_num'] = '0:0:0'
        ctx['is_check_in'] = attendance_type.check_in.value
        ctx['is_complete_attendance'] = False

    # My Leave Requests (show only latest 2 on dashboard)
    my_leaves = Leave.objects.filter(user=employee).order_by('-created')[:2]
    ctx['my_leaves'] = my_leaves

    # My Resignation Requests (show only latest 2 on dashboard)
    my_resignations = Resign.objects.filter(user=employee).order_by('-created')[:2]
    ctx['my_resignations'] = my_resignations

    # My Notifications
    my_notifications = EmployeeNotification.objects.filter(user=employee).order_by('-id')[:8]
    ctx['my_notifications'] = my_notifications

    return render(request, "employee/index.html", ctx)


def leave_creation(request,company_id, company_staff_id):
    if company_id:
        if request.method == 'POST':
            form = LeaveCreationForm(data=request.POST)
            if form.is_valid():
                try:
                    instance = form.save(commit=False)
                    company_staff = CompanyStaff.objects.get(id=company_staff_id)
                    employee = Employee.objects.filter(user=company_staff).first()
                    if employee:
                        instance.user = employee
                    else:
                        instance.user = None
                    instance.save()
                    
                    # Send email notification
                    if instance.user:
                        try:
                            from administration.email_notifications import send_leave_submission_notification
                            send_leave_submission_notification(instance)
                        except Exception as e:
                            print(f"Error sending leave submission notification: {str(e)}")
                    
                    messages.success(request, 'Leave Request Sent,wait for response',
                                     extra_tags='alert alert-success alert-dismissible show')
                    return redirect(f'/employee/leaves/view/table/{company_id}/{company_staff_id}')
                except CompanyStaff.DoesNotExist:
                    messages.error(request, 'Company staff not found.')
                    return redirect(f'/employee/leave/{company_id}/{company_staff_id}')
                except Exception as e:
                    messages.error(request, f'Error creating leave request: {str(e)}')
                    return redirect(f'/employee/leave/{company_id}/{company_staff_id}')
            messages.error(request, 'failed to Request a Leave,please check entry dates',
                           extra_tags='alert alert-warning alert-dismissible show')
        return redirect(f'/employee/leave/{company_id}/{company_staff_id}')

    dataset = dict()
    form = LeaveCreationForm()
    dataset['form'] = form
    dataset['title'] = 'Apply for Leave'
    dataset['company_id'] = company_id
    dataset['company_staff_id'] = company_staff_id
    dataset['managers'] = Manager.objects.filter(user__company__id=company_id)
    return render(request, 'employee/apply-leaves.html', dataset)


def view_my_leave_table(request,company_id, company_staff_id):
    if company_id:
        try:
            company_staff = CompanyStaff.objects.get(id=company_staff_id)
        except CompanyStaff.DoesNotExist:
            messages.error(request, 'Company staff not found.')
            return redirect('accounts:login')
        
        employee = Employee.objects.filter(user=company_staff).first()
        if not employee:
            messages.error(request, 'Employee profile not found.')
            dataset = dict()
            dataset['leave_list'] = Leave.objects.none()
            dataset['title'] = 'Leaves List'
            dataset['company_id'] = company_id
            dataset['company_staff_id'] = company_staff_id
            return render(request, 'employee/leave-status.html', dataset)
        
        leaves = Leave.objects.filter(user=employee)
        dataset = dict()
        dataset['leave_list'] = leaves
        dataset['title'] = 'Leaves List'
        dataset['company_id'] = company_id
        dataset['company_staff_id'] = company_staff_id
    else:
        return redirect('accounts:login')
    return render(request, 'employee/leave-status.html', dataset)


class LeaveRemove(View):
    def get(self, request, id):
        try:
            leave = Leave.objects.get(id=id)
            leave.delete()
            messages.success(request, 'Leave deleted successfully.')
        except Leave.DoesNotExist:
            messages.error(request, 'Leave not found.')
        except Exception as e:
            messages.error(request, f'Error deleting leave: {str(e)}')
        return HttpResponseRedirect('/employee/employee_dashboard/')


def attendance(request,company_id, company_staff_id):
    ctx = {}
    today = timezone.now().date()
    tomorrow = today + timedelta(1)

    try:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
    except CompanyStaff.DoesNotExist:
        messages.error(request, 'Company staff not found.')
        return redirect('accounts:login')
    
    employee = Employee.objects.filter(user=company_staff).first()
    if not employee:
        messages.error(request, 'Employee profile not found.')
        ctx['attendance'] = None
        ctx['company_id'] = company_id
        ctx['company_staff_id'] = company_staff_id
        ctx['is_check_in'] = attendance_type.check_in.value
        ctx.update(_attendance_month_context(Attendance.objects.none(), request))
        return render(request, 'employee/attendance-info.html', ctx)
    
    att = Attendance.objects.filter(Q(check_in__gt=today)
                                    & Q(check_in__lt=tomorrow)
                                    & Q(employee=employee)).first()
    ctx['attendance'] = att
    ctx['company_id'] = company_id
    ctx['company_staff_id'] = company_staff_id
    if att:
        try:
            if att.check_out and att.check_in:
                time_diff = att.check_out - att.check_in
                ctx['hours_num'] = strfdelta(time_diff, "{hours}:{minutes}:{seconds}")
            else:
                ctx['hours_num'] = ''
        except Exception:
            ctx['hours_num'] = ''
        ctx['is_check_in'] = attendance_type.check_out.value
        ctx['is_complete_attendance'] = True if att.check_in and att.check_out else False
    else:
        ctx['is_check_in'] = attendance_type.check_in.value
    ctx.update(_attendance_month_context(Attendance.objects.filter(employee=employee), request))
    return render(request, 'employee/attendance-info.html', ctx)


def regularization_required_attendance(request,company_id, company_staff_id):
    try:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
    except CompanyStaff.DoesNotExist:
        messages.error(request, 'Company staff not found.')
        return redirect('accounts:login')
    
    employee = Employee.objects.filter(user=company_staff).first()
    if not employee:
        messages.error(request, 'Employee profile not found.')
        return render(request,'employee/regularization.html',context={
            'atts': Attendance.objects.none(), 
            'rassigne': Manager.objects.filter(user__company__id=company_id),
            'company_id':company_id, 
            'company_staff_id':company_staff_id
        })
    
    atts = Attendance.objects.filter(employee=employee)
    if atts:
        for att in atts:
            if att.regularization_required == False:
                atts = atts.exclude(id=att.id)
    return render(request,'employee/regularization.html',context={
        'atts':atts, 
        'rassigne': Manager.objects.filter(user__company__id=company_id),
        'company_id':company_id, 
        'company_staff_id':company_staff_id
    })


def attendance_post(request,company_id, company_staff_id):
    if company_id:
        try:
            is_check_in = request.POST['is_check_in']
            attendance_id = request.POST.get('attendance_id', '')
            
            # Get existing attendance or create new one
            if attendance_id and attendance_id.strip():
                attendance_obj = Attendance.objects.filter(id=attendance_id).first()
                if not attendance_obj:
                    attendance_obj = Attendance()
            else:
                attendance_obj = Attendance()
            
            if is_check_in == attendance_type.check_in.value:
                attendance_obj.check_in = timezone.now()
            else:
                attendance_obj.check_out = timezone.now()
            
            try:
                company_staff = CompanyStaff.objects.get(id=company_staff_id)
            except CompanyStaff.DoesNotExist:
                return JsonResponse({'status': "FAILED", 'error': 'Company staff not found'}, status=404)
            
            employee = Employee.objects.filter(user=company_staff).first()
            if not employee:
                return JsonResponse({'status': "FAILED", 'error': 'Employee profile not found'}, status=404)
            
            attendance_obj.employee = employee
            # Set source field to 'manual' if not already set
            if not attendance_obj.source:
                attendance_obj.source = 'manual'
            
            attendance_obj.save()

            # Return success immediately so "Data Saved!" shows instantly; send email in background
            def _send_notification_later():
                try:
                    from administration.email_notifications import send_attendance_notification
                    action = 'check_in' if is_check_in == attendance_type.check_in.value else 'check_out'
                    send_attendance_notification(attendance_obj, action=action)
                except Exception as e:
                    print(f"Error sending attendance notification: {str(e)}")
            import threading
            threading.Thread(target=_send_notification_later, daemon=True).start()

            return JsonResponse({'status': 'SUCCESS'}, status=200)
        except KeyError as e:
            try:
                error_msg = 'Missing required field: {}'.format(str(e))
            except:
                error_msg = 'Missing required field'
            return JsonResponse({'status': "FAILED", 'error': error_msg}, status=400)
        except Exception as e:
            try:
                error_msg = str(e)
            except:
                error_msg = 'An error occurred'
            import logging
            logger = logging.getLogger(__name__)
            try:
                logger.exception("Error in attendance_post")
            except:
                pass
            return JsonResponse({'status': "FAILED", 'error': error_msg}, status=500)
    else:
        return JsonResponse({'status': "FAILED", 'error': 'Invalid company_id'}, status=400)


def attendance_grid_data(request,company_id, company_staff_id):
    if not company_id:
        return JsonResponse({'error': 'Company ID is required'}, status=400)
    
    try:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
    except CompanyStaff.DoesNotExist:
        return JsonResponse({'error': 'Company staff not found'}, status=404)
    
    employee = Employee.objects.filter(user=company_staff).first()
    if not employee:
        return JsonResponse({'error': 'Employee profile not found'}, status=404)
    
    try:
        grid_columns = ('check_in', 'check_out')
        attendance_list = Attendance.objects.filter(employee=employee).order_by('-check_in')
        
        # Handle DataTables parameters with defaults
        length = int(request.GET.get('length', 10))
        start = int(request.GET.get('start', 0))
        draw = int(request.GET.get('draw', 1))
        
        # Handle optional order parameter
        order_column = request.GET.get('order[0][column]', '0')
        order_dir = request.GET.get('order[0][dir]', 'desc')
        try:
            sort_column = grid_columns[int(order_column)]
        except (ValueError, IndexError):
            sort_column = 'check_in'  # Default to check_in
        
        # Apply sorting
        if order_dir == 'desc':
            sort_column = '-' + sort_column
        attendance_list = attendance_list.order_by(sort_column)
        
        # Get total count
        total_records = attendance_list.count()
        
        # Apply pagination
        end = start + length
        paginated_data = attendance_list[start:end]
        
        # Build context
        ctx = {
            'draw': draw,
            'recordsTotal': total_records,
            'recordsFiltered': total_records,
            'data': paginated_data
        }
        json_data = []
        for item in ctx['data']:
            dict = {}
            # Format time in local timezone (Asia/Kolkata)
            from django.utils import timezone as tz
            try:
                if item.check_in:
                    try:
                        if tz.is_aware(item.check_in):
                            local_check_in = tz.localtime(item.check_in)
                        else:
                            local_check_in = item.check_in
                        dict['check_in'] = local_check_in.strftime("%b %d, %Y, %I:%M %p")
                    except Exception:
                        dict['check_in'] = str(item.check_in)
                else:
                    dict['check_in'] = ''
                
                if item.check_out:
                    try:
                        if tz.is_aware(item.check_out):
                            local_check_out = tz.localtime(item.check_out)
                        else:
                            local_check_out = item.check_out
                        dict['check_out'] = local_check_out.strftime("%b %d, %Y, %I:%M %p")
                    except Exception:
                        dict['check_out'] = str(item.check_out)
                    try:
                        if item.check_in and item.check_out:
                            time_diff = item.check_out - item.check_in
                            if time_diff.total_seconds() >= 0:
                                dict['working_hours'] = strfdelta(time_diff, "{hours}:{minutes}")
                            else:
                                dict['working_hours'] = '0:0'
                        else:
                            dict['working_hours'] = ''
                    except Exception as e:
                        dict['working_hours'] = ''
                else:
                    dict['check_out'] = ''
                    dict['working_hours'] = ''
            except Exception as format_error:
                # Fallback to simple format if formatting fails
                try:
                    dict['check_in'] = str(item.check_in) if item.check_in else ''
                    dict['check_out'] = str(item.check_out) if item.check_out else ''
                    if item.check_out and item.check_in:
                        dict['working_hours'] = strfdelta((item.check_out - item.check_in), "{hours}:{minutes}")
                    else:
                        dict['working_hours'] = ''
                except Exception:
                    dict['check_in'] = ''
                    dict['check_out'] = ''
                    dict['working_hours'] = ''
            json_data.append(dict)
        ctx['data'] = json_data
        return ajax_response(ctx)
    except Exception as e:
        import logging
        logger = logging.getLogger(__name__)
        try:
            error_msg = str(e)
            logger.error("Error in attendance_grid_data: %s", error_msg, exc_info=True)
        except:
            logger.error("Error in attendance_grid_data", exc_info=True)
        return JsonResponse({'error': 'An error occurred while loading attendance data'}, status=500)


def taskList(request,company_id, company_staff_id):
    context ={}
    try:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
    except CompanyStaff.DoesNotExist:
        messages.error(request, 'Company staff not found.')
        return redirect('accounts:login')
    
    employee = Employee.objects.filter(user=company_staff).first()
    if not employee:
        messages.error(request, 'Employee profile not found.')
        context['tasks'] = Task.objects.none()
        context['company_id'] = company_id
        context['company_staff_id'] = company_staff_id
        return render(request, 'employee/my-project.html', context)
    
    queryset = Task.objects.filter(assigned_to__id=employee.id, company_id=company_id)
    context['tasks'] = queryset
    context['company_id'] = company_id
    context['company_staff_id'] = company_staff_id
    return render(request, 'employee/my-project.html', context)


def SalaryListView(request,company_id, company_staff_id):
    context ={}
    try:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
    except CompanyStaff.DoesNotExist:
        messages.error(request, 'Company staff not found.')
        return redirect('accounts:login')
    
    employee = Employee.objects.filter(user=company_staff).first()
    if not employee:
        messages.error(request, 'Employee profile not found.')
        context['salary'] = Salary.objects.none()
        context['company_id'] = company_id
        context['company_staff_id'] = company_staff_id
        return render(request, 'employee/salary.html', context)

    queryset = Salary.objects.filter(employee=employee)
    context['salary'] = queryset
    context['company_id'] = company_id
    context['company_staff_id'] = company_staff_id
    return render(request, 'employee/salary.html', context)


def notifications(request,company_id, company_staff_id):
    if company_id:
        try:
            user = CompanyStaff.objects.get(id=company_staff_id, company_id=company_id)
        except CompanyStaff.DoesNotExist:
            messages.error(request, 'Company staff not found.')
            return redirect('accounts:login')
        
        # Get general company notifications
        company_notifications = notification.objects.filter(company__id=company_id).order_by('-id')
        
        # Get employee-specific notifications
        employee = Employee.objects.filter(user=user).first()
        employee_notifications = []
        unread_count = 0
        
        if employee:
            from managers.models import EmployeeNotification
            employee_notifications = EmployeeNotification.objects.filter(user=employee).order_by('-id')
            unread_count = EmployeeNotification.objects.filter(user=employee, is_read=False).count()
            
            # Update session if no unread notifications
            if unread_count == 0:
                user.new_notification = False
                user.save()
                request.session["new_notification"] = user.new_notification
            else:
                user.new_notification = True
                user.save()
                request.session["new_notification"] = user.new_notification
        
        context = {
            'company_notifications': company_notifications,
            'employee_notifications': employee_notifications,
            'unread_count': unread_count,
            'total_notifications': company_notifications.count() + len(employee_notifications),
            'company_id': company_id,
            'company_staff_id': company_staff_id,
        }
        return render(request, 'employee/notifications.html', context)


def delete_employee_notification(request, company_id, company_staff_id, notification_id):
    """
    Employee can remove their own notification from the list.
    Only allows POST to avoid accidental deletes.
    """
    if request.method != "POST":
        return redirect('notification', company_id=company_id, company_staff_id=company_staff_id)

    try:
        user = CompanyStaff.objects.get(id=company_staff_id, company_id=company_id)
    except CompanyStaff.DoesNotExist:
        messages.error(request, 'Company staff not found.')
        return redirect('accounts:login')

    employee = Employee.objects.filter(user=user).first()
    if not employee:
        messages.error(request, 'Employee profile not found.')
        return redirect('notification', company_id=company_id, company_staff_id=company_staff_id)

    # Only delete notification belonging to this employee
    notif = get_object_or_404(EmployeeNotification, id=notification_id, user=employee)
    notif.delete()

    # Update new_notification flag based on remaining unread notifications
    unread_count = EmployeeNotification.objects.filter(user=employee, is_read=False).count()
    user.new_notification = unread_count > 0
    user.save()
    request.session["new_notification"] = user.new_notification

    messages.success(request, "Notification removed.")
    return redirect('mynotifications', company_id=company_id, company_staff_id=company_staff_id)


def resign_creation(request):
    if request.method == 'POST':
        form = ResignCreationForm(data=request.POST)
        if form.is_valid():
            instance = form.save(commit=False)
            user = request.user
            instance.user = user
            instance.save()

            # print(instance.defaultdays)
            messages.success(request, 'Resignation Request Sent,wait for response',
                             extra_tags='alert alert-success alert-dismissible show')
            return redirect('/employee/resign/view/table/')

        messages.error(request, 'failed to Request a Resignation,please check entry dates',
                       extra_tags='alert alert-warning alert-dismissible show')
        return redirect('/employee/resign/')

    dataset = dict()
    form = ResignCreationForm()
    dataset['form'] = form
    dataset['title'] = 'Apply for Resignation'
    return render(request, 'employee/apply-resignation.html', dataset)


def view_my_resign_table(request,company_id, company_staff_id):
    if company_id:
        try:
            company_staff = CompanyStaff.objects.get(id=company_staff_id)
        except CompanyStaff.DoesNotExist:
            messages.error(request, 'Company staff not found.')
            return redirect('accounts:login')
        
        employee = Employee.objects.filter(user=company_staff).first()
        if not employee:
            messages.error(request, 'Employee profile not found.')
            dataset = dict()
            dataset['resign_list'] = Resign.objects.none()
            dataset['employee'] = None
            dataset['title'] = 'Resign List'
            dataset['company_id'] = company_id
            dataset['company_staff_id'] = company_staff_id
            return render(request, 'employee/resignation-status.html', dataset)
        
        resign = Resign.objects.filter(user=employee)
        dataset = dict()
        dataset['resign_list'] = resign
        dataset['employee'] = employee
        dataset['title'] = 'Resign List'
        dataset['company_id'] = company_id
        dataset['company_staff_id'] = company_staff_id
    else:
        return redirect('accounts:login')
    return render(request, 'employee/resignation-status.html', dataset)


class ResignRemove(View):
    def get(self, request, id):
        try:
            resign = Resign.objects.get(id=id)
            resign.delete()
            messages.success(request, 'Resignation deleted successfully.')
        except Resign.DoesNotExist:
            messages.error(request, 'Resignation not found.')
        except Exception as e:
            messages.error(request, f'Error deleting resignation: {str(e)}')
        return HttpResponseRedirect('/employee/employee_dashboard/')


def holidays(request,company_id, company_staff_id):
    if company_id:
        holyday = holiday.objects.filter(company_id=company_id)
        context = {
            'holyday': holyday,
            'company_id': company_id,
            'company_staff_id': company_staff_id,

        }
        return render(request, 'employee/holiday.html', context)


def getfile(request):
    return serve(request, 'File')


class UserPostListView(LoginRequiredMixin, ListView):
    model = Post
    template_name = 'employee/user_posts.html'  # <app>/<model>_<viewtype>.html
    context_object_name = 'posts'
    paginate_by = 2

    def get_queryset(self):
        queryset = super(UserPostListView, self).get_queryset()
        queryset = Post.objects.filter(user=self.request.user.employee)
        return queryset


class PostDetailView(DetailView):
    model = Post
    template_name = 'employee/post_detail.html'


class PostCreateView(CreateView):
    model = Post
    template_name = 'employee/post_form.html'
    fields = ['experience_letter', 'offer_letter', 'education_certificate', 'skill_certificate', ]

    def form_valid(self, form):
        form.instance.user = self.request.user.employee
        return super().form_valid(form)


class PostUpdateView(UpdateView):
    model = Post
    template_name = 'employee/post_form.html'
    fields = ['file']

    def form_valid(self, form):
        form.instance.user = self.request.user.employee
        return super().form_valid(form)

    def test_func(self):
        post = self.get_object()
        if self.request.user == post.user.employee:
            return True
        return False


class PostDeleteView(DeleteView):
    model = Post
    success_url = '/employee/post/new/'
    template_name = 'employee/post_confirm_delete.html'

    def test_func(self):
        post = self.get_object()
        if self.request.user == post.user.employee:
            return True
        return False


def BalanceLeaveView(request,company_id, company_staff_id):
    context ={}
    try:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
    except CompanyStaff.DoesNotExist:
        messages.error(request, 'Company staff not found.')
        return redirect('accounts:login')
    
    employee = Employee.objects.filter(user=company_staff).first()
    if not employee:
        messages.error(request, 'Employee profile not found.')
        context['balance'] = BalanceLeaves.objects.none()
        context['company_id'] = company_id
        context['company_staff_id'] = company_staff_id
        return render(request, 'employee/leave-balance.html', context)

    queryset = BalanceLeaves.objects.filter(user=employee)
    context['balance'] = queryset
    context['company_id'] = company_id
    context['company_staff_id'] = company_staff_id
    return render(request, 'employee/leave-balance.html', context)


def regularization(request):
    if request.method == 'POST':
        form = RegularizationCreationForm(data=request.POST)
        if form.is_valid():
            instance = form.save(commit=False)
            user = request.user.employee
            instance.user = user
            instance.save()

            # print(instance.defaultdays)
            messages.success(request, 'Apply for regularization Request Sent,wait for response',
                             extra_tags='alert alert-success alert-dismissible show')
            return redirect('/employee/regularization/view/table/')

        messages.error(request, 'failed to Request a Regularizations,please check entry dates',
                       extra_tags='alert alert-warning alert-dismissible show')
        return redirect('/employee/regularization/')

    dataset = dict()
    form = RegularizationCreationForm()
    dataset['form'] = form
    dataset['title'] = 'Apply for Regularization'
    return render(request, 'employee/regularization.html', dataset)


def regularization_table(request,company_id, company_staff_id):
    context ={}
    try:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
    except CompanyStaff.DoesNotExist:
        messages.error(request, 'Company staff not found.')
        return redirect('accounts:login')
    
    employee = Employee.objects.filter(user=company_staff).first()
    if not employee:
        messages.error(request, 'Employee profile not found.')
        context['regularization'] = Regularization.objects.none()
        context['company_id'] = company_id
        context['company_staff_id'] = company_staff_id
        return render(request, 'employee/attendance-status.html', context)

    queryset = Regularization.objects.filter(user=employee)
    context['regularization'] = queryset
    context['company_id'] = company_id
    context['company_staff_id'] = company_staff_id
    return render(request, 'employee/attendance-status.html', context)


def EntriesCreateView(request):
    if request.method == 'POST':
        form = EntryCreationForm(data=request.POST)
        if form.is_valid():
            instance = form.save(commit=False)
            user = request.user.employee
            instance.user = user
            instance.save()
            return redirect('/employee/entries-detail/')

        return redirect('/employee/entries-detail/')

    dataset = dict()
    form = EntryCreationForm()
    dataset['form'] = form
    dataset['title'] = 'Entry'
    return render(request, 'employee/create-timesheet.html', dataset)


def EntryDetailView(request,company_id, company_staff_id):
    if request.method == "POST":
        try:
            data = json.loads(request.body.decode('utf-8'))
            entry_obj_id = data.get('id', None)
            if not entry_obj_id:
                return JsonResponse({'error': 'Entry ID is required'}, status=400)
            entry_obj = Entries.objects.get(pk=entry_obj_id)
            return JsonResponse(entry_obj.to_json())
        except Entries.DoesNotExist:
            return JsonResponse({'error': 'Entry not found'}, status=404)
        except json.JSONDecodeError:
            return JsonResponse({'error': 'Invalid JSON data'}, status=400)
        except Exception as e:
            return JsonResponse({'error': str(e)}, status=500)

    if company_id:
        try:
            company_staff = CompanyStaff.objects.get(id=company_staff_id)
        except CompanyStaff.DoesNotExist:
            messages.error(request, 'Company staff not found.')
            return redirect('accounts:login')
        
        employee = Employee.objects.filter(user=company_staff).first()
        if not employee:
            messages.error(request, 'Employee profile not found. Please contact administrator to complete your profile setup.')
            dataset = dict()
            dataset['project_list'] = []
            dataset['total_time'] = timedelta(seconds=0)
            dataset['title'] = 'Entry List'
            dataset['company_id'] = company_id
            dataset['company_staff_id'] = company_staff_id
            return render(request, 'employee/view-timesheet.html', dataset)
        
        user = employee
        entry = Entries.objects.filter(user=user)
        total_time = sum((obj.total_duration for obj in entry), timedelta())

        from collections import defaultdict
        grouped_entries = defaultdict(list)
        for obj in entry:
            grouped_entries[obj.project].append(obj)

        project_list = []
        for proj in sorted(grouped_entries.keys(), key=lambda x: str(x)):
            entries = grouped_entries[proj]
            proj_total = sum((obj.total_duration for obj in entries), timedelta())
            project_list.append({
                'name': proj,
                'entries': entries,
                'total_time': proj_total
            })

        dataset = dict()
        dataset['project_list'] = project_list
        dataset['total_time'] = total_time
        dataset['title'] = 'Entry List'
        dataset['company_id'] = company_id
        dataset['company_staff_id'] = company_staff_id
    else:
        return redirect('accounts:login')
    return render(request, 'employee/view-timesheet.html', dataset)


class EntryRemove(View):
    def get(self, request,company_id, company_staff_id, id):
        if company_id:
            try:
                entry = Entries.objects.get(id=id)
                entry.delete()
                messages.success(request, 'Entry deleted successfully.')
            except Entries.DoesNotExist:
                messages.error(request, 'Entry not found.')
            except Exception as e:
                messages.error(request, f'Error deleting entry: {str(e)}')
            return redirect(f'/employee/entries-detail/{company_id}/{company_staff_id}')


class documents(generic.CreateView):
    model = Entries
    fields = ('title', 'start_time', 'end_time', 'project', 'task', 'blocker_name', 'attachment', 'assigned_to')
    context_object_name = "entri_list"
    template_name = "employee/create-timesheet.html"
    success_url = ('/employee/entries-detail/')


def create_entry(request, company_id, company_staff_id):
    company_staff = CompanyStaff.objects.get(id=company_staff_id)
    emp = Employee.objects.get(user=company_staff)

    assigned_projects = Task.objects.filter(
        assigned_to=emp,
        company_id=company_id
    )

    context = {
        'assigned': Manager.objects.filter(user__company__id=company_id),
        'assigned_projects': assigned_projects,
        'company_id': company_id,
        'company_staff_id': company_staff_id
    }

    if company_id:
        if request.method == "POST":
            try:
                start_time = request.POST.get("start_time")
                end_time = request.POST.get("end_time")
                project_id = request.POST.get("project")
                task = request.POST.get("task")
                blocker_name = request.POST.get("blocker_name")
                attachment = request.FILES.get("attachment")
                assign_id = request.POST.get("manager_id")

                if not all([start_time, end_time, project_id, task, assign_id]):
                    messages.error(request, 'All fields are required.')
                    return render(request, 'employee/create-timesheet.html', context)

                try:
                    selected_project = Task.objects.get(
                        id=project_id,
                        assigned_to=emp,
                        company_id=company_id
                    )
                except Task.DoesNotExist:
                    messages.error(request, 'Selected project is not assigned to you.')
                    return render(request, 'employee/create-timesheet.html', context)

                try:
                    assigned_to = Manager.objects.get(id=assign_id)
                except Manager.DoesNotExist:
                    messages.error(request, 'Selected manager not found.')
                    return render(request, 'employee/create-timesheet.html', context)

                Entries.objects.create(
                    user=emp,
                    start_time=start_time,
                    end_time=end_time,
                    project=selected_project.title,
                    task=task,
                    blocker_name=blocker_name,
                    attachment=attachment,
                    assigned_to=assigned_to
                )

                messages.success(request, 'Entry created successfully!')
                return redirect(f'/employee/entries-detail/{company_id}/{company_staff_id}')

            except Exception as e:
                messages.error(request, f'Error creating entry: {str(e)}')
                return render(request, 'employee/create-timesheet.html', context)

        else:
            return render(request, 'employee/create-timesheet.html', context)

def create_leave(request,company_id, company_staff_id):
    managers = Manager.objects.filter(user__company__id=company_id)
    ctx = {
        'leavetypes': Leave.objects.all(),
        'company_id': company_id,
        'company_staff_id': company_staff_id,
        'managers': managers,
    }
    if company_id:
        if request.method == "POST":
            try:
                startdate = request.POST.get("startdate")
                enddate = request.POST.get("enddate")
                leavetype = request.POST.get("leavetype")
                reason = request.POST.get("reason")
                description = request.POST.get("description", "")
                manager_id = request.POST.get("manager_id")
                
                if not all([startdate, enddate, leavetype, reason, manager_id]):
                    messages.error(request, 'All required fields must be filled.')
                    return render(request, "employee/apply-leaves.html", ctx)
                
                try:
                    company_staff = CompanyStaff.objects.get(id=company_staff_id)
                except CompanyStaff.DoesNotExist:
                    messages.error(request, 'Company staff not found.')
                    return redirect('accounts:login')
                
                employee = Employee.objects.filter(user=company_staff).first()
                if not employee:
                    messages.error(request, 'Employee profile not found.')
                    return render(request, "employee/apply-leaves.html", ctx)

                # Dropdown se jo manager select hua (jiska email dropdown me dikh raha hai), usi par notification jayega
                manager_obj = None
                if manager_id:
                    manager_obj = Manager.objects.filter(id=manager_id, user__company__id=company_id).first()
                    if not manager_obj:
                        messages.warning(request, 'Selected manager not found. Leave saved but manager will not receive email.')

                leave = Leave.objects.create(user=employee, manager=manager_obj, startdate=startdate, enddate=enddate, leavetype=leavetype, reason=reason, description=description)
                
                # Employee ko confirmation (optional - pehle wala flow)
                try:
                    from administration.email_notifications import send_leave_submission_notification
                    send_leave_submission_notification(leave, manager=manager_obj)
                except Exception:
                    pass
                # Manager ko pakka email – sirf plain text, simple logic
                if manager_obj:
                    manager_email = (getattr(manager_obj, 'manager_email', None) or '').strip()
                    if not manager_email and getattr(manager_obj, 'user', None):
                        manager_email = (getattr(manager_obj.user, 'email', None) or '').strip()
                    if manager_email:
                        try:
                            from administration.email_notifications import send_simple_email_to_manager
                            emp_name = f"{employee.employee_first_name} {employee.employee_last_name}"
                            send_simple_email_to_manager(
                                manager_email,
                                f"Leave applied by {emp_name} – Approve or Reject",
                                f"Employee {emp_name} has applied for leave. Please log in to HRMS portal to approve or reject."
                            )
                        except Exception as e:
                            print("LEAVE_MANAGER_EMAIL_ERROR:", str(e), flush=True)
                
                messages.success(request, 'Leave request created successfully!')
                return redirect(f'/employee/create_leave/{company_id}/{company_staff_id}')
            except Exception as e:
                messages.error(request, f'Error creating leave request: {str(e)}')
                return render(request, "employee/apply-leaves.html", ctx)

        else:
            return render(request, "employee/apply-leaves.html", ctx)


def create_resign(request,company_id, company_staff_id):
    if company_id:
        if request.method == "POST":
            try:
                startdate = request.POST.get("startdate")
                reason = request.POST.get("reason")
                assign = request.POST.get("manager_id")

                if not all([startdate, reason, assign]):
                    messages.error(request, 'All required fields must be filled.')
                    return render(request,"employee/apply-resignation.html",{
                        'rassigned':Manager.objects.filter(user__company__id=company_id),
                        'company_id':company_id, 
                        'company_staff_id':company_staff_id
                    })
                
                try:
                    assigned_too = Manager.objects.get(id=assign)
                except Manager.DoesNotExist:
                    messages.error(request, 'Selected manager not found.')
                    return render(request,"employee/apply-resignation.html",{
                        'rassigned':Manager.objects.filter(user__company__id=company_id),
                        'company_id':company_id, 
                        'company_staff_id':company_staff_id
                    })
                
                try:
                    company_staff = CompanyStaff.objects.get(id=company_staff_id)
                except CompanyStaff.DoesNotExist:
                    messages.error(request, 'Company staff not found.')
                    return redirect('accounts:login')
                
                emp = Employee.objects.filter(user=company_staff).first()
                if not emp:
                    messages.error(request, 'Employee profile not found.')
                    return render(request,"employee/apply-resignation.html",{
                        'rassigned':Manager.objects.filter(user__company__id=company_id),
                        'company_id':company_id, 
                        'company_staff_id':company_staff_id
                    })

                resign = Resign.objects.create(user=emp, startdate=startdate, reason=reason, assigned_too=assigned_too)
                # Manager ko pakka email – sirf plain text, simple logic
                manager_email = (getattr(assigned_too, 'manager_email', None) or '').strip()
                if not manager_email and getattr(assigned_too, 'user', None):
                    manager_email = (getattr(assigned_too.user, 'email', None) or '').strip()
                if manager_email:
                    try:
                        from administration.email_notifications import send_simple_email_to_manager
                        emp_name = f"{emp.employee_first_name} {emp.employee_last_name}"
                        send_simple_email_to_manager(
                            manager_email,
                            f"Resignation applied by {emp_name}",
                            f"Employee {emp_name} has submitted resignation. Please log in to HRMS portal to view and process."
                        )
                    except Exception as e:
                        print("RESIGN_MANAGER_EMAIL_ERROR:", str(e), flush=True)
                messages.success(request, 'Resignation request created successfully!')
                return redirect(f'/employee/create_resign/{company_id}/{company_staff_id}')
            except Exception as e:
                messages.error(request, f'Error creating resignation request: {str(e)}')
                return render(request,"employee/apply-resignation.html",{
                    'rassigned':Manager.objects.filter(user__company__id=company_id),
                    'company_id':company_id, 
                    'company_staff_id':company_staff_id
                })

        else:
            return render(request,"employee/apply-resignation.html",{
                'rassigned':Manager.objects.filter(user__company__id=company_id),
                'company_id':company_id, 
                'company_staff_id':company_staff_id
            })


def create_regularization(request,company_id, company_staff_id):
    if company_id:
        if request.method == "POST":
            try:
                from django.utils.dateparse import parse_datetime
                check_in = request.POST.get("check_in")
                check_out = request.POST.get("check_out")
                reason = request.POST.get("reason")
                assign_i = request.POST.get("manager_id")

                if not all([check_in, check_out, reason, assign_i]):
                    messages.error(request, 'All required fields must be filled.')
                    return render(request,"employee/regularization.html",{
                        'rassigne':Manager.objects.filter(user__company__id=company_id),
                        'company_id':company_id, 
                        'company_staff_id':company_staff_id
                    })
                
                try:
                    assigned_t = Manager.objects.get(id=assign_i)
                except Manager.DoesNotExist:
                    messages.error(request, 'Selected manager not found.')
                    return render(request,"employee/regularization.html",{
                        'rassigne':Manager.objects.filter(user__company__id=company_id),
                        'company_id':company_id, 
                        'company_staff_id':company_staff_id
                    })
                
                try:
                    company_staff = CompanyStaff.objects.get(id=company_staff_id)
                except CompanyStaff.DoesNotExist:
                    messages.error(request, 'Company staff not found.')
                    return redirect('accounts:login')
                
                emp = Employee.objects.filter(user=company_staff).first()
                if not emp:
                    messages.error(request, 'Employee profile not found.')
                    return render(request,"employee/regularization.html",{
                        'rassigne':Manager.objects.filter(user__company__id=company_id),
                        'company_id':company_id, 
                        'company_staff_id':company_staff_id
                    })

                # `datetime-local` sends ISO string without timezone => parse and make aware.
                check_in_dt = parse_datetime(check_in) if check_in else None
                check_out_dt = parse_datetime(check_out) if check_out else None
                if check_in_dt and timezone.is_naive(check_in_dt):
                    check_in_dt = timezone.make_aware(check_in_dt, timezone.get_current_timezone())
                if check_out_dt and timezone.is_naive(check_out_dt):
                    check_out_dt = timezone.make_aware(check_out_dt, timezone.get_current_timezone())

                Regularization.objects.create(
                    user=emp,
                    check_in=check_in_dt if check_in_dt else check_in,
                    check_out=check_out_dt if check_out_dt else check_out,
                    reason=reason,
                    r_assigned_to=assigned_t
                )
                messages.success(request, 'Regularization request created successfully!')
                return redirect(f'/employee/regularization_required/{company_id}/{company_staff_id}')
            except Exception as e:
                messages.error(request, f'Error creating regularization request: {str(e)}')
                return render(request,"employee/regularization.html",{
                    'rassigne':Manager.objects.filter(user__company__id=company_id),
                    'company_id':company_id, 
                    'company_staff_id':company_staff_id
                })

        else:
            return render(request,"employee/regularization.html",{
                'rassigne':Manager.objects.filter(user__company__id=company_id),
                'company_id':company_id, 
                'company_staff_id':company_staff_id
            })


def create_ducuments(request,company_id, company_staff_id):
    if company_id:
        if request.method == "POST":
            try:
                experience_letter = request.FILES.get("experience_letter")
                offer_letter = request.FILES.get("offer_letter")
                education_certificate = request.FILES.get("education_certificate")
                skill_certificate = request.FILES.get("skill_certificate")
                
                if not any([experience_letter, offer_letter, education_certificate, skill_certificate]):
                    messages.error(request, 'Please upload at least one document.')
                    return render(request,"employee/my-profile.html",{
                        'company_id':company_id, 
                        'company_staff_id':company_staff_id
                    })
                
                try:
                    company_staff = CompanyStaff.objects.get(id=company_staff_id)
                except CompanyStaff.DoesNotExist:
                    messages.error(request, 'Company staff not found.')
                    return redirect('accounts:login')
                
                emp = Employee.objects.filter(user=company_staff).first()
                if not emp:
                    messages.error(request, 'Employee profile not found.')
                    return render(request,"employee/my-profile.html",{
                        'company_id':company_id, 
                        'company_staff_id':company_staff_id
                    })
                
                document = Post.objects.create(
                    user=emp,
                    experience_letter=experience_letter,
                    offer_letter=offer_letter,
                    education_certificate=education_certificate,
                    skill_certificate=skill_certificate
                )
                
                # Send email notification
                try:
                    from administration.email_notifications import send_document_submission_notification
                    send_document_submission_notification(document, user_type='employee')
                except Exception as e:
                    print(f"Error sending document submission notification: {str(e)}")
                
                messages.success(request, 'Documents uploaded successfully!')
                return redirect(f'/employee/employee_profile/{company_id}/{company_staff_id}')
            except Exception as e:
                messages.error(request, f'Error uploading documents: {str(e)}')
                return render(request,"employee/my-profile.html",{
                    'company_id':company_id, 
                    'company_staff_id':company_staff_id
                })

        else:
            return render(request,"employee/my-profile.html",{
                'company_id':company_id, 
                'company_staff_id':company_staff_id
            })


def All_document_View(request,company_id, company_staff_id):
    if request.method == "POST":
        try:
            data = json.loads(request.body.decode('utf-8'))
            document_obj_id = data.get('id', None)
            if not document_obj_id:
                return JsonResponse({'error': 'Document ID is required'}, status=400)
            document_obj = Post.objects.get(pk=document_obj_id)
            return JsonResponse(document_obj.to_json())
        except Post.DoesNotExist:
            return JsonResponse({'error': 'Document not found'}, status=404)
        except json.JSONDecodeError:
            return JsonResponse({'error': 'Invalid JSON data'}, status=400)
        except Exception as e:
            return JsonResponse({'error': str(e)}, status=500)

    if company_id:
        try:
            company_staff = CompanyStaff.objects.get(id=company_staff_id)
        except CompanyStaff.DoesNotExist:
            messages.error(request, 'Company staff not found.')
            return redirect('accounts:login')
        
        employee = Employee.objects.filter(user=company_staff).first()
        if not employee:
            messages.error(request, 'Employee profile not found.')
            return render(request, 'employee/view_documents.html', {
                'document_list': Post.objects.none(),
                'company_id':company_id, 
                'company_staff_id':company_staff_id
            })
        
        document_list = Post.objects.filter(user=employee)
        return render(request, 'employee/view_documents.html', {
            'document_list': document_list,
            'company_id':company_id, 
            'company_staff_id':company_staff_id
        })


def ChangePassword(request,company_id, company_staff_id):
    if company_id:
        if request.method == "POST":
            try:
                password = request.POST.get("password")
                new_pas = request.POST.get("npwd")
                confirm_pas = request.POST.get("cpwd", "")

                if not all([password, new_pas]):
                    messages.error(request, 'All password fields are required.')
                    return render(request,"employee/change_password.html",{
                        'company_id':company_id, 
                        'company_staff_id':company_staff_id
                    })
                
                if new_pas != confirm_pas:
                    messages.error(request, 'New password and confirm password do not match.')
                    return render(request,"employee/change_password.html",{
                        'company_id':company_id, 
                        'company_staff_id':company_staff_id
                    })

                try:
                    user = CompanyStaff.objects.get(id=company_staff_id)
                except CompanyStaff.DoesNotExist:
                    messages.error(request, 'User not found.')
                    return redirect('accounts:login')
                
                check = check_password(password, user.password)
                if check == True:
                    user.password = make_password(new_pas)
                    user.save()
                    messages.success(request, 'Password changed Successfully')
                    return redirect('/')
                else:
                    messages.error(request, 'Current password is incorrect.')
            except Exception as e:
                messages.error(request, f'Error changing password: {str(e)}')

        return render(request,"employee/change_password.html",{
            'company_id':company_id, 
            'company_staff_id':company_staff_id
        })


def MyNotification(request,company_id, company_staff_id):
    context ={}
    try:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
    except CompanyStaff.DoesNotExist:
        messages.error(request, 'Company staff not found.')
        return redirect('accounts:login')
    
    employee = Employee.objects.filter(user=company_staff).first()
    if not employee:
        messages.error(request, 'Employee profile not found.')
        context['notification'] = EmployeeNotification.objects.none()
        context['company_id'] = company_id
        context['company_staff_id'] = company_staff_id
        return render(request, 'employee/mynotification.html', context)

    queryset = EmployeeNotification.objects.filter(user=employee)
    context['notification'] = queryset
    context['company_id'] = company_id
    context['company_staff_id'] = company_staff_id
    queryset.filter(is_read=False).update(is_read=True)
    company_staff.new_notification = False
    company_staff.save()
    request.session["new_notification"] = company_staff.new_notification
    return render(request, 'employee/mynotification.html', context)
