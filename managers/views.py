from django.contrib.auth.hashers import check_password, make_password
from django.http.response import HttpResponseRedirect
import json
import calendar
from django.contrib.auth import authenticate, login
from django.shortcuts import redirect
from django.views.generic import View

from account.models import CompanyStaff
from administration.models import Task, notification, holiday, MTask, Asign, ManagerNotification
from employee.models import Attendance, Entries, Employee
from leave.forms import LeaveCreationForm
from leave.models import Leave
from manager_leave.models import ManagerLeave, BalanceLeave
from manager_resign.models import ManagerResign
from managerpayroll.models import Salary
from manageregularization.forms import RegularizationCreationForm
from manageregularization.models import MRegularization
from regularization.models import Regularization
from resign.forms import ResignCreationForm
from resign.models import Resign
from .helpers.enum import attendance_type
from .helpers.helper import getgriddatapaginated, strfdelta, ajax_response
from django.contrib import messages
from datetime import datetime, timedelta
from django.utils import timezone as tz
from django.utils.dateparse import parse_datetime
from django.http import JsonResponse
from django.shortcuts import render, get_object_or_404
from django.contrib.auth.mixins import LoginRequiredMixin, UserPassesTestMixin
from django.contrib.auth.models import User
from django.views.generic import (
    ListView,
    DetailView,
    CreateView,
    UpdateView,
    DeleteView
)
from django.contrib.staticfiles.views import serve
from django.db.models import Q
from .models import Manager, ManagerAttendance, ManagerPost, EmployeeNotification
from administration.email_notifications import send_email_notification
from administration.models import ManagerNotification
from django.contrib.auth import update_session_auth_hash
from django.contrib.auth import get_user_model
User = get_user_model()


def _attendance_month_context(attendance_queryset, request):
    today = tz.localdate()
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
    month_start = tz.make_aware(datetime.combine(month_start_date, datetime.min.time()), tz.get_current_timezone())
    month_end = tz.make_aware(datetime.combine(month_end_date + timedelta(days=1), datetime.min.time()), tz.get_current_timezone())
    month_records = list(
        attendance_queryset.filter(check_in__gte=month_start, check_in__lt=month_end).order_by('check_in')
    )

    records_by_day = {}
    table_rows = []
    present_days = set()
    for record in month_records:
        local_check_in = tz.localtime(record.check_in) if tz.is_aware(record.check_in) else record.check_in
        local_check_out = tz.localtime(record.check_out) if record.check_out and tz.is_aware(record.check_out) else record.check_out
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
            'source': 'biometric' if getattr(record, 'manager_attendance_event_logs', None) else 'manual',
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


def manager_profile_view(request, company_id, company_staff_id):
    company_staff = CompanyStaff.objects.get(id=company_staff_id)
    profile = Manager.objects.filter(user=company_staff).first()
    ctx = {'profile': profile, 'company_id': company_id, 'company_staff_id': company_staff_id}
    ctx['manager_phone_display'] = ''
    ctx['manager_emergency_phone_display'] = ''
    if profile:
        # For phone fields: show only 10 digits in input (strip +91)
        raw_phone = (profile.manager_phone or '').replace('+91', '').replace(' ', '').strip()
        if raw_phone.isdigit():
            ctx['manager_phone_display'] = raw_phone[:10]
        else:
            ctx['manager_phone_display'] = ''
        raw_em = (profile.manager_emergency_primary_phone1 or '').replace('+91', '').replace(' ', '').strip()
        if raw_em.isdigit():
            ctx['manager_emergency_phone_display'] = raw_em[:10]
        else:
            ctx['manager_emergency_phone_display'] = ''

    if not company_id or not profile:
        return render(request, 'managers/my-profile.html', ctx)

    if request.method == "POST":
        data = dict(request.POST.copy())
        for field, value in data.items():
            if field not in ('csrfmiddlewaretoken', 'manager_image', 'cropped-image-input'):
                if hasattr(profile, field) and value:
                    setattr(profile, field, value[0])
        # Normalize phone: store as +91 + 10 digits
        phone = (request.POST.get('manager_phone') or '').strip().replace(' ', '')
        if phone.isdigit() and len(phone) == 10:
            profile.manager_phone = '+91' + phone
        elif phone.isdigit() and len(phone) <= 10:
            profile.manager_phone = '+91' + phone if phone else ''
        emergency_phone = (request.POST.get('manager_emergency_primary_phone1') or '').strip().replace(' ', '')
        if emergency_phone.isdigit() and len(emergency_phone) == 10:
            profile.manager_emergency_primary_phone1 = '+91' + emergency_phone
        elif emergency_phone.isdigit() and len(emergency_phone) <= 10:
            profile.manager_emergency_primary_phone1 = '+91' + emergency_phone if emergency_phone else ''

        # Handle cropped image (base64) or regular file upload
        cropped_image_data = request.POST.get('cropped-image-input', '')
        if cropped_image_data and cropped_image_data.startswith('data:image'):
            import base64
            from django.core.files.base import ContentFile
            from django.utils.text import slugify
            import uuid
            try:
                format_part, imgstr = cropped_image_data.split(';base64,')
                ext = format_part.split('/')[-1] if '/' in format_part else 'jpg'
                name = f"manager_{slugify(profile.manager_first_name or '')}_{uuid.uuid4().hex[:8]}.{ext}"
                profile.manager_image = ContentFile(base64.b64decode(imgstr), name=name)
            except Exception as e:
                messages.error(request, f'Error processing cropped image: {str(e)}')
        elif 'manager_image' in request.FILES:
            profile.manager_image = request.FILES['manager_image']

        profile.save()
        messages.success(request, 'Profile updated successfully!')
        return redirect('manager_profile', company_id=company_id, company_staff_id=company_staff_id)

    return render(request, 'managers/my-profile.html', ctx)


def upload_manager_profile_image(request, company_id, company_staff_id):
    """Manager profile photo upload (e.g. from crop or direct file)."""
    if request.method != 'POST':
        return JsonResponse({'error': 'Method not allowed'}, status=405)
    try:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
    except CompanyStaff.DoesNotExist:
        return JsonResponse({'error': 'Company staff not found'}, status=404)
    profile = Manager.objects.filter(user=company_staff).first()
    if not profile:
        return JsonResponse({'error': 'Manager profile not found'}, status=404)
    if 'manager_image' not in request.FILES:
        return JsonResponse({'error': 'No image file provided'}, status=400)
    try:
        profile.manager_image = request.FILES['manager_image']
        profile.save()
        return JsonResponse({
            'success': True,
            'message': 'Profile photo updated.',
            'image_url': profile.manager_image.url if profile.manager_image else None,
        })
    except Exception as e:
        return JsonResponse({'error': str(e)}, status=500)


