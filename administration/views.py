import json

from django.contrib.auth import authenticate, login
from django.contrib.auth import update_session_auth_hash
from django.contrib.auth.decorators import login_required
from django.contrib.auth.mixins import LoginRequiredMixin, UserPassesTestMixin
from django.contrib.auth.models import Group
from django.core.mail import EmailMessage
from django.http.response import HttpResponseRedirect
from django.shortcuts import render, HttpResponse, redirect, get_object_or_404
from django.views import generic
from django.views.decorators.csrf import csrf_exempt
from django.views.generic import TemplateView, CreateView, UpdateView, DeleteView
from django.views.generic.base import View
import employee
from manager_leave.models import ManagerLeave, BalanceLeave
from employee.models import Employee
from manager_resign.models import ManagerResign
from employee.models import Employee 
from manageregularization.models import MRegularization
from account.utils import custom_login_required
from .forms import AttendanceForm, EmployeeForm
from leave.models import Leave
from managers.models import Manager, ManagerAttendance, ManagerPost
from regularization.models import Regularization
from resign.models import Resign
from .models import Client, Lead, Task, ManagerProject, notification, holiday, Asign, ManagerNotification, EmailNotification
from .email_service import fetch_emails_from_gmail
from django.urls import reverse
from django.contrib import messages
from django.utils.decorators import method_decorator
from employee.models import Employee, role_choices, Attendance, Post, Department, Entries
from django.db import IntegrityError, connection, transaction
from account.models import User, CompanyStaff, Company
import sweetify
from datetime import datetime
import os
import subprocess
from django.http.response import JsonResponse
from django.views.generic import DetailView, ListView
from django.db import models
from django.db.models import Q
from django.contrib.auth.hashers import make_password
from django.contrib.auth.hashers import  check_password
from django.views.decorators.csrf import csrf_exempt
from django.http import JsonResponse
from.forms import *
from biometric.forms import BiometricDeviceForm
from biometric.models import BiometricDevice, BiometricEventLog
from django.conf import settings
import socket
#---------------------------------------------Add All Document--------------------------------


def _biometric_base_url():
    return getattr(settings, 'PUBLIC_API_BASE_URL', 'http://127.0.0.1:8000').rstrip('/')


def _powershell_quote(value):
    return "'" + str(value).replace("'", "''") + "'"


@custom_login_required
def biometric_machines(request, company_id, company_staff_id):
    company = get_object_or_404(Company, id=company_id)
    edit_id = request.GET.get('edit')
    device = None
    if edit_id:
        device = get_object_or_404(BiometricDevice, id=edit_id, company=company)

    if request.method == 'POST':
        action = request.POST.get('action', 'save')

        if action == 'delete':
            device_id = request.POST.get('device_pk')
            device_to_delete = get_object_or_404(BiometricDevice, id=device_id, company=company)
            device_to_delete.delete()
            messages.success(request, 'Biometric machine deleted successfully.')
            return redirect('biometric_machines', company_id=company_id, company_staff_id=company_staff_id)

        if action == 'test':
            device_id = request.POST.get('device_pk')
            device_to_test = get_object_or_404(BiometricDevice, id=device_id, company=company)
            ok = False
            message = 'This integration mode waits for punches from the machine.'
            if device_to_test.integration_mode == 'bridge_pull':
                if not device_to_test.ip_address:
                    message = 'Device IP address is required for bridge pull testing.'
                else:
                    try:
                        with socket.create_connection((str(device_to_test.ip_address), int(device_to_test.port)), timeout=5):
                            ok = True
                            message = f'Connection opened to {device_to_test.ip_address}:{device_to_test.port}.'
                    except OSError as exc:
                        message = str(exc)
            elif device_to_test.integration_mode == 'tcp_xml_push':
                ok = device_to_test.is_online
                message = 'Device is active (recent TCP/XML packets received).' if ok else f'No recent TCP/XML data received. Run listener on port {device_to_test.port}.'
            elif device_to_test.integration_mode == 'http_push':
                ok = device_to_test.is_online
                message = 'Device is active (recent HTTP pushes received).' if ok else f'Waiting for HTTP push from device to: {_biometric_base_url()}/api/attendance/http-push/'
            device_to_test.mark_test_result(ok, message)
            messages.success(request, message) if ok else messages.warning(request, message)
            return redirect('biometric_machines', company_id=company_id, company_staff_id=company_staff_id)

        if action == 'open_logs':
            device_id = request.POST.get('device_pk')
            device_for_logs = get_object_or_404(BiometricDevice, id=device_id, company=company)
            is_tcp_xml = device_for_logs.integration_mode == 'tcp_xml_push'
            script_name = 'open_biometric_tcp_logs.ps1' if is_tcp_xml else 'open_biometric_bridge_logs.ps1'
            script_path = os.path.join(settings.BASE_DIR, script_name)
            if not os.path.exists(script_path):
                messages.error(request, 'PowerShell biometric log launcher was not found.')
                return redirect('biometric_machines', company_id=company_id, company_staff_id=company_staff_id)

            server_url = f'{_biometric_base_url()}/api/attendance/biometric-punch/'
            port = int(device_for_logs.port or 4370)
            powershell_exe = r'C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe'
            if is_tcp_xml:
                bridge_args = [
                    '-NoExit',
                    '-NoProfile',
                    '-ExecutionPolicy',
                    'Bypass',
                    '-File',
                    script_path,
                    '-HostAddress',
                    '0.0.0.0',
                    '-Port',
                    str(port),
                ]
            else:
                bridge_args = [
                    '-NoExit',
                    '-NoProfile',
                    '-ExecutionPolicy',
                    'Bypass',
                    '-File',
                    script_path,
                    '-DeviceIp',
                    str(device_for_logs.ip_address or ''),
                    '-DevicePort',
                    str(port),
                    '-DeviceId',
                    str(device_for_logs.device_id),
                    '-MachineNumber',
                    str(device_for_logs.machine_number or 1),
                    '-ServerUrl',
                    device_for_logs.get_effective_server_url(server_url),
                    '-DevicePassword',
                    str(device_for_logs.device_password or 0),
                ]
            if os.name == 'nt':
                subprocess.Popen(
                    [powershell_exe, *bridge_args],
                    cwd=settings.BASE_DIR,
                    stdin=subprocess.DEVNULL,
                    stdout=subprocess.DEVNULL,
                    stderr=subprocess.DEVNULL,
                    creationflags=subprocess.CREATE_NEW_CONSOLE | subprocess.CREATE_NEW_PROCESS_GROUP,
                    close_fds=True,
                )
            else:
                subprocess.Popen(
                    [powershell_exe, *bridge_args],
                    cwd=settings.BASE_DIR,
                    stdin=subprocess.DEVNULL,
                    stdout=subprocess.DEVNULL,
                    stderr=subprocess.DEVNULL,
                    close_fds=True,
                )

            if is_tcp_xml:
                messages.success(request, f'Opened TCP/XML biometric listener on port {port} in PowerShell.')
            elif device_for_logs.integration_mode == 'bridge_pull' and port != 4370:
                messages.warning(request, 'Opened live bridge logs. Note: bridge-pull biometric machines usually use port 4370; this machine is configured for a different port.')
            else:
                messages.success(request, 'Opened live biometric bridge logs in PowerShell.')
            return redirect('biometric_machines', company_id=company_id, company_staff_id=company_staff_id)

        device_pk = request.POST.get('device_pk')
        instance = get_object_or_404(BiometricDevice, id=device_pk, company=company) if device_pk else None
        form = BiometricDeviceForm(request.POST, instance=instance)
        if form.is_valid():
            saved = form.save(commit=False)
            saved.company = company
            saved.save()
            messages.success(request, 'Biometric machine saved successfully.')
            return redirect('biometric_machines', company_id=company_id, company_staff_id=company_staff_id)
        messages.error(request, 'Please correct the highlighted fields.')
    else:
        form = BiometricDeviceForm(instance=device)

    devices = BiometricDevice.objects.filter(company=company).order_by('name', 'id')
    recent_events = BiometricEventLog.objects.filter(company=company).select_related('device', 'employee', 'manager')[:15]
    context = {
        'form': form,
        'edit_device': device,
        'devices': devices,
        'recent_events': recent_events,
        'company_id': company_id,
        'company_staff_id': company_staff_id,
        'bridge_url': f'{_biometric_base_url()}/api/attendance/biometric-punch/',
        'manual_url': f'{_biometric_base_url()}/api/attendance/manual-punch/',
        'http_push_url': f'{_biometric_base_url()}/api/attendance/http-push/',
        'heartbeat_url': f'{_biometric_base_url()}/api/attendance/biometric-heartbeat/',
        'tcp_default_port': getattr(settings, 'BIOMETRIC_TCP_PORT', 5005),
    }
    return render(request, 'administration/biometric-machines.html', context)




# -------------------------------------all employee Profile Name --------------------------------
@login_required
def P_name(request):
    try:
        employee = Employee.objects.get(user=request.user)
    except Employee.DoesNotExist:
        employee = None

    return render(request, "administration/index.html", {"employee": employee})


# -------------------------------------all employee for admin--------------------------------
@custom_login_required
def Register_Employee_View(request,company_id, company_staff_id):
    if request.method == "POST":
        try:
            employee_first_name = request.POST.get('employee_first_name', '').strip()
            employee_last_name = request.POST.get('employee_last_name', '').strip()
            employee_email = request.POST.get('employee_email', '').strip()
            employee_joining_date_str = request.POST.get('employee_joining_date', '').strip()
            employee_password = request.POST.get('employee_password', '')
            employee_confirm_password = request.POST.get('employee_confirm_password', '')
            employee_id = request.POST.get('employee_id', '').strip()
            employee_phone = request.POST.get('employee_phone', '').strip()
            employee_salary = request.POST.get('employee_salary', '').strip()
            department_id = request.POST.get("id")
            assign_id = request.POST.get("manager_id")

            # Auto-generate employee_id if not provided or not in correct format
            if not employee_id or not employee_id.startswith('EIC-'):
                import re
                existing_employees = Employee.objects.filter(user__company__id=company_id)
                max_num = 0
                for emp in existing_employees:
                    if emp.employee_id and emp.employee_id.startswith('EIC-'):
                        try:
                            num_str = emp.employee_id.replace('EIC-', '').strip()
                            num = int(num_str)
                            if num > max_num:
                                max_num = num
                        except (ValueError, AttributeError):
                            continue
                employee_id = f"EIC-{max_num + 1:03d}"

            employee_biometric_id = request.POST.get('biometric_id', '').strip() or None
            if employee_biometric_id and Employee.objects.filter(biometric_id=employee_biometric_id).exists():
                messages.error(request, f"Biometric ID '{employee_biometric_id}' is already assigned to another employee!")
                return redirect(f'/administration/all_employee/{company_id}/{company_staff_id}')

            if not all([employee_first_name, employee_last_name, employee_email, employee_joining_date_str, 
                       employee_password, employee_confirm_password, employee_id, department_id, assign_id]):
                messages.error(request, "All required fields must be filled.")
                return redirect(f'/administration/all_employee/{company_id}/{company_staff_id}')

            try:
                # HTML5 date input sends YYYY-MM-DD; also support DD/MM/YYYY
                if '-' in employee_joining_date_str and len(employee_joining_date_str) == 10:
                    employee_joining_date = datetime.strptime(employee_joining_date_str, '%Y-%m-%d').date()
                else:
                    employee_joining_date = datetime.strptime(employee_joining_date_str, '%d/%m/%Y').date()
            except ValueError:
                messages.error(request, "Invalid date format. Use calendar or DD/MM/YYYY.")
                return redirect(f'/administration/all_employee/{company_id}/{company_staff_id}')

            try:
                employee_department = Department.objects.get(id=department_id, company_id=company_id)
            except Department.DoesNotExist:
                messages.error(request, "Selected department not found.")
                return redirect(f'/administration/all_employee/{company_id}/{company_staff_id}')

            try:
                employee_reports_to = Manager.objects.get(id=assign_id, user__company_id=company_id)
            except Manager.DoesNotExist:
                messages.error(request, "Selected manager not found.")
                return redirect(f'/administration/all_employee/{company_id}/{company_staff_id}')

            if company_id:
                try:
                    if (employee_password == employee_confirm_password):
                        user = CompanyStaff.objects.create(email=employee_email, password=employee_password,company_id=company_id)
                        user.password = make_password(user.password)
                        user.full_name = employee_first_name + ' ' + employee_last_name
                        user.is_active = True
                        user.is_employee = True
                        user.save()
                        register_employee = Employee(user=user, employee_salary=employee_salary,
                                                     employee_first_name=employee_first_name,
                                                     employee_last_name=employee_last_name, employee_email=employee_email,
                                                     employee_joining_date=employee_joining_date,
                                                     employee_id=employee_id,
                                                     employee_phone=employee_phone,
                                                     employee_department=employee_department,
                                                     employee_reports_to=employee_reports_to,
                                                     biometric_id=employee_biometric_id,
                                                     )

                        register_employee.save()
                        
                        # Send email notification for new user
                        try:
                            from administration.email_notifications import send_new_user_notification
                            send_new_user_notification(user, user_type='employee', password=employee_password)
                        except Exception as e:
                            print(f"Error sending new user notification: {str(e)}")
                        
                        messages.success(request, 'Employee Registered Successfully!')

                    else:
                        messages.error(request, "Confirm password and password do not match!")
                except IntegrityError as e:
                    messages.error(request, "Email Already Registered!")
                except Exception as e:
                    messages.error(request, f"Error registering employee: {str(e)}")

        except Exception as e:
            messages.error(request, f"Error processing form: {str(e)}")

        return redirect(f'/administration/all_employee/{company_id}/{company_staff_id}')

    # For GET request, redirect to All_Employee_View which properly handles the context
    return redirect(f'/administration/all_employee/{company_id}/{company_staff_id}')