def remove_manager_profile_image(request, company_id, company_staff_id):
    """Manager profile photo remove."""
    if request.method != 'POST':
        messages.error(request, 'Invalid request.')
        return redirect('manager_profile', company_id=company_id, company_staff_id=company_staff_id)
    try:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
    except CompanyStaff.DoesNotExist:
        messages.error(request, 'Company staff not found.')
        return redirect('manager_profile', company_id=company_id, company_staff_id=company_staff_id)
    profile = Manager.objects.filter(user=company_staff).first()
    if not profile:
        messages.error(request, 'Manager profile not found.')
        return redirect('manager_profile', company_id=company_id, company_staff_id=company_staff_id)
    try:
        if profile.manager_image:
            try:
                profile.manager_image.delete(save=False)
            except Exception:
                pass
        profile.manager_image = ''
        profile.save()
        messages.success(request, 'Profile photo removed.')
    except Exception as e:
        messages.error(request, f'Error removing photo: {str(e)}')
    return redirect('manager_profile', company_id=company_id, company_staff_id=company_staff_id)


class managerUpdateView(UpdateView):
    model = Manager
    template_name = 'managers/my-profile.html'
    fields = '__all__'
    context_object_name = 'manager_update'


def ManagerDashboardView(request, company_id, company_staff_id):
    ctx = {}
    today = datetime.now().date()
    tomorrow = today + timedelta(1)

    company_staff = CompanyStaff.objects.get(id=company_staff_id)
    try:
        manager = company_staff.manager
    except Manager.DoesNotExist:
        manager = None

    # Manager's own attendance (today)
    att = None
    if manager:
        att = ManagerAttendance.objects.filter(
            Q(check_in__gte=today) & Q(check_in__lt=tomorrow) & Q(manager=manager)
        ).first()
    ctx['attendance'] = att
    ctx['company_id'] = company_id
    ctx['company_staff_id'] = company_staff_id
    if att:
        ctx['hours_num'] = strfdelta((att.check_out - att.check_in), "{hours}:{minutes}:{seconds}") if att.check_out else ''
        ctx['is_check_in'] = attendance_type.check_out.value
        ctx['is_complete_attendance'] = bool(att.check_in and att.check_out)
    else:
        ctx['hours_num'] = '0:0:0'
        ctx['is_check_in'] = attendance_type.check_in.value
        ctx['is_complete_attendance'] = False

    # 1. Team members (employees under this manager)
    team_members = []
    if manager:
        team_members = Employee.objects.filter(
            employee_reports_to=manager,
            user__company_id=company_id
        ).select_related('user', 'employee_department').order_by('employee_first_name')
    ctx['team_members'] = team_members

    # 2. Pending leave requests (team only) for approval - show only latest few
    pending_leaves = []
    if manager:
        pending_leaves = Leave.objects.all_pending_leaves().filter(
            user__employee_reports_to=manager
        ).select_related('user').order_by('-created')[:4]
    ctx['pending_leaves'] = pending_leaves

    # 2b. Pending resignation requests (team only) for approval - show only latest few
    pending_resignations = []
    if manager:
        pending_resignations = Resign.objects.all_pending_resign().filter(
            assigned_too=manager
        ).select_related('user').order_by('-created')[:4]
    ctx['pending_resignations'] = pending_resignations

    # 3. Team attendance overview (today)
    team_attendance_today = []
    if manager:
        team_employee_ids = list(Employee.objects.filter(employee_reports_to=manager).values_list('id', flat=True))
        team_attendance_today = Attendance.objects.filter(
            employee_id__in=team_employee_ids,
            check_in__date=today
        ).select_related('employee').order_by('-check_in')
    ctx['team_attendance_today'] = team_attendance_today

    # 4. Team project overview (Task + Asign for this manager's team)
    team_tasks = []
    team_assigns = []
    if manager:
        team_tasks = Task.objects.filter(
            assigned_to__employee_reports_to=manager,
            company_id=company_id
        ).select_related('assigned_to')[:4]
        team_assigns = Asign.objects.filter(assigned_to=manager).select_related('employee').order_by('-created_date')[:4]
    ctx['team_tasks'] = team_tasks
    ctx['team_assigns'] = team_assigns

    # 5. Team-related notifications (for this manager)
    team_notifications = []
    if manager:
        team_notifications = ManagerNotification.objects.filter(user=manager, is_read=False).order_by('-id')[:4]
    ctx['team_notifications'] = team_notifications
    ctx['manager'] = manager

    return render(request, "managers/index.html", ctx)

def leave_creation(request,company_id, company_staff_id):
    if company_id:
        if request.method == 'POST':
            form = LeaveCreationForm(data=request.POST)
            if form.is_valid():
                instance = form.save(commit=False)
                company_staff = CompanyStaff.objects.get(id=company_staff_id)
                user = company_staff.manager
                instance.user = user
                instance.save()
                
                # Send email notification
                try:
                    from administration.email_notifications import send_leave_submission_notification
                    send_leave_submission_notification(instance)
                except Exception as e:
                    print(f"Error sending leave submission notification: {str(e)}")
                
                messages.success(request, 'Leave Request Sent,wait for response',
                                 extra_tags='alert alert-success alert-dismissible show')
                return redirect('/managers/mleaves/view/table/')

            messages.error(request, 'failed to Request a Leave,please check entry dates',
                           extra_tags='alert alert-warning alert-dismissible show')
            return redirect(f'/managers/leave/{company_id}/{company_staff_id}')

        dataset = dict()
        form = LeaveCreationForm()
        dataset['form'] = form
        dataset['title'] = 'Apply for Leave'
        dataset['company_id'] = company_id
        dataset['company_staff_id'] = company_staff_id
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
        try:
            current_mgr = company_staff.manager
        except Manager.DoesNotExist:
            current_mgr = None
        managers_qs = Manager.objects.filter(user__company_id=company_id).order_by('manager_first_name', 'manager_last_name')
        if current_mgr:
            managers_qs = managers_qs.exclude(id=current_mgr.id)
        dataset['managers'] = list(managers_qs)
        return render(request, 'managers/apply-leave.html', dataset)


#  staffs leaves table user only
def view_my_leave_table(request,company_id, company_staff_id):
    # work on the logics
    if company_id:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
        user = company_staff.manager
        leaves = ManagerLeave.objects.filter(user=user)
        # manager = Manager.objects.filter(user=user.manager_email).first()
        dataset = dict()
        dataset['leave_list'] = leaves
        # dataset['manager'] = manager
        dataset['title'] = 'Leaves List'
        dataset['company_id'] = company_id
        dataset['company_staff_id'] = company_staff_id
    else:
        return redirect('accounts:login')
    return render(request, 'managers/leave-status.html', dataset)


class LeaveRemove(View):
    def get(self, request, id):
        leave = Leave.objects.get(id=id)
        leave.delete()
        return HttpResponseRedirect('/managers/manager_dashboard/')


def BalanceLeaveView(request,company_id, company_staff_id):
    context ={}

    company_staff = CompanyStaff.objects.get(id=company_staff_id)

    queryset = BalanceLeave.objects.filter(user=company_staff.manager)
    print('queryset: ', queryset)
    context['balance']= queryset
    context['company_id']= company_id
    context['company_staff_id']= company_staff_id
    return render(request, 'managers/leave-balance.html', context)


def attendance(request,company_id, company_staff_id):
    ctx = {}
    today = datetime.now().date()
    tomorrow = today + timedelta(1)

    company_staff = CompanyStaff.objects.get(id=company_staff_id)
    manager = company_staff.manager
    att = ManagerAttendance.objects.filter(Q(check_in__gt=today)
                                           & Q(check_in__lt=tomorrow)
                                           & Q(manager=manager)).first()
    ctx['attendance'] = att
    ctx['company_id'] = company_id
    ctx['company_staff_id'] = company_staff_id
    if att:
        ctx['hours_num'] = strfdelta((att.check_out - att.check_in),
                                     "{hours}:{minutes}:{seconds}") if att.check_out else ''
        ctx['is_check_in'] = attendance_type.check_out.value
        ctx['is_complete_attendance'] = True if att.check_in and att.check_out else False
    else:
        ctx['is_check_in'] = attendance_type.check_in.value
    ctx.update(_attendance_month_context(ManagerAttendance.objects.filter(manager=manager), request))
    return render(request, 'managers/attendance-info.html', ctx)


def regularization_required_attendance(request,company_id, company_staff_id):
    company_staff = CompanyStaff.objects.get(id=company_staff_id)
    atts = ManagerAttendance.objects.filter(manager=company_staff.manager)
    if atts:
        for att in atts:
            if att.regularization_required == False:
                atts = atts.exclude(id=att.id)
    print(atts)
    return render(request, 'managers/regularization.html', context={'atts': atts,'company_id':company_id, 'company_staff_id':company_staff_id})


def attendance_post(request,company_id, company_staff_id):
    if company_id:
        try:
            is_check_in = request.POST['is_check_in']
            attendance_id = request.POST['attendance_id']
            attendance_obj = ManagerAttendance.objects.filter(
                id=attendance_id).first() if attendance_id else ManagerAttendance()
            if is_check_in == attendance_type.check_in.value:
                attendance_obj.check_in = tz.now()
            else:
                attendance_obj.check_out = tz.now()
            company_staff = CompanyStaff.objects.get(id=company_staff_id)
            attendance_obj.manager = company_staff.manager
            attendance_obj.save()
            
            # Send email notification for manager attendance
            try:
                from administration.email_notifications import send_attendance_notification
                action = 'check_in' if is_check_in == attendance_type.check_in.value else 'check_out'
                # Create a wrapper to make ManagerAttendance compatible with attendance notification
                class AttendanceWrapper:
                    def __init__(self, manager_attendance):
                        self.manager_attendance = manager_attendance
                        self.check_in = manager_attendance.check_in
                        self.check_out = manager_attendance.check_out
                        self.employee = None  # Managers don't have employee field
                        # Create a mock employee-like object for manager
                        class ManagerAsEmployee:
                            def __init__(self, manager):
                                self.employee_first_name = manager.manager_first_name
                                self.employee_last_name = manager.manager_last_name
                                self.employee_email = manager.manager_email
                                self.employee_id = manager.manager_id
                                self.employee_reports_to = None
                        self.manager = ManagerAsEmployee(manager_attendance.manager)
                
                # For managers, we'll send a simplified notification
                # The function needs to be updated to handle managers, but for now we'll skip
                # send_attendance_notification(AttendanceWrapper(attendance_obj), action=action)
            except Exception as e:
                print(f"Error sending manager attendance notification: {str(e)}")
            
            return JsonResponse({'status': 'SUCCESS'}, status=200)
        except Exception as e:
            return JsonResponse({'status': "FAILED"}, status=500)


def attendance_grid_data(request,company_id, company_staff_id):
    if company_id:
        grid_columns = ('check_in', 'check_out')
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
        attendance_list = ManagerAttendance.objects.filter(manager=company_staff.manager)
        sort_column = grid_columns[int(request.GET['order[0][column]'])]
        ctx = getgriddatapaginated(request, attendance_list, sort_column)
        ctx['company_id'] = company_id
        ctx['company_staff_id'] = company_staff_id
        json_data = []
        for item in ctx['data']:
            dict = {}
            _ci = tz.localtime(item.check_in) if getattr(item.check_in, 'tzinfo', None) and tz.is_aware(item.check_in) else item.check_in
            dict['check_in'] = _ci.strftime("%Y-%m-%d %H:%M:%S") if item.check_in else ''
            if item.check_out:
                _co = tz.localtime(item.check_out) if getattr(item.check_out, 'tzinfo', None) and tz.is_aware(item.check_out) else item.check_out
                dict['check_out'] = _co.strftime("%Y-%m-%d %H:%M:%S")
                dict['working_hours'] = strfdelta((item.check_out - item.check_in), "{hours}:{minutes}")
            else:
                dict['check_out'] = ''
                dict['working_hours'] = ''
            json_data.append(dict)
        ctx['data'] = json_data
        return ajax_response(ctx)


def SalaryListView(request,company_id, company_staff_id):
    context ={}

    company_staff = CompanyStaff.objects.get(id=company_staff_id)

    queryset = Salary.objects.filter(manager=company_staff.manager)
    print('queryset: ', queryset)
    context['salary']= queryset
    context['company_id']= company_id
    context['company_staff_id']= company_staff_id
    return render(request, 'managers/salary.html', context)


def notifications(request,company_id, company_staff_id):
    if company_id:
        notify = notification.objects.filter(company__id=company_id)
        user = CompanyStaff.objects.get(id=company_staff_id, company_id=company_id)
        manager = Manager.objects.get(user=user)
        private_notifications = ManagerNotification.objects.filter(user=manager, is_read=False).count()
        context = {
            'notify': notify,
            'company_id': company_id,
            'company_staff_id': company_staff_id,
            'unread_count': private_notifications,
        }
        if private_notifications == 0:
            user.new_notification = False
            user.save()
            request.session["new_notification"] = user.new_notification
        return render(request, 'managers/notifications.html', context)


def resign_creation(request):
    if request.method == 'POST':
        form = ResignCreationForm(data=request.POST)
        if form.is_valid():
            instance = form.save(commit=False)
            user = request.user
            instance.user = user
            instance.save()

            messages.success(request, 'Resignation Request Sent,wait for response',
                             extra_tags='alert alert-success alert-dismissible show')
            return redirect('/managers/mresign/view/table/')

        messages.error(request, 'failed to Request a Resignation,please check entry dates',
                       extra_tags='alert alert-warning alert-dismissible show')
        return redirect('/managers/resign/')

    dataset = dict()
    form = ResignCreationForm()
    dataset['form'] = form
    dataset['title'] = 'Apply for Resignation'
    return render(request, 'managers/apply-resignation.html', dataset)