@custom_login_required
def All_Employee_View(request, company_id, company_staff_id):
    if request.method == "POST":
        try:
            data = json.loads(request.body.decode('utf-8'))
            employee_obj_id = data.get('employee_id', None)
            if not employee_obj_id:
                return JsonResponse({'error': 'Employee ID is required'}, status=400)
            employee_obj = Employee.objects.get(pk=employee_obj_id, user__company_id=company_id)
            return JsonResponse(employee_obj.to_json())
        except Employee.DoesNotExist:
            return JsonResponse({'error': 'Employee not found'}, status=404)
        except json.JSONDecodeError:
            return JsonResponse({'error': 'Invalid JSON data'}, status=400)
        except Exception as e:
            return JsonResponse({'error': str(e)}, status=500)

    else:
        AllEmployee = Employee.objects.filter(user__company__id = company_id)
        departments = Department.objects.filter(company__id=company_id)
        reports_to = Manager.objects.filter(user__company__id=company_id)
        
        # Calculate next employee_id based on existing employee_id values
        import re
        max_num = 0
        if AllEmployee:
            for emp in AllEmployee:
                if emp.employee_id and emp.employee_id.startswith('EIC-'):
                    try:
                        # Extract number from EIC-XXX format
                        num_str = emp.employee_id.replace('EIC-', '').strip()
                        num = int(num_str)
                        if num > max_num:
                            max_num = num
                    except (ValueError, AttributeError):
                        continue
        
        # Next employee_id will be max_num + 1, formatted as EIC-001, EIC-002, etc.
        next_employee_id = max_num + 1
        
        return render(request, 'administration/all-employees.html',
                    {'Employees': AllEmployee, 'max_employee_id': next_employee_id, 'departments': departments,
                    'reports_to': reports_to, 'company_id' : company_id, 'company_staff_id':company_staff_id
                    })


def All_Employee_List_View(request):
    AllEmployee = Employee.objects.filter(employee_status="Active")
    return render(request, 'administration/employees_list.html', {'Employees': AllEmployee})


@custom_login_required
def Employee_Edit_View(request, company_id,company_staff_id):
    if company_id:
        if request.method == "GET":
            try:
                employee_id = request.GET.get('employee_id')
                if not employee_id:
                    return JsonResponse({'error': 'Employee ID is required'}, status=400)
                employee_obj = Employee.objects.get(pk=employee_id, user__company_id=company_id)
                return JsonResponse(employee_obj.to_json())
            except Employee.DoesNotExist:
                return JsonResponse({'error': 'Employee not found'}, status=404)
            except Exception as e:
                return JsonResponse({'error': str(e)}, status=500)

        elif request.method == "POST":
            employee_models_fields_list = [f.name for f in Employee._meta.get_fields()]
            employee_models_fields_dict = {}
            employee_obj_id = request.POST.get('employee_id')
            employee_obj = Employee.objects.filter(pk=employee_obj_id)
            
            # Get old manager before update for email notification
            old_manager = None
            new_manager = None
            employee_instance = None
            if employee_obj.exists():
                employee_instance = employee_obj.first()
                old_manager = employee_instance.employee_reports_to

            for key, value in request.POST.items():
                if key in employee_models_fields_list and key != 'employee_id' and key != 'id':
                    if key == 'biometric_id':
                        employee_models_fields_dict[key] = value.strip() or None
                    elif value is not None and len(value) != 0:
                        employee_models_fields_dict.setdefault(key, value)
            
            # Check if manager is being changed
            if 'employee_reports_to' in employee_models_fields_dict:
                try:
                    from managers.models import Manager
                    new_manager_id = employee_models_fields_dict['employee_reports_to']
                    new_manager = Manager.objects.get(id=new_manager_id) if new_manager_id else None
                except:
                    new_manager = None
            
            employee_obj.update(**employee_models_fields_dict)
            emp_id = request.POST.get('employee_id')

            if 'employee_image' in request.FILES:
                employee_obj = employee_obj.first()
                employee_obj.employee_image = request.FILES['employee_image']
                employee_obj.save()
            
            # Send email notification if manager changed
            if employee_instance and old_manager != new_manager and (old_manager or new_manager):
                try:
                    from administration.email_notifications import send_manager_change_notification
                    # Refresh employee instance to get updated manager
                    employee_instance.refresh_from_db()
                    send_manager_change_notification(employee_instance, old_manager=old_manager, new_manager=employee_instance.employee_reports_to)
                except Exception as e:
                    print(f"Error sending manager change notification: {str(e)}")

            print('Employee id is-')
            print(emp_id)
            return redirect(f'/administration/all_employee/{company_id}/{company_staff_id}')


def Remove_Employee_List(request, id):
    try:
        employees = Employee.objects.get(id=id)
        try:
            User.objects.get(id=employees.user.id).delete()
        except User.DoesNotExist:
            pass  # User already deleted or doesn't exist
        employees.delete()
        messages.success(request, "deleted successfully")
    except Employee.DoesNotExist:
        messages.error(request, "Employee not found.")
    except Exception as e:
        messages.error(request, f"Error deleting employee: {str(e)}")
    return HttpResponseRedirect('/administration/all_employee_list')


def Remove_Employee(request, id,company_id,company_staff_id):
    try:
        employees = Employee.objects.get(id=id, user__company_id=company_id)
        employee_user_id = employees.user.id if employees.user else None
        
        # Only delete CompanyStaff if it exists and is not the current admin's CompanyStaff
        if employee_user_id and employee_user_id != company_staff_id:
            try:
                employee_company_staff = CompanyStaff.objects.get(id=employee_user_id)
                employee_company_staff.delete()
            except CompanyStaff.DoesNotExist:
                pass  # CompanyStaff already deleted or doesn't exist
        
        # Delete the employee record
        employees.delete()
        messages.success(request, "Employee deleted successfully")
    except Employee.DoesNotExist:
        messages.error(request, "Employee not found.")
    except Exception as e:
        messages.error(request, f"Error deleting employee: {str(e)}")
    
    # Verify company_staff_id exists before redirecting
    try:
        CompanyStaff.objects.get(id=company_staff_id, company_id=company_id)
        return redirect(f'/administration/all_employee/{company_id}/{company_staff_id}')
    except CompanyStaff.DoesNotExist:
        # If admin CompanyStaff doesn't exist, redirect to a safe page
        messages.error(request, "Session expired. Please login again.")
        return redirect('/')


@custom_login_required
def Update_Employees_View(request, company_id, id):
    try:
        update_info = Employee.objects.get(id=id, user__company_id=company_id)
        return render(request, 'administration/all-employees.html', {'update_info': update_info})
    except Employee.DoesNotExist:
        messages.error(request, 'Employee not found.')
        return redirect(f'/administration/all_employee/{company_id}/{request.session.get("company_staff_id", 0)}')
    except Exception as e:
        messages.error(request, f'Error loading employee: {str(e)}')
        return redirect(f'/administration/all_employee/{company_id}/{request.session.get("company_staff_id", 0)}')


@custom_login_required
def Register_manager_View(request,company_id, company_staff_id):
    if request.method == "POST":
        try:
            manager_first_name = request.POST.get('manager_first_name', '').strip()
            manager_last_name = request.POST.get('manager_last_name', '').strip()
            manager_email = request.POST.get('manager_email', '').strip()
            manager_joining_date_str = request.POST.get('manager_joining_date', '').strip()
            manager_password = request.POST.get('manager_password', '')
            manager_confirm_password = request.POST.get('manager_confirm_password', '')
            manager_id = request.POST.get('manager_id', '').strip()
            manager_phone = request.POST.get('manager_phone', '').strip()
            manager_salary = request.POST.get('manager_salary', '').strip()
            department_id = request.POST.get("id")

            manager_biometric_id = request.POST.get('biometric_id', '').strip() or None
            if manager_biometric_id and Manager.objects.filter(biometric_id=manager_biometric_id).exists():
                messages.error(request, f"Biometric ID '{manager_biometric_id}' is already assigned to another manager!")
                return redirect(f'/administration/all_manager/{company_id}/{company_staff_id}')

            if not all([manager_first_name, manager_last_name, manager_email, manager_joining_date_str, 
                       manager_password, manager_confirm_password, manager_id, department_id]):
                messages.error(request, "All required fields must be filled.")
                return redirect(f'/administration/all_manager/{company_id}/{company_staff_id}')

            try:
                # HTML5 date input sends YYYY-MM-DD; also support DD/MM/YYYY
                if '-' in manager_joining_date_str and len(manager_joining_date_str) == 10:
                    manager_joining_date = datetime.strptime(manager_joining_date_str, '%Y-%m-%d').date()
                else:
                    manager_joining_date = datetime.strptime(manager_joining_date_str, '%d/%m/%Y').date()
            except ValueError:
                messages.error(request, "Invalid date format. Use calendar or DD/MM/YYYY.")
                return redirect(f'/administration/all_manager/{company_id}/{company_staff_id}')

            try:
                manager_department = Department.objects.get(id=department_id, company_id=company_id)
            except Department.DoesNotExist:
                messages.error(request, "Selected department not found.")
                return redirect(f'/administration/all_manager/{company_id}/{company_staff_id}')

            if company_id:
                try:
                    if (manager_password == manager_confirm_password):
                        user = CompanyStaff.objects.create(email=manager_email, password=manager_password,company_id=company_id)
                        user.password = make_password(user.password)

                        user.full_name = manager_first_name + ' ' + manager_last_name
                        user.is_active = True
                        user.is_manager = True

                        user.save()
                        register_manager = Manager(user=user, manager_salary=manager_salary,
                                                   manager_first_name=manager_first_name,
                                                   manager_last_name=manager_last_name, manager_email=manager_email,
                                                   manager_joining_date=manager_joining_date,
                                                   manager_department=manager_department, manager_id=manager_id,
                                                   manager_phone=manager_phone,
                                                   biometric_id=manager_biometric_id)
                        register_manager.save()
                        
                        # Send email notification for new manager
                        try:
                            from administration.email_notifications import send_new_user_notification
                            send_new_user_notification(user, user_type='manager', password=manager_password)
                        except Exception as e:
                            print(f"Error sending new manager notification: {str(e)}")
                        
                        messages.success(request, 'Manager Registered Successfully!')

                    else:
                        messages.error(request, "Confirm password and password do not match!")
                except IntegrityError as e:
                    messages.error(request, "Email Already Registered!")
                except Exception as e:
                    messages.error(request, f"Error registering manager: {str(e)}")

        except Exception as e:
            messages.error(request, f"Error processing form: {str(e)}")

        return redirect(f'/administration/all_manager/{company_id}/{company_staff_id}')
    else:
        groups = Group.objects.all()
        return render(request, 'administration/all-manager.html',{'departments':Department.objects.filter(company__id=company_id)},{'groups': groups,'company_id':company_id, 'company_staff_id':company_staff_id})


@custom_login_required
def All_manager_View(request, company_id, company_staff_id):
    if request.method == "POST":
        try:
            data = json.loads(request.body.decode('utf-8'))
            manager_obj_id = data.get('manager_id', None)
            if not manager_obj_id:
                return JsonResponse({'error': 'Manager ID is required'}, status=400)
            manager_obj = Manager.objects.get(pk=manager_obj_id, user__company_id=company_id)
            return JsonResponse(manager_obj.to_json())
        except Manager.DoesNotExist:
            return JsonResponse({'error': 'Manager not found'}, status=404)
        except json.JSONDecodeError:
            return JsonResponse({'error': 'Invalid JSON data'}, status=400)
        except Exception as e:
            return JsonResponse({'error': str(e)}, status=500)

    # company_id = request.session.get('company')
    if company_id:
        manager = Manager.objects.filter(user__company__id=company_id)
        if manager:
            max_manager_id = Manager.objects.filter(user__company__id=company_id).order_by("-id")[0].id + 1
            departments = Department.objects.filter(company__id=company_id).only('department_name')
            return render(request, 'administration/all-manager.html',
                          {'manager': manager, 'max_manager_id': max_manager_id, 'role_choices': role_choices,'departments': departments, 'company_id':company_id, 'company_staff_id':company_staff_id})
        else:
            max_manager_id = "NA"
            departments = Department.objects.filter(company__id=company_id).only('department_name')
            return render(request, 'administration/all-manager.html',
                          {'manager': manager, 'max_manager_id': max_manager_id, 'role_choices': role_choices,'departments': departments, 'company_id':company_id, 'company_staff_id':company_staff_id})