def view_my_resign_table(request,company_id, company_staff_id):
    # work on the logics
    if company_id:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
        user = company_staff.manager
        resign = ManagerResign.objects.filter(user=user)
        manager = Manager.objects.filter(user__company__id=company_id).first()
        dataset = dict()
        dataset['resign_list'] = resign
        dataset['manager'] = manager
        dataset['title'] = 'Resign List'
        dataset['company_id'] = company_id
        dataset['company_staff_id'] = company_staff_id
    else:
        return redirect('accounts:login')
    return render(request, 'managers/resignation-status.html', dataset)


class ResignRemove(View):
    def get(self, request, id):
        resign = Resign.objects.get(id=id)
        resign.delete()
        return HttpResponseRedirect('/managers/manager_dashboard/')


def holidays(request,company_id, company_staff_id):
    if company_id:
        holyday = holiday.objects.filter(company_id=company_id)
        context = {
            'holyday': holyday,
            'company_id': company_id,
            'company_staff_id': company_staff_id,

        }
        return render(request, 'managers/holiday.html', context)


def getfile(request):
    return serve(request, 'File')


class PostListView(ListView):
    model = ManagerPost
    template_name = 'managers/home.html'
    context_object_name = 'posts'
    ordering = ['-date_posted']
    paginate_by = 2


class UserPostListView(LoginRequiredMixin, ListView):
    model = ManagerPost
    template_name = 'managers/my-profile.html'
    context_object_name = 'posts'

    def get_queryset(self):
        queryset = super(UserPostListView, self).get_queryset()
        queryset = ManagerPost.objects.filter(user=self.request.user)
        return queryset


class PostDetailView(DetailView):
    model = ManagerPost
    template_name = 'managers/post_detail.html'
    context_object_name = 'posts'

    def get_queryset(self):
        queryset = super(PostDetailView, self).get_queryset()
        queryset = ManagerPost.objects.filter(user=self.request.user.manager)
        return queryset


class PostCreateView(CreateView):
    model = ManagerPost
    template_name = 'managers/post_form.html'
    fields = ['experience_letter', 'offer_letter', 'education_certificate', 'skill_certificate', ]

    def form_valid(self, form):
        form.instance.user = self.request.user.manager
        return super().form_valid(form)


class PostUpdateView(UpdateView):
    model = ManagerPost
    template_name = 'managers/post_form.html'
    fields = ['file']

    def form_valid(self, form):
        form.instance.user = self.request.user.manager
        return super().form_valid(form)

    def test_func(self):
        post = self.get_object()
        if self.request.user == post.user.manager:
            return True
        return False


class PostDeleteView(DeleteView):
    model = ManagerPost
    success_url = '/managers/manager_dashboard/'
    template_name = 'managers/view_documents.html'

    def test_func(self):
        post = self.get_object()
        if self.request.user == post.user.manager:
            return True
        return False


def mregularization(request):
    if request.method == 'POST':
        form = RegularizationCreationForm(data=request.POST)
        if form.is_valid():
            instance = form.save(commit=False)
            user = request.user.manager
            instance.user = user
            instance.save()

            messages.success(request, 'Apply for regularization Request Sent,wait for response',
                             extra_tags='alert alert-success alert-dismissible show')
            return redirect('/managers/attendanc/')

        messages.error(request, 'failed to Request a Regularizations,please check entry dates',
                       extra_tags='alert alert-warning alert-dismissible show')
        return redirect('/managers/mregularizations/')

    dataset = dict()
    form = RegularizationCreationForm()
    dataset['form'] = form
    dataset['title'] = 'Apply for Regularization'
    return render(request, 'managers/regularization.html', dataset)


def view_my_regularization_table(request,company_id, company_staff_id):
    # work on the logics
    if company_id:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
        user = company_staff.manager
        regularization = MRegularization.objects.filter(user=user)
        dataset = dict()
        dataset['regularization_list'] = regularization
        dataset['title'] = 'regularization List'
        dataset['company_id'] = company_id
        dataset['company_staff_id'] = company_staff_id
    else:
        return redirect('accounts:login')
    return render(request, 'managers/attendance-status.html', dataset)


class TaskCreateViews(CreateView, LoginRequiredMixin):
    model = MTask
    fields = ['title', 'description', 'assigned_to']

    def form_valid(self, form):
        form.instance.created_by = self.request.user.email
        return super().form_valid(form)


class TaskDetailViews(DetailView, LoginRequiredMixin):
    model = MTask
    template_name = "managers/task_detail.html"


class TaskDeleteViews(DeleteView, LoginRequiredMixin, UserPassesTestMixin):
    model = MTask
    success_url = '/managers/dashboard/'

    def test_func(self):
        task = self.get_object()
        return self.request.user == task.created_by


def Project_list(request,company_id, company_staff_id):
    if company_id:
        project = MTask.objects.filter(assigned_to__user__company__id=company_id)
        context = {
            'project': project,
            'company_id': company_id,
            'company_staff_id': company_staff_id,

        }
        return render(request, 'managers/list-project.html', context)


class ProjectRemove(View):
    def get(self, request,company_id, company_staff_id, id):
        if company_id:
            project = MTask.objects.get(id=id)
            project.delete()
            return redirect(f'/managers/mprojectlist/{company_id}/{company_staff_id}')


def TaskListView(request,company_id, company_staff_id):
    context ={}

    company_staff = CompanyStaff.objects.get(id=company_staff_id)

    # Manager "My Project" list should show manager-created tasks (MTask),
    # not admin Task (which is assigned to Employee).
    try:
        manager = company_staff.manager
    except Manager.DoesNotExist:
        manager = None

    queryset = MTask.objects.none()
    admin_projects = []
    if manager:
        queryset = MTask.objects.filter(user=manager)
        # Admin assigned projects to this manager
        try:
            from administration.models import ManagerProject
            admin_projects = list(ManagerProject.objects.filter(assigned_to=manager).order_by('-created_date'))
        except Exception:
            admin_projects = []
    print('queryset: ', queryset)
    # Combine both sources for display
    combined = []
    for t in queryset:
        combined.append({
            'created_date': t.created_date,
            'title': t.title,
            'description': t.description,
            'source': 'Manager',
        })
    for p in admin_projects:
        combined.append({
            'created_date': p.created_date,
            'title': p.title,
            'description': p.description,
            'source': 'Admin',
        })
    combined.sort(key=lambda x: (x['created_date'] or datetime.min.date()), reverse=True)

    context['tasks'] = combined
    context['company_id']= company_id
    context['company_staff_id']= company_staff_id
    return render(request, 'managers/my-project.html', context)


def attendanc(request,company_id, company_staff_id):
    if company_id:
        attendance = Attendance.objects.filter(employee__user__company__id=company_id)
        context = {
            'attendance': attendance,
            'company_id': company_id,
            'company_staff_id':company_staff_id,

        }
    return render(request, 'managers/attendance-list.html',context)


def mattendance_Edit_View(request,company_id, company_staff_id):
    if company_id:
        if request.method == "GET":
            id = request.GET.get('id')
            attendance_obj = Attendance.objects.get(pk=id)
            return JsonResponse(attendance_obj.to_json())

        elif request.method == "POST":
            attendance_models_fields_list = [f.name for f in Attendance._meta.get_fields()]
            attendance_models_fields_dict = {}
            attendance_obj_id = request.POST.get('id')
            attendance_obj = Attendance.objects.filter(pk=attendance_obj_id)

            for key, value in request.POST.items():
                if key in attendance_models_fields_list and key != 'id' and key != 'id' and value is not None and len(
                        value) != 0:
                    print(key, value)
                    attendance_models_fields_dict.setdefault(key, value)
            attendance_obj.update(**attendance_models_fields_dict)
            emp_id = request.POST.get('id')

            print('attendance id is-')
            print(emp_id)
            return redirect(f'/managers/mnattendancee/{company_id}/{company_staff_id}')


class AttendanceRemove(View):
    def get(self, request,company_id, company_staff_id, id):
        if company_id:
            attendance = Attendance.objects.get(id=id)
            attendance.delete()
            messages.success(request, f"{attendance} deleted successfully")
            return redirect(f'/managers/mnattendancee/{company_id}/{company_staff_id}')


class AttendanceManage(UpdateView):
    model = Attendance
    fields = ['check_in', 'check_out']
    context_object_name = "attendance_update"
    template_name = "managers/attendance_manage.html"
    success_url = ("/managers/attendanc/")

    def post(self, request, pk):
        data = Attendance.objects.get(id=pk)
        check_out_str = request.POST.get('check_out')
        check_in_str = request.POST.get('check_in')

        # `datetime-local` sends an ISO string without timezone => Django parses it as naive.
        # Convert to timezone-aware to avoid warnings and inconsistent storage.
        check_out_dt = parse_datetime(check_out_str) if check_out_str else None
        check_in_dt = parse_datetime(check_in_str) if check_in_str else None
        if check_out_dt and tz.is_naive(check_out_dt):
            check_out_dt = tz.make_aware(check_out_dt, tz.get_current_timezone())
        if check_in_dt and tz.is_naive(check_in_dt):
            check_in_dt = tz.make_aware(check_in_dt, tz.get_current_timezone())

        if check_out_dt:
            data.check_out = check_out_dt
        else:
            data.check_out = check_out_str
        if check_in_dt:
            data.check_in = check_in_dt
        else:
            data.check_in = check_in_str
        data.save()
        print(str(data) + "=" + str(request.POST.get('check_out')))
        return HttpResponseRedirect("/managers/mnattendancee/")


def Attendancesearch(request,company_id, company_staff_id):
    if 'q' in request.GET:
        q = request.GET['q']
        multiple_q = Q(Q(employee__user__email__icontains=q) | Q(check_in__icontains=q) | Q(check_out__icontains=q))
        attendance = Attendance.objects.filter(multiple_q)
    else:
        attendance = Attendance.objects.filter(employee__user__company__id=company_id)
    context = {
        'attendance': attendance,
        'company_id': company_id,
        'company_staff_id': company_staff_id
    }
    return render(request, 'managers/attendance-list.html', context)