def manager_Edit_View(request,company_id, company_staff_id):
    if company_id:
        if request.method == "GET":
            try:
                manager_id = request.GET.get('manager_id')
                if not manager_id:
                    return JsonResponse({'error': 'Manager ID is required'}, status=400)
                manager_obj = Manager.objects.get(pk=manager_id, user__company_id=company_id)
                return JsonResponse(manager_obj.to_json())
            except Manager.DoesNotExist:
                return JsonResponse({'error': 'Manager not found'}, status=404)
            except Exception as e:
                return JsonResponse({'error': str(e)}, status=500)

        elif request.method == "POST":
            manager_models_fields_list = [f.name for f in Manager._meta.get_fields()]
            manager_models_fields_dict = {}
            manager_obj_id = request.POST.get('manager_id')
            manager_obj = Manager.objects.filter(pk=manager_obj_id)

            for key, value in request.POST.items():
                if key in manager_models_fields_list and key != 'manager_id' and key != 'id':
                    if key == 'biometric_id':
                        manager_models_fields_dict[key] = value.strip() or None
                    elif value is not None and len(value) != 0:
                        manager_models_fields_dict.setdefault(key, value)
            manager_obj.update(**manager_models_fields_dict)
            emp_id = request.POST.get('manager_id')

            if 'manager_image' in request.FILES:
                manager_obj = manager_obj.first()
                manager_obj.manager_image = request.FILES['manager_image']
                manager_obj.save()

            print('manager id is-')
            print(emp_id)
            return redirect(f'/administration/all_manager/{company_id}/{company_staff_id}')


def All_manager_List_View(request):
    manager = Manager.objects.filter(manager_status="Active")
    return render(request, 'administration/all-manager-list.html', {'manager': manager})


def _delete_manager_and_staff(cursor, manager):
    """Unlink all references to this manager, then delete Manager, then CompanyStaff.
    Order matters: Manager has FK to CompanyStaff, so delete Manager before CompanyStaff.
    """
    mid, cid = manager.id, manager.user_id
    # 1) Unlink / nullify all FKs pointing to this manager
    cursor.execute(
        "UPDATE employee_employee SET employee_reports_to_id = NULL WHERE employee_reports_to_id = %s",
        [mid],
    )
    cursor.execute("UPDATE leave_leave SET manager_id = NULL WHERE manager_id = %s", [mid])
    cursor.execute("UPDATE resign_resign SET assigned_too_id = NULL WHERE assigned_too_id = %s", [mid])
    cursor.execute("UPDATE regularization_regularization SET r_assigned_to_id = NULL WHERE r_assigned_to_id = %s", [mid])
    cursor.execute(
        "UPDATE manager_leave_managerleave SET user_id = NULL, assigned_to_id = NULL WHERE user_id = %s OR assigned_to_id = %s",
        [mid, mid],
    )
    cursor.execute("UPDATE manager_leave_balanceleave SET user_id = NULL WHERE user_id = %s", [mid])
    cursor.execute(
        "UPDATE manager_resign_managerresign SET user_id = NULL, assigned_too_id = NULL WHERE user_id = %s OR assigned_too_id = %s",
        [mid, mid],
    )
    cursor.execute("UPDATE manageregularization_mregularization SET user_id = NULL WHERE user_id = %s", [mid])
    # 2) Delete rows that reference manager
    cursor.execute("DELETE FROM administration_asign WHERE assigned_to_id = %s", [mid])
    cursor.execute("DELETE FROM managers_managerattendance WHERE manager_id = %s", [mid])
    cursor.execute("DELETE FROM managerpayroll_salary WHERE manager_id = %s", [mid])
    cursor.execute("DELETE FROM managers_managerpost WHERE user_id = %s", [mid])
    # 3) Delete Manager first (it references CompanyStaff)
    cursor.execute("DELETE FROM managers_manager WHERE id = %s", [mid])
    # 4) Then delete CompanyStaff
    cursor.execute("DELETE FROM account_companystaff WHERE id = %s", [cid])


def Remove_manager_List(request, id,company_id, company_staff_id):
    if company_id:
        try:
            manager = Manager.objects.get(id=id, user__company_id=company_id)
            try:
                with transaction.atomic():
                    with connection.cursor() as cursor:
                        _delete_manager_and_staff(cursor, manager)
                messages.success(request, "Manager deleted successfully")
            except Exception as e:
                messages.error(request, f"Error deleting manager: {str(e)}")
        except Manager.DoesNotExist:
            messages.error(request, "Manager not found.")
        except Exception as e:
            messages.error(request, f"Error deleting manager: {str(e)}")
    return HttpResponseRedirect('/administration/all_manager_list')


def Remove_manager(request, id,company_id,company_staff_id):
    try:
        managers = Manager.objects.get(id=id, user__company_id=company_id)
        try:
            with transaction.atomic():
                with connection.cursor() as cursor:
                    _delete_manager_and_staff(cursor, managers)
            messages.success(request, "Manager deleted successfully")
        except Exception as e:
            messages.error(request, f"Error deleting manager: {str(e)}")
    except Manager.DoesNotExist:
        messages.error(request, "Manager not found.")
    except Exception as e:
        messages.error(request, f"Error deleting manager: {str(e)}")
    return redirect(f'/administration/all_manager/{company_id}/{company_staff_id}')


def Update_manager_View(request, id):
    try:
        update_info = Manager.objects.get(id=id)
        return render(request, 'administration/manager_profile.html', {'update_info': update_info})
    except Manager.DoesNotExist:
        messages.error(request, 'Manager not found.')
        return HttpResponseRedirect('/administration/all_manager_list')
    except Exception as e:
        messages.error(request, f'Error loading manager: {str(e)}')
        return HttpResponseRedirect('/administration/all_manager_list')


@custom_login_required
def IndexView(request, company_id, company_staff_id):
    # company_id = request.session.get('company')
    if company_id:
        projects_count = Task.objects.filter(assigned_to__user__company__id=company_id).count()
        clients_count = Client.objects.filter(company_id=company_id).count()
        employee_count = Employee.objects.filter(user__company__id=company_id).count()
        lead_count = Lead.objects.all().count()
        context = {
            'projects_count': projects_count,
            'clients_count': clients_count,
            'employee_count': employee_count,
            'lead_count': lead_count,
            'company_id': company_id,
            'company_staff_id': company_staff_id

        }
    else:
        context = {
            'projects_count': 0,
            'clients_count': 0,
            'employee_count': 0,
            'lead_count': 0,
            'company_staff_id': company_staff_id
        }

    return render(request, "administration/index.html", context)


# --------------------------------------------client------------------------------------------------------------------------
def All_client_View(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        client_obj_id = data.get('id', None)
        client_obj = Client.objects.get(pk=client_obj_id)
        return JsonResponse(client_obj.to_json())

    # Old Code
    if company_id:

        client_list = Client.objects.filter(company_id=company_id)
        context = {
            'client_list': client_list,
            'company_id': company_id,
            'company_staff_id': company_staff_id

        }
        return render(request, 'administration/clients-list.html',context)


def EditClient(request,company_id, company_staff_id):
    if company_id:
        if request.method == "GET":
            id = request.GET.get('id')
            client_obj = Client.objects.get(pk=id)
            return JsonResponse(client_obj.to_json())

        elif request.method == "POST":
            client_models_fields_list = [f.name for f in Client._meta.get_fields()]
            client_models_fields_dict = {}
            client_obj_id = request.POST.get('id')
            client_obj = Client.objects.filter(pk=client_obj_id)

            for key, value in request.POST.items():
                if key in client_models_fields_list and key != 'id' and key != 'id' and value is not None and len(
                        value) != 0:
                    print(key, value)
                    client_models_fields_dict.setdefault(key, value)
            client_obj.update(**client_models_fields_dict)
            emp_id = request.POST.get('id')

            print('client id is-')
            print(emp_id)
            return redirect(f'/administration/client_list/{company_id}/{company_staff_id}')


def CreateClientsView(request,company_id, company_staff_id):
    if company_id:
        try:
            if request.method == 'POST':
                client_first_name = request.POST['client_first_name']
                client_last_name = request.POST['client_last_name']
                client_username = request.POST['client_username']
                client_email = request.POST['client_email']
                client_id = request.POST['client_id']
                client_address = request.POST['client_address']
                client_phone = request.POST['client_phone']
                technology = request.POST['technology']
                description = request.POST['description']
                client_obj = Client(client_first_name=client_first_name,client_last_name=client_last_name,client_address=client_address,client_phone=client_phone,description=description,
                                      client_username=client_username, client_email=client_email,technology=technology,client_id=client_id,company_id=company_id)
                client_obj.save()
                return redirect(f'/administration/client_list/{company_id}/{company_staff_id}')

        except Exception as e:
            print(e)
        return render(request, 'administration/clients.html',{'company_id':company_id, 'company_staff_id':company_staff_id})


class CreateClientsListView(generic.ListView):
    model = Client
    template_name = "administration/clients-list.html"
    context_object_name = "client_list"
    success_url = ('/administration/clients_grid')


class CreateClientsGridView(generic.ListView):
    model = Client
    template_name = "administration/clients-list.html"
    context_object_name = "client_list"
    success_url = ('/administration/clients_grid')


class ClientRemove(View):
    def get(self, request,company_id, company_staff_id, id):
        if company_id:
            client = Client.objects.get(id=id)
            client.delete()
            messages.success(request, 'deleted successfuully')
            return redirect(f'/administration/client_list/{company_id}/{company_staff_id}')


class ClientRemoveGrid(View):
    def get(self, request, id):
        client = Client.objects.get(id=id)
        client.delete()
        messages.success(request, 'deleted successfully')
        return HttpResponseRedirect('/administration/clients_grid')


class ClientManageGrid(UpdateView):
    model = Client
    fields = ['client_first_name', 'client_last_name', 'client_username', 'client_email', 'client_id', 'client_address',
              'client_phone', 'client_status']
    context_object_name = "client_update"
    template_name = "administration/client_grid_manage.html"
    success_url = ("/administration/clients_grid/")


class ClientManageList(UpdateView):
    model = Client
    fields = ['client_first_name', 'client_last_name', 'client_username', 'client_email', 'client_id', 'client_address',
              'client_phone', 'client_status']
    context_object_name = "client_list_update"
    template_name = "administration/client_list_manage.html"
    success_url = ("/administration/clients_list/")


# -----------------------------------/client----------------------------------------------------------------

# -------------------------------------Lead----------------------------------------------------------------

def CreateLeadView(request,company_id, company_staff_id):
    if company_id:
        try:
            if request.method == 'POST':
                lead_name = request.POST['lead_name']
                lead_email = request.POST['lead_email']
                lead_phone = request.POST['lead_phone']
                lead_project = request.POST['lead_project']
                lead_assign_staff = request.POST['lead_assign_staff']
                lead_created = request.POST['lead_created']
                lead_source = request.POST['lead_source']

                lead_obj = Lead(lead_name=lead_name,lead_email=lead_email,lead_phone=lead_phone,lead_project=lead_project,
                                      lead_assign_staff=lead_assign_staff, lead_created=lead_created,lead_source=lead_source,company_id=company_id)
                lead_obj.save()
                return redirect(f'/administration/leads_list/{company_id}/{company_staff_id}')

        except Exception as e:
            print(e)
        # return redirect(f'/administration/leads_list/{company_id}/{company_staff_id}')
        return render(request, 'administration/leads.html',{'company_id':company_id, 'company_staff_id':company_staff_id})


def lead_Edit_View(request,company_id, company_staff_id):
    if company_id:
        if request.method == "GET":
            id = request.GET.get('id')
            lead_obj = Lead.objects.get(pk=id)
            return JsonResponse(lead_obj.to_json())

        elif request.method == "POST":
            lead_models_fields_list = [f.name for f in Lead._meta.get_fields()]
            lead_models_fields_dict = {}
            lead_obj_id = request.POST.get('id')
            lead_obj = Lead.objects.filter(pk=lead_obj_id)

            for key, value in request.POST.items():
                if key in lead_models_fields_list and key != 'id' and key != 'id' and value is not None and len(
                        value) != 0:
                    print(key, value)
                    lead_models_fields_dict.setdefault(key, value)
            lead_obj.update(**lead_models_fields_dict)
            emp_id = request.POST.get('id')

            print('lead id is-')
            print(emp_id)
            return redirect(f'/administration/leads_list/{company_id}/{company_staff_id}')


def All_lead_View(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        lead_obj_id = data.get('id', None)
        lead_obj = Lead.objects.get(pk=lead_obj_id)
        return JsonResponse(lead_obj.to_json())

    # Old Code
    if company_id:
        lead_list = Lead.objects.filter(company_id=company_id)
        context = {
            'lead_list': lead_list,
            'company_id': company_id,
            'company_staff_id': company_staff_id

        }
        return render(request, 'administration/leads.html',context)


class LeadsRemove(View):
    def get(self, request,company_id, company_staff_id, id):
        if company_id:
            lead = Lead.objects.get(id=id)
            lead.delete()
            messages.success(request, f"{lead} deleted successfully")
        return redirect(f'/administration/leads_list/{company_id}/{company_staff_id}')


class LeadManage(UpdateView):
    model = Lead
    fields = ['lead_name', 'lead_email', 'lead_phone', 'lead_project', 'lead_assign_staff', 'lead_created',
              'lead_source']
    context_object_name = "lead_update"
    template_name = "administration/lead_manage.html"
    success_url = ("/administration/leads_list/")


def ChangePassword(request):
    if request.user.is_authenticated:
        if request.method == 'POST':
            current = request.POST["cpwd"]
            new_pas = request.POST["npwd"]

            user = User.objects.get(id=request.user.id)
            un = user.email
            check = user.check_password(current)
            if check == True:
                user.set_password(new_pas)
                user.save()
                update_session_auth_hash(request, user)
                messages.success(request, 'Password changed Successfully')
                user = User.objects.get(email=un)
                login(request, user)
            else:
                messages.error(request, 'Incorrect Current Password')

        return render(request, "administration/setting_change_password.html")


def All_entry(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        entry_obj_id = data.get('id', None)
        entry_obj = Entries.objects.get(pk=entry_obj_id)
        return JsonResponse(entry_obj.to_json())

    if company_id:
        entry = Entries.objects.filter(user__user__company_id=company_id)
        
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
        from datetime import timedelta
        
        # Group by employee directly
        employee_map = defaultdict(list)
        for obj in entry:
            employee_map[obj.user].append(obj)

        employee_list = []
        for user, entries in employee_map.items():
            user_total = sum((obj.total_duration for obj in entries), timedelta())
            # Sort entries by project, then by start_time
            entries.sort(key=lambda x: (str(x.project), x.start_time))
            
            email = ''
            name = 'Unknown'
            if user:
                if hasattr(user, 'employee_email') and user.employee_email:
                    email = user.employee_email
                elif user.user and hasattr(user.user, 'email') and user.user.email:
                    email = user.user.email
                name = f"{user.employee_first_name} {user.employee_last_name}"
            
            employee_list.append({
                'user': user,
                'email': email,
                'name': name,
                'total_time': user_total,
                'entries': entries
            })

        # Sort employees by email
        employee_list.sort(key=lambda e: e['email'].lower() if e['email'] else '')

        context = {
            'employee_list': employee_list,
            'company_id': company_id,
            'company_staff_id': company_staff_id,
            'start_date': start_date_str,
            'end_date': end_date_str,
        }
        return render(request, 'administration/view-timesheet.html', context)



class EntryDetailView(DetailView):
    """
    View to show detail info about an Entry
    """
    model = Entries
    template_name = "administration/detail.html"


class EntryRemove(View):
    def get(self, request, id):
        entry_list = Entries.objects.get(id=id)
        entry_list.delete()
        messages.success(request, f"{entry_list} deleted successfully")
        return HttpResponseRedirect('/administration/index')


@csrf_exempt
def check_email_availability(request):
    email = request.POST.get("email")
    try:
        user = User.objects.filter(email=email).exists()
        if user:
            return HttpResponse(True)
        return HttpResponse(False)
    except Exception as e:
        return HttpResponse(False)


def TaskCreateView(request,company_id, company_staff_id):
    if company_id:
        if request.method == "POST":
            try:
                title = request.POST.get("title", '').strip()
                description = request.POST.get("description", '').strip()
                assign_id = request.POST.get("employee_id", '').strip()
                
                if not all([title, description, assign_id]):
                    messages.error(request, "All required fields must be filled.")
                    return redirect(f'/administration/task/new/{company_id}/{company_staff_id}')
                
                try:
                    # Verify employee exists and belongs to the company
                    assigned_to = Employee.objects.get(id=assign_id, user__company__id=company_id)
                except Employee.DoesNotExist:
                    messages.error(request, "Selected employee not found or does not belong to this company.")
                    return redirect(f'/administration/task/new/{company_id}/{company_staff_id}')
                
                try:
                    company = Company.objects.get(id=company_id)
                except Company.DoesNotExist:
                    messages.error(request, "Company not found.")
                    return redirect(f'/administration/task/new/{company_id}/{company_staff_id}')

                Task.objects.create(title=title, description=description, assigned_to=assigned_to, company=company)
                messages.success(request, 'Project assigned successfully!')
                return redirect(f'/administration/projectlist/{company_id}/{company_staff_id}')
            except Exception as e:
                messages.error(request, f'Error creating project: {str(e)}')
                return redirect(f'/administration/task/new/{company_id}/{company_staff_id}')

        else:
            return render(request,"administration/add-project.html",{'assigned':Employee.objects.filter(user__company__id=company_id),'company_id':company_id, 'company_staff_id':company_staff_id})


def ManagerProjectCreateView(request, company_id, company_staff_id):
    if company_id:
        if request.method == "POST":
            try:
                title = request.POST.get("title", "").strip()
                description = request.POST.get("description", "").strip()
                manager_id = request.POST.get("manager_id", "").strip()

                if not all([title, manager_id]):
                    messages.error(request, "All required fields must be filled.")
                    return redirect(f"/administration/manager-project/new/{company_id}/{company_staff_id}")

                try:
                    assigned_to = Manager.objects.get(id=manager_id, user__company_id=company_id)
                except Manager.DoesNotExist:
                    messages.error(request, "Selected manager not found or does not belong to this company.")
                    return redirect(f"/administration/manager-project/new/{company_id}/{company_staff_id}")

                company = Company.objects.filter(id=company_id).first()
                created_by = CompanyStaff.objects.filter(id=company_staff_id, company_id=company_id).first()

                ManagerProject.objects.create(
                    title=title,
                    description=description,
                    assigned_to=assigned_to,
                    company=company,
                    created_by=created_by,
                )
                messages.success(request, "Manager project assigned successfully!")
                return redirect(f"/administration/manager-project/list/{company_id}/{company_staff_id}")
            except Exception as e:
                messages.error(request, f"Error creating manager project: {str(e)}")
                return redirect(f"/administration/manager-project/new/{company_id}/{company_staff_id}")

        managers_qs = Manager.objects.filter(user__company_id=company_id).order_by("manager_first_name", "manager_last_name")
        return render(
            request,
            "administration/add-manager-project.html",
            {"assigned": managers_qs, "company_id": company_id, "company_staff_id": company_staff_id},
        )


def ManagerProject_list(request, company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode("utf-8"))
        project_obj_id = data.get("id", None)
        project_obj = ManagerProject.objects.get(pk=project_obj_id)
        return JsonResponse(project_obj.to_json())

    if company_id:
        project = ManagerProject.objects.filter(assigned_to__user__company_id=company_id)
        context = {
            "project": project,
            "company_id": company_id,
            "company_staff_id": company_staff_id,
        }
        return render(request, "administration/list-manager-project.html", context)


class ManagerProjectRemove(View):
    def get(self, request, company_id, company_staff_id, id):
        if company_id:
            project = ManagerProject.objects.get(id=id)
            project.delete()
            return redirect(f"/administration/manager-project/list/{company_id}/{company_staff_id}")


class TaskDetailView(DetailView, LoginRequiredMixin):
    model = Task
    template_name = "administration/task_detail.html"


class TaskDeleteView(DeleteView, LoginRequiredMixin, UserPassesTestMixin):
    model = Task
    success_url = '/administration/index/'

    def test_func(self):
        task = self.get_object()
        return self.request.user == task.created_by


def attendance(request,company_id, company_staff_id):
    if company_id:
        attendance = Attendance.objects.filter(employee__user__company__id=company_id)
        context = {
            'attendance': attendance,
            'company_id': company_id,
            'company_staff_id':company_staff_id,

        }
    return render(request, 'administration/employee-attendance-list.html',context)


def Project_list(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        project_obj_id = data.get('id', None)
        project_obj = Task.objects.get(pk=project_obj_id)
        return JsonResponse(project_obj.to_json())

    if company_id:
        project =Task.objects.filter(assigned_to__user__company__id=company_id)
        context = {
            'project': project,
            'company_id': company_id,
            'company_staff_id': company_staff_id,

        }
    return render(request, 'administration/list-project.html', context)


class ProjectRemove(View):
    def get(self, request,company_id, company_staff_id, id):
        if company_id:
            project = Task.objects.get(id=id)
            project.delete()
            return redirect(f'/administration/projectlist/{company_id}/{company_staff_id}')


def leaves_list(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        leave_obj_id = data.get('id', None)
        # Try ManagerLeave first, then Leave
        try:
            leave_obj = ManagerLeave.objects.get(pk=leave_obj_id)
            return JsonResponse(leave_obj.to_json())
        except ManagerLeave.DoesNotExist:
            try:
                leave_obj = Leave.objects.get(pk=leave_obj_id)
                return JsonResponse(leave_obj.to_json())
            except Leave.DoesNotExist:
                return JsonResponse({'error': 'Leave not found'}, status=404)

    if company_id:
        # Get both ManagerLeave and Leave pending leaves for the company
        manager_leaves = ManagerLeave.objects.all_pending_leaves().filter(user__user__company_id=company_id)
        employee_leaves = Leave.objects.all_pending_leaves().filter(user__user__company_id=company_id)
        
        # Combine both querysets
        from itertools import chain
        leaves = list(chain(manager_leaves, employee_leaves))
        # Sort by created date (most recent first)
        leaves.sort(key=lambda x: x.created, reverse=True)
        
        return render(request, 'administration/pending-leaves.html',
                      {'leave_list': leaves, 'title': 'leaves list - pending','company_id':company_id, 'company_staff_id':company_staff_id})


def leaves_approved_list(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        leave_obj_id = data.get('id', None)
        # Try ManagerLeave first, then Leave
        try:
            leave_obj = ManagerLeave.objects.get(pk=leave_obj_id)
            return JsonResponse(leave_obj.to_json())
        except ManagerLeave.DoesNotExist:
            try:
                leave_obj = Leave.objects.get(pk=leave_obj_id)
                return JsonResponse(leave_obj.to_json())
            except Leave.DoesNotExist:
                return JsonResponse({'error': 'Leave not found'}, status=404)

    if company_id:
        # Get both ManagerLeave and Leave approved leaves for the company
        manager_leaves = ManagerLeave.objects.all_approved_leaves().filter(user__user__company_id=company_id)
        employee_leaves = Leave.objects.all_approved_leaves().filter(user__user__company_id=company_id)
        
        # Combine both querysets
        from itertools import chain
        leaves = list(chain(manager_leaves, employee_leaves))
        # Sort by created date (most recent first)
        leaves.sort(key=lambda x: x.created, reverse=True)
        
        return render(request, 'administration/approved-leaves.html',
                      {'leave_list': leaves, 'title': 'approved leave list','company_id':company_id, 'company_staff_id':company_staff_id})


def leaves_view(request, id):

    leave = get_object_or_404(ManagerLeave, id=id)
    print(leave.user)

    return render(request, 'administration/leave_detail_view.html', {'leave': leave,
                                                                     'title': '{0}-{1} leave'.format(
                                                                         leave.user.username,
                                                                         leave.status)})


def approve_leave(request,company_id, company_staff_id, id):
    # Try ManagerLeave first, then Leave
    try:
        leave = ManagerLeave.objects.get(id=id)
        leave.approve_leave
        approved = True
    except ManagerLeave.DoesNotExist:
        try:
            leave = Leave.objects.get(id=id)
            leave.approve_leave
            approved = True
        except Leave.DoesNotExist:
            messages.error(request, 'Leave not found',
                           extra_tags='alert alert-danger alert-dismissible show')
            return redirect(f'/administration/leaves/pending/all/{company_id}/{company_staff_id}')

    # Send email notification
    try:
        from administration.email_notifications import send_leave_approval_notification
        send_leave_approval_notification(leave, approved=True)
    except Exception as e:
        print(f"Error sending leave approval notification: {str(e)}")

    messages.success(request, 'Leave successfully approved',
                   extra_tags='alert alert-success alert-dismissible show')
    return redirect(f'/administration/leaves/approved/all/{company_id}/{company_staff_id}')


def cancel_leaves_list(request):
    if not (request.user.is_superuser and request.user.is_authenticated):
        return redirect('/')
    leaves = ManagerLeave.objects.all_cancel_leaves()
    return render(request, 'administration/cancelled-leaves.html',
                  {'leave_list_cancel': leaves, 'title': 'Cancel leave list'})


def unapprove_leave(request, id):
    if not (request.user.is_authenticated and request.user.is_superuser):
        return redirect('/')
    leave = get_object_or_404(ManagerLeave, id=id)
    leave.unapprove_leave
    return redirect('leaveslist')  # redirect to unapproved list


def cancel_leave(request, id):
    if not (request.user.is_superuser and request.user.is_authenticated):
        return redirect('/')
    leave = get_object_or_404(ManagerLeave, id=id)
    leave.leaves_cancel

    messages.success(request, 'Leave is canceled', extra_tags='alert alert-success alert-dismissible show')
    return redirect('canceleaveslist')  # work on redirecting to instance leave - detail view


def uncancel_leave(request, id):
    if not (request.user.is_superuser and request.user.is_authenticated):
        return redirect('/')
    leave = get_object_or_404(ManagerLeave, id=id)
    leave.status = 'pending'
    leave.is_approved = False
    leave.save()
    messages.success(request, 'Leave is uncanceled,now in pending list',
                     extra_tags='alert alert-success alert-dismissible show')
    return redirect('canceleaveslist')  # work on redirecting to instance leave - detail view


def leave_rejected_list(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        leave_obj_id = data.get('id', None)
        # Try ManagerLeave first, then Leave
        try:
            leave_obj = ManagerLeave.objects.get(pk=leave_obj_id)
            return JsonResponse(leave_obj.to_json())
        except ManagerLeave.DoesNotExist:
            try:
                leave_obj = Leave.objects.get(pk=leave_obj_id)
                return JsonResponse(leave_obj.to_json())
            except Leave.DoesNotExist:
                return JsonResponse({'error': 'Leave not found'}, status=404)

    if company_id:
        dataset = dict()
        # Get both ManagerLeave and Leave rejected leaves for the company
        manager_leaves = ManagerLeave.objects.all_rejected_leaves().filter(user__user__company_id=company_id)
        employee_leaves = Leave.objects.all_rejected_leaves().filter(user__user__company_id=company_id)
        
        # Combine both querysets
        from itertools import chain
        leaves = list(chain(manager_leaves, employee_leaves))
        # Sort by created date (most recent first)
        leaves.sort(key=lambda x: x.created, reverse=True)

        dataset['leave_list_rejected'] = leaves
        dataset[ 'company_id'] =  company_id
        dataset['company_staff_id'] = company_staff_id
        return render(request, 'administration/rejected-leaves.html', dataset)


def reject_leave(request,company_id, company_staff_id,id):
    dataset = dict()
    # Try ManagerLeave first, then Leave
    try:
        leave = ManagerLeave.objects.get(id=id)
        leave.reject_leave
    except ManagerLeave.DoesNotExist:
        try:
            leave = Leave.objects.get(id=id)
            leave.reject_leave
        except Leave.DoesNotExist:
            messages.error(request, 'Leave not found',
                           extra_tags='alert alert-danger alert-dismissible show')
            return redirect(f'/administration/leaves/pending/all/{company_id}/{company_staff_id}')
    
    # Send email notification
    try:
        from administration.email_notifications import send_leave_approval_notification
        send_leave_approval_notification(leave, approved=False)
    except Exception as e:
        print(f"Error sending leave rejection notification: {str(e)}")
    
    messages.success(request, 'Leave is rejected', extra_tags='alert alert-success alert-dismissible show')
    return redirect(f'/administration/leaves/rejected/all/{company_id}/{company_staff_id}')


def unreject_leave(request, id):
    leave = get_object_or_404(ManagerLeave, id=id)
    leave.status = 'pending'
    leave.is_approved = False
    leave.save()
    messages.success(request, 'Leave is now in pending list ', extra_tags='alert alert-success alert-dismissible show')

    return redirect('leavesrejected')


def add_leaves_balance(request, company_id, company_staff_id):
    """View for Add Leaves Balance page - handles both leave application and balance management"""
    # Handle leave balance assignment form submission
    if request.method == "POST" and 'balancedays' in request.POST:
        balancedays = request.POST.get("balancedays")
        employee_id = request.POST.get("employee_id")
        try:
            if employee_id:
                employee = Employee.objects.get(id=employee_id)
                BalanceLeaves.objects.create(user=employee, balancedays=int(balancedays))
                messages.success(request, f'Leave balance of {balancedays} day(s) assigned to {employee.employee_first_name} {employee.employee_last_name}!')
            else:
                messages.error(request, 'Please select an employee.')
        except Exception as e:
            messages.error(request, f'Error assigning leave balance: {str(e)}')
        return redirect("add_leaves_balance", company_id=company_id, company_staff_id=company_staff_id)

    # Handle leave application form submission
    if request.method == "POST" and 'startdate' in request.POST:
        startdate = request.POST.get("startdate")
        enddate = request.POST.get("enddate")
        leavetype = request.POST.get("leavetype")
        reason = request.POST.get("reason")
        description = request.POST.get("description", "")
        employee_id = request.POST.get("employee_id")
        
        try:
            if employee_id:
                employee = Employee.objects.get(id=employee_id)
            else:
                # Fallback to company_staff employee
                company_staff = CompanyStaff.objects.get(id=company_staff_id)
                employee = company_staff.employee
            
            if employee:
                from datetime import datetime as dt
                start_dt = dt.strptime(startdate, "%Y-%m-%d").date()
                end_dt = dt.strptime(enddate, "%Y-%m-%d").date()
                if start_dt > end_dt:
                    messages.error(request, 'End date must be on or after start date.')
                    return redirect("add_leaves_balance", company_id=company_id, company_staff_id=company_staff_id)

                days_requested = (end_dt - start_dt).days + 1
                balance_summary = BalanceLeaves.get_balance_summary(employee)
                if days_requested > balance_summary['remaining_balance']:
                    messages.error(
                        request,
                        f"Insufficient leave balance! Employee has {balance_summary['remaining_balance']} day(s) remaining, but requested {days_requested} day(s)."
                    )
                    return redirect("add_leaves_balance", company_id=company_id, company_staff_id=company_staff_id)

                Leave.objects.create(
                    user=employee,
                    startdate=startdate,
                    enddate=enddate,
                    leavetype=leavetype,
                    reason=reason,
                    description=description
                )
                messages.success(request, 'Leave applied successfully!')
                return redirect("add_leaves_balance", company_id=company_id, company_staff_id=company_staff_id)
            else:
                messages.error(request, 'Employee not found.')
        except Employee.DoesNotExist:
            messages.error(request, 'Employee not found.')
        except CompanyStaff.DoesNotExist:
            messages.error(request, 'Company staff not found.')
        except Exception as e:
            messages.error(request, f'Error applying leave: {str(e)}')
    
    # GET request - display the page
    if company_id:
        # Get employees for the dropdown
        employees = Employee.objects.filter(user__company__id=company_id).order_by('employee_first_name')
        context = {
            'employees': employees,
            'company_id': company_id,
            'company_staff_id': company_staff_id,
        }
        return render(request, 'administration/add-leaves-balance.html', context)


def Balance_list(request,company_id, company_staff_id):
    if company_id:
        emp_balance = BalanceLeaves.objects.filter(user__user__company_id=company_id)
        mgr_balance = BalanceLeave.objects.filter(user__user__company_id=company_id)
        context = {
            'balance': emp_balance,
            'mgr_balance': mgr_balance,
            'company_id': company_id,
            'company_staff_id': company_staff_id,
        }
        return render(request, 'administration/leaves-balance-list.html', context)


class BalanceRemove(View):
    def get(self, request,company_id, company_staff_id, id):
        if company_id:
            try:
                try:
                    balance = BalanceLeaves.objects.get(id=id)
                except BalanceLeaves.DoesNotExist:
                    balance = BalanceLeave.objects.get(id=id)
                balance.delete()
                messages.success(request, 'Leave balance deleted successfully!')
            except Exception:
                messages.error(request, 'Leave balance not found!')
            return redirect(f'/administration/balancelist/{company_id}/{company_staff_id}')


def notifications(request,company_id, company_staff_id):
    if company_id:
        notify = notification.objects.filter(company__id=company_id)
        context = {
            'notify': notify ,
            'company_id': company_id,
            'company_staff_id': company_staff_id,
        }
        user = CompanyStaff.objects.get(id=company_staff_id, company_id=company_id)
        user.new_notification = False
        user.save()
        request.session["new_notification"] = user.new_notification
        return render(request, 'administration/notifications.html', context)


def createnotifications(request,company_id, company_staff_id):
    if company_id:
        try:
            if request.method == 'POST':
                notify = request.POST['notify']
                print(notify)
                notify_obj = notification(notify=notify,company_id=company_id)
                notify_obj.save()
                return redirect(f'/administration/notifications/{company_id}/{company_staff_id}')
        except Exception as e:
            print(e)
        return render(request, 'administration/notifications.html',{'company_id':company_id, 'company_staff_id':company_staff_id})


def getnotification(request):
    notify = notification.objects.all()
    notify_obj = [{'notify': i.notify} for i in notify]
    return JsonResponse({'notify': notify_obj})


def getattendance(request):
    attendance = Attendance.objects.all()
    attendance_obj = [{'employee__employee_id': i.employee_id, 'check_in': i.check_in, 'check_out': i.check_out}
                      for i in attendance]
    return JsonResponse({'attendance': attendance_obj})


def attendance(request,company_id, company_staff_id):
    if company_id:
        attendance = Attendance.objects.filter(employee__user__company__id=company_id)
        context = {
            'attendance': attendance,
            'company_id': company_id,
            'company_staff_id':company_staff_id,

        }
    return render(request, 'administration/employee-attendance-list.html',context)


def attendance_Edit_View(request,company_id, company_staff_id):
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

            # print('attendance id is-')
            # print(emp_id)
            return redirect(f'/administration/attendancee/{company_id}/{company_staff_id}')


class AttendanceRemove(View):
    def get(self, request,company_id, company_staff_id, id):
        if company_id:
            attendance = Attendance.objects.get(id=id)
            attendance.delete()
            messages.success(request, f"{attendance} deleted successfully")
            return redirect(f'/administration/attendancee/{company_id}/{company_staff_id}')


class AttendanceManage(UpdateView):
    model = Attendance
    fields = ['check_in', 'check_out']
    context_object_name = "attendance_update"
    template_name = "administration/attendance_manage.html"
    success_url = ("/administration/attendancee/")

    def post(self, request, pk):
        from django.utils.dateparse import parse_datetime
        from django.utils import timezone as tz
        data = Attendance.objects.get(id=pk)
        check_out_str = request.POST.get('check_out')
        check_in_str = request.POST.get('check_in')

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
        return HttpResponseRedirect("/administration/attendancee/")


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
    return render(request, 'administration/employee-attendance-list.html', context)


def resign_list(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        resign_obj_id = data.get('id', None)
        resign_obj = Resign.objects.get(pk=resign_obj_id)
        return JsonResponse(resign_obj.to_json())

    if company_id:
        resign = Resign.objects.all_pending_resign().filter(user__user__company_id=company_id)
        return render(request, 'administration/pending-resignation.html',
                      {'resign_list': resign, 'title': 'resign list - pending','company_id':company_id, 'company_staff_id':company_staff_id})


def resign_approved_list(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        resign_obj_id = data.get('id', None)
        resign_obj = Resign.objects.get(pk=resign_obj_id)
        return JsonResponse(resign_obj.to_json())

    if company_id:
        resign = Resign.objects.all_approved_resign().filter(user__user__company_id=company_id)  # approved leaves -> calling model manager method
        return render(request, 'administration/approved-resignation.html',
                      {'resign_list': resign, 'title': 'approved resign list','company_id':company_id, 'company_staff_id':company_staff_id})


def resign_view(request, id):
    if not (request.user.is_authenticated):
        return redirect('/')

    resign = get_object_or_404(Resign, id=id)

    return render(request, 'administration/resign_detail_view.html', {'resign': resign,
                                                                      'title': '{0}-{1} resign'.format(
                                                                          resign.user.username,
                                                                          resign.status)})


def approve_resign(request,company_id, company_staff_id, id):

    resign = get_object_or_404(Resign, id=id)
    # user = resign.user
    resign.approve_resign

    messages.error(request, 'Resignation successfully approved',
                   extra_tags='alert alert-success alert-dismissible show')
    return redirect(f'/administration/resign/approved/all/{company_id}/{company_staff_id}')


def cancel_resign_list(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        resign_obj_id = data.get('id', None)
        resign_obj = Resign.objects.get(pk=resign_obj_id)
        return JsonResponse(resign_obj.to_json())

    if company_id:
        resign = Resign.objects.all_cancel_resign().filter(user__user__company_id=company_id)
        return render(request, 'administration/cancelled-resignation.html',
                      {'resign_list': resign, 'title': 'Cancel resign list','company_id':company_id, 'company_staff_id':company_staff_id})


def unapprove_resign(request, id):
    if not (request.user.is_authenticated and request.user.is_superuser):
        return redirect('/')
    resign = get_object_or_404(Resign, id=id)
    resign.unapprove_resign
    return redirect('resignlist')  # redirect to unapproved list


def cancel_resign(request,company_id, company_staff_id, id):

    resign = get_object_or_404(Resign, id=id)
    resign.resign_cancel

    messages.success(request, 'Resign is canceled', extra_tags='alert alert-success alert-dismissible show')
    return redirect(f'/administration/resign/cancel/all/{company_id}/{company_staff_id}')


# Current section -> here
def uncancel_resign(request, id):
    if not (request.user.is_superuser and request.user.is_authenticated):
        return redirect('/')
    resign = get_object_or_404(Resign, id=id)
    resign.status = 'pending'
    resign.is_approved = False
    resign.save()
    messages.success(request, 'Leave is uncanceled,now in pending list',
                     extra_tags='alert alert-success alert-dismissible show')
    return redirect('cancelresignlist')


def resign_rejected_list(request, company_id, company_staff_id):
    dataset = dict()
    if company_id:
        resign = Resign.objects.all_rejected_resign().filter(user__user__company_id=company_id)
    else:
        resign = Resign.objects.all_rejected_resign()
    dataset['resign_list_rejected'] = resign
    dataset['company_id'] = company_id
    dataset['company_staff_id'] = company_staff_id
    return render(request, 'administration/rejected_resign_list.html', dataset)


def reject_resign(request, company_id, company_staff_id, id):
    dataset = dict()
    resign = get_object_or_404(Resign, id=id)
    resign.reject_resign
    messages.success(request, 'Resignation is rejected', extra_tags='alert alert-success alert-dismissible show')
    return redirect(f'/administration/resign/rejected/all/{company_id}/{company_staff_id}')


def unreject_resign(request, company_id, company_staff_id, id):
    resign = get_object_or_404(Resign, id=id)
    resign.status = 'pending'
    resign.is_approved = False
    resign.save()
    messages.success(request, 'Resignation is now in pending list ',
                     extra_tags='alert alert-success alert-dismissible show')

    return redirect(f'/administration/resign/rejected/all/{company_id}/{company_staff_id}')


def holidays(request,company_id, company_staff_id):
    if company_id:
        try:
            if request.method == 'POST':
                day = request.POST['day']
                print(day)
                date = request.POST['date']
                print(date)
                occassion = request.POST['occassion']
                print(occassion)
                type = request.POST['type']
                print(type)
                status = 1
                holiday_obj = holiday(day=day, date=date,
                                      occassion=occassion, holidaytype=type, status=status,company_id=company_id)
                holiday_obj.save()
                return render(request, 'administration/add-holiday.html', {'msg': 'Data updated'},{'company_id':company_id, 'company_staff_id':company_staff_id})
        except Exception as e:
            print(e)
        return render(request, 'administration/add-holiday.html',{'company_id':company_id, 'company_staff_id':company_staff_id})


def fnholidays(request):
    holidays = holiday.objects.all()
    holiday_obj = [{'id': i.id, 'day': i.day, 'date': i.date,
                    'occassion': i.occassion, 'type': i.holidaytype} for i in holidays]
    print(holiday_obj)
    return JsonResponse({'holiday': holiday_obj})


def getdatas(request):
    user = Employee.objects.all()
    user_obj = [{'id': i.id} for i in user]
    holidays = holiday.objects.all().count()
    return JsonResponse({'user': user_obj, 'holiday': holidays})


def holiday_list(request,company_id, company_staff_id):
    if company_id:
        holyday = holiday.objects.filter(company_id=company_id)
        context = {
            'holyday': holyday,
            'company_id': company_id,
            'company_staff_id': company_staff_id,

        }
        return render(request, 'administration/list-holiday.html', context)


class delholiday(View):
    def get(self, request,company_id, company_staff_id, id):
        if company_id:
            holyday = holiday.objects.get(id=id)
            holyday.delete()
            messages.success(request, f"{holyday} deleted successfully")
            return redirect(f'/administration/holidaylist/{company_id}/{company_staff_id}')


class PostListView(ListView):
    def dispatch(self, request, company_id, company_staff_id, *args, **kwargs):
        print('Dispatch function called')
        company_staff = CompanyStaff.objects.filter(pk=company_staff_id)
        if company_staff.exists():
            if company_staff.first().is_authenticated:
                return super().dispatch(request, company_id, company_staff_id, *args, **kwargs)
            else:
                return redirect('/')
        else:
            return redirect('/')

    model = Post
    template_name = 'administration/employee-all-documents.html'
    context_object_name = 'posts'
    ordering = ['-date_posted']
    paginate_by = 2


def All_document_View(request,company_id, company_staff_id):
    if request.method == "POST":
        try:
            data = json.loads(request.body.decode('utf-8'))
            document_obj_id = data.get('id', None)
            if not document_obj_id:
                return JsonResponse({'error': 'Document ID is required'}, status=400)
            document_obj = Post.objects.get(pk=document_obj_id, user__user__company_id=company_id)
            return JsonResponse(document_obj.to_json())
        except Post.DoesNotExist:
            return JsonResponse({'error': 'Document not found'}, status=404)
        except json.JSONDecodeError:
            return JsonResponse({'error': 'Invalid JSON data'}, status=400)
        except Exception as e:
            return JsonResponse({'error': str(e)}, status=500)

    # Old Code
    if company_id:
        document_list = Post.objects.filter(user__user__company__id=company_id)
        employees_list = Employee.objects.filter(user__company__id=company_id).order_by('employee_first_name')
        return render(request, 'administration/employee-all-documents.html',{
            'document_list': document_list,
            'employees': employees_list,
            'company_id': company_id, 
            'company_staff_id': company_staff_id
        })


def search(request):
    template = 'administration/employee-all-documents.html'

    query = request.GET.get('q')

    result = Post.objects.filter(
        Q(user__employee_email__icontains=query))
    paginate_by = 2
    context = {'posts': result}
    return render(request, template, context)


class UserPostListView(LoginRequiredMixin, ListView):
    model = Post
    template_name = 'administration/employee-all-documents.html'
    context_object_name = 'posts'
    paginate_by = 2

    def get_queryset(self):
        queryset = super(UserPostListView, self).get_queryset()
        queryset = Post.objects.filter(user=self.request.user.employee)
        return queryset


def PostDetailView(request,company_id, company_staff_id,id):
    if request.method == "POST":
        try:
            data = json.loads(request.body.decode('utf-8'))
            document_obj_id = data.get('id', None)
            if not document_obj_id:
                return JsonResponse({'error': 'Document ID is required'}, status=400)
            document_obj = Post.objects.get(pk=document_obj_id, user__user__company_id=company_id)
            return JsonResponse(document_obj.to_json())
        except Post.DoesNotExist:
            return JsonResponse({'error': 'Document not found'}, status=404)
        except json.JSONDecodeError:
            return JsonResponse({'error': 'Invalid JSON data'}, status=400)
        except Exception as e:
            return JsonResponse({'error': str(e)}, status=500)

    # Old Code
    if company_id:
        # company_staff = CompanyStaff.objects.get(id=company_staff_id)
        document_list = Post.objects.filter(id=id)
        # document_list = Post.objects.filter(user=company_staff)
        return render(request, 'administration/employee_documents.html',
                      {'document_list': document_list,'company_id':company_id, 'company_staff_id':company_staff_id})


class PostDeleteView(View):
    def get(self, request,company_id, company_staff_id, id):
        if company_id:
            posts = Post.objects.get(id=id)
            posts.delete()
            messages.success(request, f"{posts} deleted successfully")
            return redirect(f'/administration/all_document_View/{company_id}/{company_staff_id}')


def DepartmentCreateView(request,company_id, company_staff_id):
    if company_id:
        if request.method == 'POST':
            try:
                department_name_str = request.POST.get('department_name', '').strip()
                if not department_name_str:
                    messages.error(request, 'Department name cannot be empty.')
                    return redirect(f'/administration/department_lst/{company_id}/{company_staff_id}')
                
                # Check if department with same name already exists for this company
                if Department.objects.filter(department_name=department_name_str, company_id=company_id).exists():
                    messages.error(request, f'Department "{department_name_str}" already exists for this company.')
                    return redirect(f'/administration/department_lst/{company_id}/{company_staff_id}')
                
                # Create the department
                department = Department(department_name=department_name_str, company_id=company_id)
                department.save()
                messages.success(request, f'Department "{department_name_str}" created successfully!')
                return redirect(f'/administration/department_lst/{company_id}/{company_staff_id}')
                
            except Exception as e:
                messages.error(request, f'Error creating department: {str(e)}')
                return redirect(f'/administration/department_lst/{company_id}/{company_staff_id}')
        
        return render(request, 'administration/department.html',{'company_id':company_id, 'company_staff_id':company_staff_id})


def DepartmentList(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        client_obj_id = data.get('id', None)
        client_obj = Client.objects.get(pk=client_obj_id)
        return JsonResponse(client_obj.to_json())

    # Old Code
    if company_id:
        department_list = Department.objects.filter(company_id=company_id)
        # Add sequential formatted IDs to each department
        for index, department in enumerate(department_list, start=1):
            department.display_id = f"DEP-{index:03d}"
        context = {
            'department_list': department_list,
            'company_id': company_id,
            'company_staff_id': company_staff_id

        }
        return render(request, 'administration/department.html',context)


def department_Edit_View(request,company_id, company_staff_id):
    if company_id:
        if request.method == "GET":
            try:
                id = request.GET.get('id')
                if not id:
                    return JsonResponse({'error': 'Department ID is required'}, status=400)
                department_obj = Department.objects.get(pk=id, company_id=company_id)
                return JsonResponse(department_obj.to_json())
            except Department.DoesNotExist:
                return JsonResponse({'error': 'Department not found'}, status=404)
            except Exception as e:
                return JsonResponse({'error': str(e)}, status=500)

        elif request.method == "POST":
            department_models_fields_list = [f.name for f in Department._meta.get_fields()]
            department_models_fields_dict = {}
            department_obj_id = request.POST.get('id')
            department_obj = Department.objects.filter(pk=department_obj_id)

            for key, value in request.POST.items():
                if key in department_models_fields_list and key != 'id' and key != 'id' and value is not None and len(
                        value) != 0:
                    print(key, value)
                    department_models_fields_dict.setdefault(key, value)
            department_obj.update(**department_models_fields_dict)
            emp_id = request.POST.get('id')

            print('department id is-')
            print(emp_id)
            return redirect(f'/administration/department_lst/{company_id}/{company_staff_id}')


class DepartmentRemove(View):
    def get(self, request,company_id, company_staff_id, id):
        if company_id:
            department = Department.objects.get(id=id)
            department.delete()
            messages.success(request, f"{department} deleted successfully")
            return redirect(f'/administration/department_lst/{company_id}/{company_staff_id}')


class ManageDepartment(UpdateView):
    model = Department
    fields = ['department_name']
    context_object_name = "department_update"
    template_name = "administration/department_manage.html"
    success_url = ("/administration/department_list/")


def regularization_list(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        regularization_obj_id = data.get('id', None)
        regularization_obj = Regularization.objects.get(pk=regularization_obj_id)
        return JsonResponse(regularization_obj.to_json())

    if company_id:
        regularization = Regularization.objects.all_pending_regularization().filter(user__user__company__id=company_id)
        return render(request, 'administration/employee-pending-regularization.html',
                      {'regularization_list': regularization, 'title': 'regularization list - pending','company_id':company_id, 'company_staff_id':company_staff_id})


def regularization_approved_list(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        regularization_obj_id = data.get('id', None)
        regularization_obj = Regularization.objects.get(pk=regularization_obj_id)
        return JsonResponse(regularization_obj.to_json())

    regularization = Regularization.objects.all_approved_regularization().filter(user__user__company__id=company_id)  # approved leaves -> calling model manager method
    return render(request, 'administration/employee-approved-regularization.html',
                  {'regularization_list': regularization, 'title': 'approved regularization list','company_id':company_id, 'company_staff_id':company_staff_id})


def approve_regularization(request,company_id, company_staff_id, id):
    if company_id:
        regularization = get_object_or_404(Regularization, id=id)
        regularization.approve_regularization

        messages.success(request, 'regularization is approved', extra_tags='alert alert-success alert-dismissible show')
        return redirect(f'/administration/regularization/approved/all/{company_id}/{company_staff_id}')


def cancel_regularization_list(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        regularization_obj_id = data.get('id', None)
        regularization_obj = Regularization.objects.get(pk=regularization_obj_id)
        return JsonResponse(regularization_obj.to_json())

    regularization = Regularization.objects.all_cancel_regularization().filter(user__user__company__id=company_id)
    return render(request, 'administration/employee-cancelled-regularization.html',
                  {'regularization_list_cancel': regularization, 'title': 'Cancel regularization list','company_id':company_id, 'company_staff_id':company_staff_id})


def unapprove_regularization(request, id):
    if not (request.user.is_authenticated and request.user.is_superuser):
        return redirect('/')
    regularization = get_object_or_404(Regularization, id=id)
    regularization.unapprove_regularization
    return redirect('regularizationlist')  # redirect to unapproved list


def cancel_regularization(request,company_id, company_staff_id, id):
    if company_id:
        regularization = get_object_or_404(Regularization, id=id)
        regularization.regularization_cancel

        messages.success(request, 'regularization is canceled', extra_tags='alert alert-success alert-dismissible show')
        return redirect(f'/administration/regularization/cancel/all/{company_id}/{company_staff_id}')


def uncancel_regularization(request, id):
    if not (request.user.is_superuser and request.user.is_authenticated):
        return redirect('/')
    regularization = get_object_or_404(Regularization, id=id)
    regularization.status = 'pending'
    regularization.is_approved = False
    regularization.save()
    messages.success(request, 'Leave is uncanceled,now in pending list',
                     extra_tags='alert alert-success alert-dismissible show')
    return redirect('cancelregularizationlist')


def regularization_rejected_list(request):
    dataset = dict()
    regularization = Regularization.objects.all_rejected_regularization()

    dataset['regularization_list_rejected'] = regularization
    return render(request, 'administration/rejected_regularization_list.html', dataset)


def reject_regularization(request, id):
    dataset = dict()
    regularization = get_object_or_404(Leave, id=id)
    regularization.reject_leave
    messages.success(request, 'regularizationation is rejected',
                     extra_tags='alert alert-success alert-dismissible show')
    return redirect('regularizationrejected')


def unreject_regularization(request, id):
    regularization = get_object_or_404(Regularization, id=id)
    regularization.status = 'pending'
    regularization.is_approved = False
    regularization.save()
    messages.success(request, 'regularizationation is now in pending list ',
                     extra_tags='alert alert-success alert-dismissible show')

    return redirect('regularizationrejected')


def mregularization_list(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        regularization_obj_id = data.get('id', None)
        regularization_obj = MRegularization.objects.get(pk=regularization_obj_id)
        return JsonResponse(regularization_obj.to_json())

    if company_id:
        regularization = MRegularization.objects.all_pending_regularization().filter(user__user__company__id=company_id)
        return render(request, 'administration/manager-pending-regularization.html',
                      {'regularization_list': regularization, 'title': 'regularization list - pending','company_id':company_id, 'company_staff_id':company_staff_id})


def mregularization_approved_list(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        regularization_obj_id = data.get('id', None)
        regularization_obj = MRegularization.objects.get(pk=regularization_obj_id)
        return JsonResponse(regularization_obj.to_json())

    if company_id:
        regularization = MRegularization.objects.all_approved_regularization().filter(user__user__company__id=company_id) # approved leaves -> calling model manager method
        return render(request, 'administration/manager-approved-regularization.html',
                      {'regularization_list': regularization, 'title': 'approved regularization list','company_id':company_id, 'company_staff_id':company_staff_id})


def mregularization_view(request, id):
    if not (request.user.is_authenticated):
        return redirect('/')

    regularization = get_object_or_404(MRegularization, id=id)

    return render(request, 'administration/mregularization_detail_view.html', {'regularization': regularization,
                                                                               'title': '{0}-{1} regularization'.format(
                                                                                   regularization.user.manager_email,
                                                                                   regularization.status)})


def mapprove_regularization(request,company_id, company_staff_id, id):
    if company_id:
        regularization = get_object_or_404(MRegularization, id=id)

        regularization.approve_regularization

        messages.error(request, 'regularizationation successfully approved',
                       extra_tags='alert alert-success alert-dismissible show')
        return redirect(f'/administration/mregularization/approved/all/{company_id}/{company_staff_id}')


def mcancel_regularization_list(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        regularization_obj_id = data.get('id', None)
        regularization_obj = MRegularization.objects.get(pk=regularization_obj_id)
        return JsonResponse(regularization_obj.to_json())

    if company_id:
        regularization = MRegularization.objects.all_cancel_regularization().filter(user__user__company__id=company_id)
        return render(request, 'administration/manager-cancelled-regularization.html',
                      {'regularization_list': regularization, 'title': 'Cancel regularization list','company_id':company_id, 'company_staff_id':company_staff_id})


def munapprove_regularization(request, id):

    regularization = get_object_or_404(MRegularization, id=id)
    regularization.unapprove_regularization
    return redirect('mregularizationlist')  # redirect to unapproved list

def mcancel_regularization(request,company_id, company_staff_id, id):
    if company_id:

        regularization = get_object_or_404(MRegularization, id=id)
        regularization.regularization_cancel

        messages.success(request, 'regularization is canceled', extra_tags='alert alert-success alert-dismissible show')
        return redirect(f'/administration/mregularization/cancel/all/{company_id}/{company_staff_id}')


def muncancel_regularization(request, id):
    if not (request.user.is_superuser and request.user.is_authenticated):
        return redirect('/')
    regularization = get_object_or_404(MRegularization, id=id)
    regularization.status = 'pending'
    regularization.is_approved = False
    regularization.save()
    messages.success(request, 'Regularization is uncanceled,now in pending list',
                     extra_tags='alert alert-success alert-dismissible show')
    return redirect('mcancelregularizationlist')


def mregularization_rejected_list(request):
    dataset = dict()
    regularization = MRegularization.objects.all_rejected_regularization()

    dataset['regularization_list_rejected'] = regularization
    return render(request, 'administration/mrejected_regularization_list.html', dataset)


def mreject_regularization(request, id):
    dataset = dict()
    regularization = get_object_or_404(Leave, id=id)
    regularization.reject_leave
    messages.success(request, 'regularizationation is rejected',
                     extra_tags='alert alert-success alert-dismissible show')
    return redirect('mregularizationrejected')


def munreject_regularization(request, id):
    regularization = get_object_or_404(MRegularization, id=id)
    regularization.status = 'pending'
    regularization.is_approved = False
    regularization.save()
    messages.success(request, 'regularizationation is now in pending list ',
                     extra_tags='alert alert-success alert-dismissible show')

    return redirect('mregularizationrejected')


def mattendance(request,company_id, company_staff_id):
    if company_id:
        attendance = ManagerAttendance.objects.filter(manager__user__company__id=company_id)
        context = {
            'attendance': attendance,
            'company_id': company_id,
            'company_staff_id': company_staff_id,

        }
        return render(request, 'administration/manager-attendance-list.html', context)


def Mattendance_Edit_View(request,company_id, company_staff_id):
    if company_id:
        if request.method == "GET":
            id = request.GET.get('id')
            attendance_obj = ManagerAttendance.objects.get(pk=id)
            return JsonResponse(attendance_obj.to_json())

        elif request.method == "POST":
            attendance_models_fields_list = [f.name for f in ManagerAttendance._meta.get_fields()]
            attendance_models_fields_dict = {}
            attendance_obj_id = request.POST.get('id')
            attendance_obj = ManagerAttendance.objects.filter(pk=attendance_obj_id)

            for key, value in request.POST.items():
                if key in attendance_models_fields_list and key != 'id' and key != 'id' and value is not None and len(
                        value) != 0:
                    print(key, value)
                    attendance_models_fields_dict.setdefault(key, value)
            attendance_obj.update(**attendance_models_fields_dict)
            emp_id = request.POST.get('id')

            print('attendance id is-')
            print(emp_id)
            return redirect(f'/administration/mattendancee/{company_id}/{company_staff_id}')


class mAttendanceRemove(View):
    def get(self, request,company_id, company_staff_id, id):
        if company_id:
            attendance = ManagerAttendance.objects.get(id=id)
            attendance.delete()
            messages.success(request, f"{attendance} deleted successfully")
            return redirect(f'/administration/mattendancee/{company_id}/{company_staff_id}')


class mAttendanceManage(UpdateView):
    model = ManagerAttendance
    fields = ['check_in', 'check_out']
    context_object_name = "attendance_update"
    template_name = "administration/manager-attendance-list.html"
    success_url = ("/administration/mattendancee/")

    def post(self, request, pk):
        data = ManagerAttendance.objects.get(id=pk)
        data.check_out = request.POST.get('check_out')
        data.check_in = request.POST.get('check_in')
        data.save()
        print(str(data) + "=" + str(request.POST.get('check_out')))
        return HttpResponseRedirect("/administration/mattendancee/")


def mAttendancesearch(request,company_id, company_staff_id):
    if 'q' in request.GET:
        q = request.GET['q']
        multiple_q = Q(Q(manager__user__email__icontains=q) | Q(check_in__icontains=q) | Q(check_out__icontains=q))
        attendance = ManagerAttendance.objects.filter(multiple_q)
    else:
        attendance = ManagerAttendance.objects.filter(manager__user__company__id=company_id)
    context = {
        'attendance': attendance,
        'company_id': company_id,
        'company_staff_id': company_staff_id
    }
    return render(request, 'administration/manager-attendance-list.html', context)


def assignCreateView(request,company_id, company_staff_id):
    if company_id:
        if request.method == "POST":
            employee_id = request.POST.get("employee_id")
            employee_to = Employee.objects.get(id=employee_id)
            description = request.POST.get("description")
            assign_id = request.POST.get("manager_id")
            assigned_to = Manager.objects.get(id =assign_id)
            # company_staff = CompanyStaff.objects.get(id=company_staff_id)
            # user = company_staff
            # emp = Employee.objects.get(user = user)

            Asign.objects.create(employee=employee_to,description=description,assigned_to=assigned_to)
            return redirect(f'/administration/assignlist/{company_id}/{company_staff_id}')

        else:
            return render(request,"administration/assign-employee.html",{'assigned':Manager.objects.filter(user__company__id=company_id),'assignedto':Employee.objects.filter(user__company__id=company_id),'company_id':company_id, 'company_staff_id':company_staff_id})


class assignDetailView(DetailView, LoginRequiredMixin):
    model = Asign
    template_name = "administration/assign-employee.html"


class aasignDeleteView(DeleteView, LoginRequiredMixin, UserPassesTestMixin):
    model = Asign
    success_url = '/administration/index/'

    def test_func(self):
        assign = self.get_object()
        return self.request.user == assign.created_by


def Assign_list(request,company_id, company_staff_id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        assign_obj_id = data.get('id', None)
        assign_obj = Asign.objects.get(pk=assign_obj_id)
        return JsonResponse(assign_obj.to_json())

    if company_id:
        assign = Asign.objects.filter(employee__user__company__id=company_id)
        context = {
            'assign': assign,
            'company_id': company_id,
            'company_staff_id': company_staff_id,

        }
        return render(request, 'administration/employee-list.html', context)


class AssignRemove(View):
    def get(self, request,company_id, company_staff_id, id):
        assign = Asign.objects.get(id=id)
        assign.delete()
        return redirect(f'/administration/assignlist/{company_id}/{company_staff_id}')


def All_document_Views(request,company_id, company_staff_id):
    # Handle AJAX POST request for document details
    if request.method == "POST" and request.content_type == 'application/json':
        data = json.loads(request.body.decode('utf-8'))
        document_obj_id = data.get('id', None)
        if document_obj_id:
            document_obj = ManagerPost.objects.get(pk=document_obj_id)
            return JsonResponse(document_obj.to_json())
    
    # Fetch managers for the dropdown (filtered by company) - EXACTLY like All_manager_View line 268
    if company_id:
        managers_list = Manager.objects.filter(user__company__id=company_id).order_by('manager_first_name')
    else:
        managers_list = Manager.objects.none()
    
    # Handle form POST request for document upload
    if request.method == "POST" and 'manager' in request.POST:
        manager_id = request.POST.get("manager")
        if not manager_id:
            messages.error(request, 'Please select a manager.')
            document_list = ManagerPost.objects.filter(user__user__company__id=company_id) if company_id else ManagerPost.objects.none()
            return render(request, 'administration/manager-all-documents.html', {
                'document_list': document_list,
                'managers': managers_list,
                'company_id': company_id,
                'company_staff_id': company_staff_id
            })
        
        # Check if at least one file is provided
        has_files = any(key in request.FILES for key in ['experience_letter', 'offer_letter', 'education_certificate', 'skill_certificate'])
        if not has_files:
            messages.error(request, 'Please upload at least one document.')
            document_list = ManagerPost.objects.filter(user__user__company__id=company_id) if company_id else ManagerPost.objects.none()
            return render(request, 'administration/manager-all-documents.html', {
                'document_list': document_list,
                'managers': managers_list,
                'company_id': company_id,
                'company_staff_id': company_staff_id
            })
        
        manager = get_object_or_404(Manager, id=manager_id)
        
        # Create ManagerPost for this manager with files
        obj = ManagerPost.objects.create(
            user=manager,
            experience_letter=request.FILES.get('experience_letter'),
            offer_letter=request.FILES.get('offer_letter'),
            education_certificate=request.FILES.get('education_certificate'),
            skill_certificate=request.FILES.get('skill_certificate')
        )
        
        # Send email notification
        try:
            from administration.email_notifications import send_document_submission_notification
            send_document_submission_notification(obj, user_type='manager')
        except Exception as e:
            print(f"Error sending document submission notification: {str(e)}")
        
        messages.success(request, 'Manager documents uploaded successfully!')
        return redirect("manager_document_View", company_id=company_id, company_staff_id=company_staff_id)

    # GET request - display the page
    if company_id:
        document_list = ManagerPost.objects.filter(user__user__company__id=company_id)
        return render(request, 'administration/manager-all-documents.html', {
            'document_list': document_list,
            'managers': managers_list,
            'company_id': company_id,
            'company_staff_id': company_staff_id
        })


# Employee Documents View - EXACTLY like manager
def all_documents(request, company_id, company_staff_id):
    # Handle AJAX POST request for document details
    if request.method == "POST" and request.content_type == 'application/json':
        data = json.loads(request.body.decode('utf-8'))
        document_obj_id = data.get('id', None)
        if document_obj_id:
            document_obj = Post.objects.get(pk=document_obj_id)
            return JsonResponse(document_obj.to_json())
    
    # Fetch employees for the dropdown (filtered by company) - EXACTLY like All_Employee_View line 121
    if company_id:
        employees_list = Employee.objects.filter(user__company__id=company_id).order_by('employee_first_name')
    else:
        employees_list = Employee.objects.none()
    
    # Handle form POST request for document upload
    if request.method == "POST" and 'employee' in request.POST:
        employee_id = request.POST.get("employee")
        if not employee_id:
            messages.error(request, 'Please select an employee.')
            document_list = Post.objects.filter(user__user__company__id=company_id) if company_id else Post.objects.none()
            return render(request, 'administration/employee-all-documents.html', {
                'document_list': document_list,
                'employees': employees_list,
                'company_id': company_id,
                'company_staff_id': company_staff_id
            })
        
        # Check if at least one file is provided
        has_files = any(key in request.FILES for key in ['experience_letter', 'offer_letter', 'education_certificate', 'skill_certificate'])
        if not has_files:
            messages.error(request, 'Please upload at least one document.')
            document_list = Post.objects.filter(user__user__company__id=company_id) if company_id else Post.objects.none()
            return render(request, 'administration/employee-all-documents.html', {
                'document_list': document_list,
                'employees': employees_list,
                'company_id': company_id,
                'company_staff_id': company_staff_id
            })
        
        employee = get_object_or_404(Employee, id=employee_id)
        
        # Create Post for this employee with files (EXACTLY like manager)
        obj = Post.objects.create(
            user=employee,
            experience_letter=request.FILES.get('experience_letter'),
            offer_letter=request.FILES.get('offer_letter'),
            education_certificate=request.FILES.get('education_certificate'),
            skill_certificate=request.FILES.get('skill_certificate')
        )
        
        # Send email notification
        try:
            from administration.email_notifications import send_document_submission_notification
            send_document_submission_notification(obj, user_type='employee')
        except Exception as e:
            print(f"Error sending document submission notification: {str(e)}")
        
        messages.success(request, 'Employee documents uploaded successfully!')
        return redirect("all_documents", company_id=company_id, company_staff_id=company_staff_id)

    # GET request - display the page
    if company_id:
        document_list = Post.objects.filter(user__user__company__id=company_id)
        return render(request, 'administration/employee-all-documents.html', {
            'document_list': document_list,
            'employees': employees_list,
            'company_id': company_id,
            'company_staff_id': company_staff_id
        })


@custom_login_required
def delete_employee_documents(request, company_id, company_staff_id, id):
    """Delete employee documents"""
    if request.method == "GET":
        try:
            doc = Post.objects.get(id=id)
            doc.delete()
            messages.success(request, 'Employee documents deleted successfully!')
        except Post.DoesNotExist:
            messages.error(request, 'Documents not found!')
    
    return redirect("all_documents", company_id=company_id, company_staff_id=company_staff_id)


class ManagerPostListView(ListView):
    def dispatch(self, request, company_id, company_staff_id, *args, **kwargs):
        print('Dispatch function called')
        company_staff = CompanyStaff.objects.filter(pk=company_staff_id)
        if company_staff.exists():
            if company_staff.first().is_authenticated:
                return super().dispatch(request, company_id, company_staff_id, *args, **kwargs)
            else:
                return redirect('/')
        else:
            return redirect('/')

    model = ManagerPost
    template_name = 'administration/manager-all-documents.html'
    context_object_name = 'posts'
    ordering = ['-date_posted']
    paginate_by = 2


class MPostListView(LoginRequiredMixin, ListView):
    model = ManagerPost
    template_name = 'administration/manager-all-documents.html'
    context_object_name = 'posts'
    paginate_by = 2

    def get_queryset(self):
        queryset = super(MPostListView, self).get_queryset()
        queryset = ManagerPost.objects.filter(user=self.request.user.manager)
        return queryset


def MPostDetailView(request,company_id, company_staff_id,id):
    if request.method == "POST":
        data = json.loads(request.body.decode('utf-8'))
        document_obj_id = data.get('id', None)
        document_obj = Post.objects.get(pk=document_obj_id)
        return JsonResponse(document_obj.to_json())

    # Old Code
    if company_id:
        document_list = ManagerPost.objects.filter(id=id)
        return render(request, 'administration/manager_documents.html',
                      {'document_list': document_list,'company_id':company_id, 'company_staff_id':company_staff_id})


class MPostDeleteView(View):
    def get(self, request,company_id, company_staff_id, id):
        if company_id:
            posts = ManagerPost.objects.get(id=id)
            posts.delete()
            messages.success(request, f"{posts} deleted successfully")
            return redirect(f'/administration/manager_document_View/{company_id}/{company_staff_id}')


def sendemail(request, company_id, company_staff_id):
    if company_id:
        context = {}
        ch = CompanyStaff.objects.filter(id=company_staff_id)
        if len(ch) > 0:
            data = CompanyStaff.objects.get(id=company_staff_id)
            context["data"] = data

        if request.method == "POST":

            rec = request.POST["to"].split(",")
            print(rec)
            sub = request.POST["sub"]
            msz = request.POST["msz"]

            try:
                em = EmailMessage(sub, msz, to=rec)
                em.send()
                context["status"] = "Email Sent"
                context["cls"] = "alert-success"
            except:
                context["status"] = "Could not Send, Please check Internet Connection / Email Address"
                context["cls"] = "alert-danger"
                context['company_id'] = company_id
                context['company_staff_id'] = company_staff_id
        return render(request, "administration/sendemail.html", context)
    

def Notification_Edit_View(request,company_id, company_staff_id):
    if company_id:
        if request.method == "GET":
            id = request.GET.get('id')
            notification_obj = notification.objects.get(pk=id)
            return JsonResponse(notification_obj.to_json())

        elif request.method == "POST":
            notification_models_fields_list = [f.name for f in notification._meta.get_fields()]
            notification_models_fields_dict = {}
            notification_obj_id = request.POST.get('id')
            notification_obj = notification.objects.filter(pk=notification_obj_id)

            for key, value in request.POST.items():
                if key in notification_models_fields_list and key != 'id' and key != 'id' and value is not None and len(
                        value) != 0:
                    print(key, value)
                    notification_models_fields_dict.setdefault(key, value)
            notification_obj.update(**notification_models_fields_dict)
            emp_id = request.POST.get('id')

            print('notification id is-')
            print(emp_id)
            return redirect(f'/administration/notifications/{company_id}/{company_staff_id}')
        
class NotificationRemove(View):
    def get(self, request,company_id, company_staff_id, id):
        if company_id:
            notifications = notification.objects.get(id=id)
            notifications.delete()
            messages.success(request, f"{notifications} deleted successfully")
            return redirect(f'/administration/notifications/{company_id}/{company_staff_id}')


def Managernotifications(request,company_id, company_staff_id):
    if company_id:
        if request.method == "POST":
            notifications = request.POST.get("notifications")
            assign_id = request.POST.get("manager_id")
            
            # Debug: Print received values
            print(f"DEBUG: POST data received")
            print(f"  - notifications: {notifications}")
            print(f"  - manager_id: {assign_id}")
            print(f"  - All POST keys: {list(request.POST.keys())}")
            
            # Validate that manager_id is provided and not empty
            if not assign_id or assign_id == "Select Manager" or assign_id == "":
                messages.error(request, 'Please select a manager.')
                return render(request,"administration/managernotification.html",{
                    'assigned':Manager.objects.filter(user__company__id=company_id),
                    'company_id':company_id, 
                    'company_staff_id':company_staff_id
                })
            
            if not notifications or notifications.strip() == "":
                messages.error(request, 'Please enter a notification message.')
                return render(request,"administration/managernotification.html",{
                    'assigned':Manager.objects.filter(user__company__id=company_id),
                    'company_id':company_id, 
                    'company_staff_id':company_staff_id
                })
            
            try:
                assigned_to = Manager.objects.get(id=assign_id)
                print(f"DEBUG: Manager found: {assigned_to.manager_email}")
                
                # Create the notification
                notification_obj = ManagerNotification.objects.create(
                    notifications=notifications, 
                    user=assigned_to
                )
                print(f"DEBUG: Notification created with ID: {notification_obj.id}")
                
                # Update the manager's user notification flag
                user = assigned_to.user
                user.new_notification = True
                user.save()
                request.session["new_notification"] = user.new_notification
                print(f"DEBUG: Manager user notification flag updated")
                
                # Send email notification to manager
                email_sent = False
                try:
                    subject = 'New Notification - HR Portal'
                    message = f"""Hello {assigned_to.manager_first_name},

You have received a new notification:

{notifications}

Please log in to your HR Portal account to view more details.

Best regards,
HR Portal Team"""
                    
                    email = EmailMessage(
                        subject,
                        message,
                        to=[assigned_to.manager_email]
                    )
                    email.send()
                    print(f"DEBUG: Email sent successfully to {assigned_to.manager_email}")
                    email_sent = True
                except Exception as email_error:
                    print(f"DEBUG: Email sending failed: {email_error}")
                    import traceback
                    print(traceback.format_exc())
                    # Don't fail the whole operation if email fails
                
                if email_sent:
                    messages.success(request, f'Notification sent successfully to {assigned_to.manager_email}! (Web App & Email)')
                else:
                    messages.success(request, f'Notification saved in web app for {assigned_to.manager_email}! Email could not be sent - please configure EMAIL settings in settings.py')
                return redirect(f'/administration/notifications/{company_id}/{company_staff_id}')
                
            except Manager.DoesNotExist:
                print(f"DEBUG: Manager with ID {assign_id} not found")
                messages.error(request, 'Selected manager not found.')
            except Exception as e:
                import traceback
                print(f"DEBUG: Error in Managernotifications: {e}")
                print(traceback.format_exc())
                messages.error(request, f'Error sending notification: {str(e)}')
        
        # GET request or error - show form
        managers = Manager.objects.filter(user__company__id=company_id)
        print(f"DEBUG: Found {managers.count()} managers for company {company_id}")
        return render(request,"administration/managernotification.html",{
            'assigned':managers,
            'company_id':company_id, 
            'company_staff_id':company_staff_id
        })


# ============================================ Email Notifications ============================================

def email_notifications(request, company_id, company_staff_id):
    """View to display email notifications fetched from Gmail."""
    if company_id:
        company = Company.objects.get(id=company_id)
        email_notifs = EmailNotification.objects.filter(
            models.Q(company=company) | models.Q(company__isnull=True)
        )
        unread_count = email_notifs.filter(is_read=False).count()
        context = {
            'email_notifications': email_notifs,
            'unread_count': unread_count,
            'company_id': company_id,
            'company_staff_id': company_staff_id,
        }
        return render(request, 'administration/email_notifications.html', context)


def fetch_emails_view(request, company_id, company_staff_id):
    """View to trigger fetching emails from Gmail and redirect back."""
    if company_id:
        company = Company.objects.get(id=company_id)
        result = fetch_emails_from_gmail(company=company, max_emails=50, days_back=30)
        
        if result['new_count'] > 0:
            sweetify.success(request, f"Fetched {result['new_count']} new email(s)!", timer=3000)
        elif result['errors']:
            error_msg = result['errors'][0] if result['errors'] else 'Unknown error'
            sweetify.error(request, f"Error: {error_msg}", timer=5000)
        else:
            sweetify.info(request, "No new emails found.", timer=3000)
        
        return redirect(f'/administration/email_notifications/{company_id}/{company_staff_id}')


def mark_email_read(request, company_id, company_staff_id, id):
    """Mark a single email notification as read."""
    if company_id:
        try:
            email_notif = EmailNotification.objects.get(id=id)
            email_notif.is_read = True
            email_notif.save()
        except EmailNotification.DoesNotExist:
            pass
        return redirect(f'/administration/email_notifications/{company_id}/{company_staff_id}')


def mark_all_emails_read(request, company_id, company_staff_id):
    """Mark all email notifications as read."""
    if company_id:
        company = Company.objects.get(id=company_id)
        EmailNotification.objects.filter(
            models.Q(company=company) | models.Q(company__isnull=True),
            is_read=False
        ).update(is_read=True)
        sweetify.success(request, "All emails marked as read!", timer=2000)
        return redirect(f'/administration/email_notifications/{company_id}/{company_staff_id}')


def email_notification_detail(request, company_id, company_staff_id, id):
    """View to see the full email notification detail."""
    if company_id:
        email_notif = get_object_or_404(EmailNotification, id=id)
        # Mark as read when viewed
        if not email_notif.is_read:
            email_notif.is_read = True
            email_notif.save()
        context = {
            'email_notif': email_notif,
            'company_id': company_id,
            'company_staff_id': company_staff_id,
        }
        return render(request, 'administration/email_notification_detail.html', context)


def delete_email_notification(request, company_id, company_staff_id, id):
    """Delete a single email notification."""
    if company_id:
        try:
            email_notif = EmailNotification.objects.get(id=id)
            email_notif.delete()
            sweetify.success(request, "Email notification deleted!", timer=2000)
        except EmailNotification.DoesNotExist:
            sweetify.error(request, "Notification not found!", timer=2000)
        return redirect(f'/administration/email_notifications/{company_id}/{company_staff_id}')