def regularization_list(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        regularization_obj_id = data.get('id', None)
        regularization_obj = Regularization.objects.get(pk=regularization_obj_id)
        return JsonResponse(regularization_obj.to_json())

    if company_id:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
        regularization = Regularization.objects.all_pending_regularization().filter(r_assigned_to=company_staff.manager)
        return render(request, 'managers/pending-regularization.html',{'regularization_list': regularization, 'title': 'regularization list - pending','company_id':company_id, 'company_staff_id':company_staff_id})


def regularization_approved_list(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        regularization_obj_id = data.get('id', None)
        regularization_obj = Regularization.objects.get(pk=regularization_obj_id)
        return JsonResponse(regularization_obj.to_json())

    if company_id:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
    regularization = Regularization.objects.all_approved_regularization().filter(r_assigned_to=company_staff.manager) # approved leaves -> calling model manager method
    return render(request, 'managers/approved-regularization.html',
                  {'regularization_list': regularization, 'title': 'approved regularization list','company_id':company_id, 'company_staff_id':company_staff_id})


def regularization_view(request, id):
    if not request.user.is_staff:
        return redirect('/')

    regularization = get_object_or_404(Regularization, id=id)
    return render(request, 'managers/regularization_detail_view.html',
                  {'regularization': regularization, 'attendance': attendance,
                   'title': '{0}-{1} regularization'.format(
                       regularization.user.employee_email,
                       regularization.status)})


def approve_regularization(request,company_id, company_staff_id,id):
    if company_id:
        regularization = get_object_or_404(Regularization, id=id)
        regularization.approve_regularization
        messages.error(request, 'regularizationation successfully approved',
                       extra_tags='alert alert-success alert-dismissible show')
        return redirect(f'/managers/mnregularization/approved/all/{company_id}/{company_staff_id}')


def cancel_regularization_list(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        regularization_obj_id = data.get('id', None)
        regularization_obj = Regularization.objects.get(pk=regularization_obj_id)
        return JsonResponse(regularization_obj.to_json())

    if company_id:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
    regularization = Regularization.objects.all_cancel_regularization().filter(r_assigned_to=company_staff.manager)
    return render(request, 'managers/cancelled-regularization.html',
                  {'regularization_list_cancel': regularization, 'title': 'Cancel regularization list','company_id':company_id, 'company_staff_id':company_staff_id})


def unapprove_regularization(request, id):
    if not request.user.is_staff:
        return redirect('/')
    regularization = get_object_or_404(Regularization, id=id)
    regularization.unapprove_regularization
    return redirect('mnregularizationlist')  # redirect to unapproved list


def cancel_regularization(request,company_id, company_staff_id,id):
    if company_id:
        regularization = get_object_or_404(Regularization, id=id)
        regularization.regularization_cancel

        messages.success(request, 'regularization is canceled', extra_tags='alert alert-success alert-dismissible show')
        return redirect(f'/managers/mnregularization/cancel/all/{company_id}/{company_staff_id}')


# Current section -> here
def uncancel_regularization(request, id):
    if not request.user.is_staff:
        return redirect('/')
    regularization = get_object_or_404(Regularization, id=id)
    regularization.status = 'pending'
    regularization.is_approved = False
    regularization.save()
    messages.success(request, 'Regulaization is uncanceled,now in pending list',
                     extra_tags='alert alert-success alert-dismissible show')
    return redirect('mncancelregularizationlist')


def regularization_rejected_list(request):
    dataset = dict()
    regularization = Regularization.objects.all_rejected_regularization()

    dataset['regularization_list_rejected'] = regularization
    return render(request, 'managers/rejected_regularization_list.html', dataset)


def reject_regularization(request, id):
    dataset = dict()
    regularization = get_object_or_404(Leave, id=id)
    regularization.reject_leave
    messages.success(request, 'regularizationation is rejected',
                     extra_tags='alert alert-success alert-dismissible show')
    return redirect('mnregularizationrejected')


def unreject_regularization(request, id):
    regularization = get_object_or_404(Regularization, id=id)
    regularization.status = 'pending'
    regularization.is_approved = False
    regularization.save()
    messages.success(request, 'regularizationation is now in pending list ',
                     extra_tags='alert alert-success alert-dismissible show')

    return redirect('mnregularizationrejected')

def AssignListView(request,company_id, company_staff_id):
    context ={}

    company_staff = CompanyStaff.objects.get(id=company_staff_id)

    queryset = Asign.objects.filter(assigned_to=company_staff.manager)
    print('queryset: ', queryset)
    context['assign']= queryset
    context['company_id']= company_id
    context['company_staff_id']= company_staff_id
    return render(request, 'managers/list-employee.html', context)


def EntryListView(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        entry_obj_id = data.get('id', None)
        entry_obj = Entries.objects.get(pk=entry_obj_id)
        return JsonResponse(entry_obj.to_json())
    if company_id:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
        entry = Entries.objects.filter(assigned_to=company_staff.manager)
        
        # Date range filter logic
        start_date_str = request.GET.get('start_date', '')
        end_date_str = request.GET.get('end_date', '')
        if start_date_str:
            try:
                start_date = datetime.strptime(start_date_str, '%Y-%m-%d').date()
                entry = entry.filter(end_time__date__gte=start_date)
            except ValueError:
                pass
        if end_date_str:
            try:
                end_date = datetime.strptime(end_date_str, '%Y-%m-%d').date()
                entry = entry.filter(end_time__date__lte=end_date)
            except ValueError:
                pass
        
        from collections import defaultdict
        # Group by employee directly
        employee_map = defaultdict(list)
        for obj in entry:
            employee_map[obj.user].append(obj)

        employee_list = []
        for user, entries in employee_map.items():
            user_total = sum((obj.total_duration for obj in entries), timedelta())
            # Sort entries by project, then by start_time
            entries.sort(key=lambda x: (str(x.project), x.start_time))
            employee_list.append({
                'user': user,
                'email': user.user.email if user.user else '',
                'name': f"{user.employee_first_name} {user.employee_last_name}",
                'total_time': user_total,
                'entries': entries
            })

        # Sort employees by email
        employee_list.sort(key=lambda e: e['email'])

        context = {
            'employee_list': employee_list,
            'company_id': company_id,
            'company_staff_id': company_staff_id,
            'start_date': start_date_str,
            'end_date': end_date_str,
        }
        return render(request, 'managers/employee-timesheet.html', context)


class EntryRemove(View):
    def get(self, request, id):
        entry = Entries.objects.get(id=id)
        entry.delete()
        return HttpResponseRedirect('/managers/entry-list/')


def create_ducument(request,company_id, company_staff_id):
    if company_id:
        if request.method == "POST":
            experience_letter = request.POST.get("experience_letter")
            offer_letter = request.POST.get("offer_letter")
            education_certificate = request.POST.get("education_certificate")
            skill_certificate = request.POST.get("skill_certificate")
            company_staff = CompanyStaff.objects.get(id=company_staff_id)
            user = company_staff
            emp = Manager.objects.get(user=user)
            document = ManagerPost.objects.create(user=emp, experience_letter=experience_letter, offer_letter=offer_letter,
                                       education_certificate=education_certificate, skill_certificate=skill_certificate)
            
            # Send email notification
            try:
                from administration.email_notifications import send_document_submission_notification
                send_document_submission_notification(document, user_type='manager')
            except Exception as e:
                print(f"Error sending document submission notification: {str(e)}")
            
            return redirect(f'/managers/manager_profile/{company_id}/{company_staff_id}')

        else:
            return render(request, "managers/my-profile.html",{'company_id':company_id, 'company_staff_id':company_staff_id})


def create_mregularizations(request,company_id, company_staff_id):
    if company_id:
        if request.method == "POST":
            check_in = request.POST.get("check_in")
            check_out = request.POST.get("check_out")

            reason = request.POST.get("reason")

            company_staff = CompanyStaff.objects.get(id=company_staff_id)
            user = company_staff
            emp = Manager.objects.get(user=user)

            MRegularization.objects.create(user=emp, check_in=check_in, check_out=check_out, reason=reason)
            return redirect(f'/managers/mregularization_required/{company_id}/{company_staff_id}')

        else:
            return render(request, "managers/regularization.html", {'rassigne': Manager.objects.all()},{'company_id':company_id, 'company_staff_id':company_staff_id})


def add_project(request,company_id, company_staff_id):
    if company_id:
        if request.method == "POST":
            title = request.POST.get("title")
            description = request.POST.get("description")

            assign_i = request.POST.get("employee_id")
            assigned_t = Employee.objects.get(id=assign_i)
            company_staff = CompanyStaff.objects.get(id=company_staff_id)
            user = company_staff
            emp = Manager.objects.get(user=user)

            MTask.objects.create(user=emp, title=title, description=description, assigned_to=assigned_t)
            return redirect(f'/managers/mprojectlist/{company_id}/{company_staff_id}')

        else:
            return render(request, "managers/add-project.html", {'addProject': Employee.objects.filter(user__company__id=company_id),'company_id':company_id, 'company_staff_id':company_staff_id})


def add_leave(request, company_id, company_staff_id):
    if company_id:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
        try:
            current_manager = company_staff.manager
        except Manager.DoesNotExist:
            current_manager = None

        # Managers in same company for "Reporting to" dropdown (exclude self)
        managers_qs = Manager.objects.filter(user__company_id=company_id).order_by('manager_first_name', 'manager_last_name')
        if current_manager:
            managers_qs = managers_qs.exclude(id=current_manager.id)
        managers_list = list(managers_qs)

        if request.method == "POST":
            startdate = request.POST.get("startdate")
            enddate = request.POST.get("enddate")
            leavetype = request.POST.get("leavetype")
            reason = request.POST.get("reason")
            description = request.POST.get("description", "")
            assigned_to_id = request.POST.get("assigned_to")
            user = current_manager

            assigned_to = None
            if assigned_to_id:
                try:
                    assigned_to = Manager.objects.get(id=assigned_to_id, user__company_id=company_id)
                except Manager.DoesNotExist:
                    pass

            leave = ManagerLeave.objects.create(
                user=user,
                assigned_to=assigned_to,
                startdate=startdate,
                enddate=enddate,
                leavetype=leavetype,
                reason=reason,
                description=description
            )

            try:
                from administration.email_notifications import send_leave_submission_notification
                send_leave_submission_notification(leave)
            except Exception as e:
                print(f"Error sending leave submission notification: {str(e)}")

            # Notify selected manager (manager-to-manager): dashboard + email with applicant contact
            if assigned_to:
                try:
                    applicant_name = f"{user.manager_first_name} {user.manager_last_name}".strip()
                    recipient_name = f"{assigned_to.manager_first_name} {assigned_to.manager_last_name}".strip()
                    ManagerNotification.objects.create(
                        user=assigned_to,
                        notifications=f"{applicant_name} applied for {str(leavetype).title()} leave ({startdate} to {enddate}).",
                    )
                    # Set notification flag for recipient manager
                    if getattr(assigned_to, "user", None):
                        assigned_to.user.new_notification = True
                        assigned_to.user.save()
                    # Email to recipient manager
                    send_email_notification(
                        subject=f"Manager Leave Request – Approve/Reject: {applicant_name}",
                        recipient_email=getattr(assigned_to, "manager_email", ""),
                        template_name="leave_submission_manager",
                        context={
                            "manager_name": recipient_name,
                            "employee_name": applicant_name,
                            "employee_id": getattr(user, "formatted_manager_id", "") or getattr(user, "manager_id", ""),
                            "employee_email": getattr(user, "manager_email", ""),
                            "employee_phone": getattr(user, "manager_phone", ""),
                            "leave_type": (str(leavetype) or "").title(),
                            "start_date": startdate,
                            "end_date": enddate,
                            "reason": reason or "N/A",
                            "description": description or "N/A",
                            "status": "pending",
                            "leave_days": leave.leave_days if hasattr(leave, "leave_days") else "",
                        },
                        recipient_name=recipient_name or "Manager",
                    )
                except Exception as e:
                    print("Error notifying selected manager (leave):", e)

            return redirect(f'/managers/mleaves/view/table/{company_id}/{company_staff_id}')

        dataset = {
            'leavetypes': ManagerLeave.objects.all(),
            'company_id': company_id,
            'company_staff_id': company_staff_id,
            'managers': managers_list,
        }
        return render(request, "managers/apply-leave.html", dataset)


def create_mresignation(request, company_id, company_staff_id):
    if company_id:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
        try:
            current_manager = company_staff.manager
        except Manager.DoesNotExist:
            current_manager = None

        # Managers in same company (for Reporting to dropdown), exclude self
        managers_qs = Manager.objects.filter(user__company_id=company_id).order_by('manager_first_name')
        if current_manager:
            managers_qs = managers_qs.exclude(id=current_manager.id)
        managers_list = list(managers_qs)

        if request.method == "POST":
            startdate = request.POST.get("startdate")
            reason = request.POST.get("reason")
            assigned_too_id = request.POST.get("assigned_too")
            if not current_manager:
                messages.error(request, 'Manager profile not found.')
                return render(request, "managers/apply-resignation.html", {
                    'company_id': company_id, 'company_staff_id': company_staff_id, 'managers': managers_list,
                })
            assigned_too = None
            if assigned_too_id:
                try:
                    assigned_too = Manager.objects.get(id=assigned_too_id, user__company_id=company_id)
                except Manager.DoesNotExist:
                    pass
            resign_obj = ManagerResign.objects.create(
                user=current_manager, startdate=startdate, reason=reason, assigned_too=assigned_too
            )
            # Notify selected manager (manager-to-manager): dashboard + email with applicant contact
            if assigned_too:
                try:
                    applicant_name = f"{current_manager.manager_first_name} {current_manager.manager_last_name}".strip()
                    recipient_name = f"{assigned_too.manager_first_name} {assigned_too.manager_last_name}".strip()
                    ManagerNotification.objects.create(
                        user=assigned_too,
                        notifications=f"{applicant_name} submitted a resignation request (Date: {startdate}).",
                    )
                    if getattr(assigned_too, "user", None):
                        assigned_too.user.new_notification = True
                        assigned_too.user.save()
                    send_email_notification(
                        subject=f"Manager Resignation Request – {applicant_name}",
                        recipient_email=getattr(assigned_too, "manager_email", ""),
                        template_name="resignation_submission_manager",
                        context={
                            "manager_name": recipient_name,
                            "employee_name": applicant_name,
                            "employee_id": getattr(current_manager, "formatted_manager_id", "") or getattr(current_manager, "manager_id", ""),
                            "employee_email": getattr(current_manager, "manager_email", ""),
                            "employee_phone": getattr(current_manager, "manager_phone", ""),
                            "resignation_date": startdate,
                            "reason": reason or "N/A",
                            "status": getattr(resign_obj, "status", "pending"),
                        },
                        recipient_name=recipient_name or "Manager",
                    )
                except Exception as e:
                    print("Error notifying selected manager (resignation):", e)
            messages.success(request, 'Resignation request submitted. Waiting for approval.')
            return redirect(f'/managers/create_mresignation/{company_id}/{company_staff_id}')

        return render(request, "managers/apply-resignation.html", {
            'company_id': company_id, 'company_staff_id': company_staff_id, 'managers': managers_list,
        })


def resign_list(request,company_id, company_staff_id):
    if company_id:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
        resign = Resign.objects.all_pending_resign().filter(assigned_too=company_staff.manager)
        return render(request, 'managers/employee-resignation.html',
                      {'resign_list': resign, 'title': 'resign list - pending','company_id':company_id, 'company_staff_id':company_staff_id})


def approve_resign(request,company_id, company_staff_id, id):
    if company_id:
        resign = get_object_or_404(Resign, id=id)
        # user = resign.user
        # employee = Employee.objects.filter(user=user)
        resign.approve_resign

        messages.error(request, 'Resignation successfully approved',
                       extra_tags='alert alert-success alert-dismissible show')
        return redirect(f'/managers/resign_list/{company_id}/{company_staff_id}')


def cancel_resign(request,company_id, company_staff_id, id):
    if company_id:
        resign = get_object_or_404(Resign, id=id)
        resign.resign_cancel

        messages.success(request, 'Resign is canceled', extra_tags='alert alert-success alert-dismissible show')
        return redirect(f'/managers/resign_list/{company_id}/{company_staff_id}')


def leave_list(request, company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        leave_obj_id = data.get('id', None)
        leave_obj = Leave.objects.get(pk=leave_obj_id)
        return JsonResponse(leave_obj.to_json())

    if company_id:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
        try:
            manager = company_staff.manager
            leaves = Leave.objects.all_pending_leaves().filter(user__employee_reports_to=manager)
        except Manager.DoesNotExist:
            leaves = Leave.objects.none()
        return render(request, 'managers/employee-leaves.html', {'leave_list': leaves, 'title': 'Leaves list - pending (your team)', 'company_id': company_id, 'company_staff_id': company_staff_id})



def leaves_approved_list(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        leave_obj_id = data.get('id', None)
        leave_obj = Leave.objects.get(pk=leave_obj_id)
        return JsonResponse(leave_obj.to_json())

    if company_id:
        leaves = Leave.objects.all_approved_leaves().filter(user__user__company_id=company_id)  # approved leaves -> calling model manager method
        return render(request, 'managers/approved-leaves.html',
                      {'leave_list': leaves, 'title': 'approved leave list','company_id':company_id, 'company_staff_id':company_staff_id})


def leaves_view(request, id):
    if not (request.user.is_authenticated):
        return redirect('/')

    leave = get_object_or_404(Leave, id=id)
    print(leave.user)

    return render(request, 'managers/leave_detail_view.html', {'leave': leave,
                                                                     'title': '{0}-{1} leave'.format(
                                                                         leave.user.username,
                                                                         leave.status)})


def approve_leave(request,company_id, company_staff_id, id):
    if company_id:

        leave = get_object_or_404(Leave, id=id)

        leave.approve_leave

        messages.error(request, 'Leave successfully approved',
                       extra_tags='alert alert-success alert-dismissible show')
        return redirect(f'/managers/leave_list/{company_id}/{company_staff_id}')


def cancel_leaves_list(request,company_id, company_staff_id):
    if company_id:
        leaves = Leave.objects.all_cancel_leaves().filter(user__user__company_id=company_id)
        return render(request, 'managers/cancelled-leaves.html',
                      {'leave_list_cancel': leaves, 'title': 'Cancel leave list','company_id':company_id, 'company_staff_id':company_staff_id})


def unapprove_leave(request, id):

    leave = get_object_or_404(Leave, id=id)
    leave.unapprove_leave
    return redirect('leave_list')  # redirect to unapproved list


def cancel_leave(request,company_id, company_staff_id, id):
    if company_id:
        leave = get_object_or_404(Leave, id=id)
        leave.leaves_cancel

        messages.success(request, 'Leave is canceled', extra_tags='alert alert-success alert-dismissible show')
        return redirect(f'/managers/leave_list/{company_id}/{company_staff_id}')


# Current section -> here
def uncancel_leave(request, id):

    leave = get_object_or_404(Leave, id=id)
    leave.status = 'pending'
    leave.is_approved = False
    leave.save()
    messages.success(request, 'Leave is uncanceled,now in pending list',
                     extra_tags='alert alert-success alert-dismissible show')
    return redirect('leave_list')  # work on redirecting to instance leave - detail view


def leave_rejected_list(request):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        leave_obj_id = data.get('id', None)
        leave_obj = Leave.objects.get(pk=leave_obj_id)
        return JsonResponse(leave_obj.to_json())

    dataset = dict()
    leave = Leave.objects.all_rejected_leaves()

    dataset['leave_list_rejected'] = leave
    return render(request, 'managers/rejected-leaves.html', dataset)


def reject_leave(request,company_id, company_staff_id, id):
    if company_id:
        dataset = dict()
        leave = get_object_or_404(Leave, id=id)
        leave.reject_leave
        messages.success(request, 'Leave is rejected', extra_tags='alert alert-success alert-dismissible show')
        return redirect(f'/managers/leave_list/{company_id}/{company_staff_id}')


def unreject_leave(request, id):
    leave = get_object_or_404(Leave, id=id)
    leave.status = 'pending'
    leave.is_approved = False
    leave.save()
    messages.success(request, 'Leave is now in pending list ', extra_tags='alert alert-success alert-dismissible show')

    return redirect('leavesrejected')


def All_document_Views(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        document_obj_id = data.get('id', None)
        document_obj = ManagerPost.objects.get(pk=document_obj_id)
        return JsonResponse(document_obj.to_json())

    # Old Code
    if company_id:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
        document_list = ManagerPost.objects.filter(user=company_staff.manager)
        return render(request, 'managers/view_documents.html', {'document_list': document_list,'company_id':company_id, 'company_staff_id':company_staff_id})


def ChangePassword(request,company_id, company_staff_id):
    if company_id:
        if request.method == "POST":
            password = request.POST["password"]
            new_pas = request.POST["npwd"]

            user = CompanyStaff.objects.get(id=company_staff_id)
            un = user.email
            # check = user.check_password(current)
            check=check_password(password, user.password)
            if check == True:
                user.password=make_password(new_pas)
                user.save()
                messages.success(request, 'Password changed Successfully')
                return redirect('/')

        return render(request,"managers/change_password.html",{'company_id':company_id, 'company_staff_id':company_staff_id})

def MyNotification(request,company_id, company_staff_id):
    context ={}

    company_staff = CompanyStaff.objects.get(id=company_staff_id)

    queryset = ManagerNotification.objects.filter(user=company_staff.manager)
    context['notification']= queryset
    context['company_id']= company_id
    context['company_staff_id']= company_staff_id
    queryset.filter(is_read=False).update(is_read=True)
    company_staff.new_notification = False
    company_staff.save()
    request.session["new_notification"] = company_staff.new_notification
    return render(request, 'managers/mynotification.html', context)


def delete_manager_notification(request, company_id, company_staff_id, notification_id):
    """
    Manager can remove their own notification from the list.
    """
    if request.method != "POST":
        return redirect('mynotification', company_id=company_id, company_staff_id=company_staff_id)

    company_staff = CompanyStaff.objects.get(id=company_staff_id)
    manager = getattr(company_staff, "manager", None)
    if not manager:
        messages.error(request, "Manager profile not found.")
        return redirect('mynotification', company_id=company_id, company_staff_id=company_staff_id)

    notif = get_object_or_404(ManagerNotification, id=notification_id, user=manager)
    notif.delete()

    unread_count = ManagerNotification.objects.filter(user=manager, is_read=False).count()
    company_staff.new_notification = unread_count > 0
    company_staff.save()
    request.session["new_notification"] = company_staff.new_notification

    messages.success(request, "Notification removed.")
    return redirect('mynotification', company_id=company_id, company_staff_id=company_staff_id)


def Employeenotifications(request,company_id, company_staff_id):
    if company_id:
        if request.method == "POST":
            notifications  = request.POST.get("notifications")
            assign_id = request.POST.get("employee_id")
            assigned_to = Employee.objects.get(id =assign_id)
            # company_staff = CompanyStaff.objects.get(id=company_staff_id)
            # user = company_staff
            # emp = Employee.objects.get(user = user)

            # 1) Create in-app notification
            EmployeeNotification.objects.create(notifications=notifications, user=assigned_to)

            # 2) Mark employee's CompanyStaff as having a new notification
            user = assigned_to.user
            user.new_notification = True
            user.save()
            request.session["new_notification"] = user.new_notification

            # 3) Send email notification to employee
            try:
                subject = "New notification from your manager"
                recipient_email = assigned_to.employee_email
                context = {
                    "employee_name": f"{assigned_to.employee_first_name} {assigned_to.employee_last_name}",
                    "manager_name": request.user.email if hasattr(request, "user") else "",
                    "message": notifications,
                }
                # Uses generic HTML email helper; falls back to plain text if needed
                send_email_notification(
                    subject=subject,
                    recipient_email=recipient_email,
                    template_name="employee_notification",
                    context=context,
                    recipient_name=assigned_to.employee_first_name or "Employee",
                )
            except Exception as e:
                # Fail silently for email so UI flow is not broken
                print("Error sending employee notification email:", e)

            return redirect(f'/managers/mnotification/{company_id}/{company_staff_id}')

        else:
            return render(request,"managers/employeenotification.html",{'assigned':Employee.objects.filter(user__company__id=company_id),'company_id':company_id, 'company_staff_id':company_staff_id})