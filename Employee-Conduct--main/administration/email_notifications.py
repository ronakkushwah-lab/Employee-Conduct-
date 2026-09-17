"""
In-App Notification Service for HRMS
All system events (leaves, approvals, promotions, manager changes, attendance)
create clean in-app notifications for Employees, Managers, and Admins.
"""
import logging
from django.conf import settings
from django.utils import timezone
from datetime import datetime

logger = logging.getLogger(__name__)

COMPANY_EMAIL = getattr(settings, 'GMAIL_EMAIL', 'eic.developer.testing@gmail.com')
COMPANY_NAME = "Employee Conduct HRMS"


def create_in_app_notification(recipient_employee=None, recipient_manager=None, recipient_staff=None, message=""):
    """
    Creates an in-app notification in EmployeeNotification, ManagerNotification, or notification table.
    """
    if not message or not str(message).strip():
        return False
    msg = str(message).strip()[:500]
    created = False
    try:
        if recipient_employee:
            from managers.models import EmployeeNotification
            EmployeeNotification.objects.create(user=recipient_employee, notifications=msg)
            created = True
        if recipient_manager:
            from administration.models import ManagerNotification
            ManagerNotification.objects.create(user=recipient_manager, notifications=msg)
            created = True
        if recipient_staff:
            from administration.models import notification as AdminNotif
            AdminNotif.objects.create(user=recipient_staff, notification=msg)
            created = True
    except Exception as e:
        logger.exception("create_in_app_notification error: %s", str(e))
    return created


def send_simple_email_to_manager(manager_email, subject, plain_message, async_send=True):
    """
    In-app notification dispatcher for Manager plain text notifications.
    """
    try:
        from managers.models import Manager
        manager = Manager.objects.filter(manager_email=manager_email).first()
        if not manager and manager_email:
            manager = Manager.objects.filter(user__email=manager_email).first()
        if manager:
            return create_in_app_notification(recipient_manager=manager, message=f"{subject}: {plain_message}")
        print("IN_APP_MANAGER_NOTIF:", manager_email, subject, flush=True)
        return True
    except Exception as e:
        logger.exception("send_simple_email_to_manager failed: %s", str(e))
        return False


def send_email_notification(subject, recipient_email, template_name, context, recipient_name=None, async_send=True):
    """
    Generic in-app notification dispatcher.
    Resolves recipient to Employee, Manager, or Admin staff and creates in-app notification.
    """
    if not recipient_email or not str(recipient_email).strip():
        return False
    email = str(recipient_email).strip()
    msg = f"{subject}"
    if context and isinstance(context, dict):
        details = []
        if 'leave_type' in context:
            details.append(f"Type: {context['leave_type']}")
        if 'start_date' in context and 'end_date' in context:
            details.append(f"Dates: {context['start_date']} to {context['end_date']}")
        if 'reason' in context and context['reason'] != 'N/A':
            details.append(f"Reason: {context['reason']}")
        if details:
            msg += f" ({', '.join(details)})"

    try:
        from employee.models import Employee
        from managers.models import Manager
        from account.models import CompanyStaff

        emp = Employee.objects.filter(employee_email=email).first() or Employee.objects.filter(user__email=email).first()
        mgr = Manager.objects.filter(manager_email=email).first() or Manager.objects.filter(user__email=email).first()
        staff = CompanyStaff.objects.filter(email=email).first()

        create_in_app_notification(recipient_employee=emp, recipient_manager=mgr, recipient_staff=staff, message=msg)
        return True
    except Exception as e:
        logger.exception("send_email_notification in-app dispatch failed: %s", str(e))
        return False


def send_attendance_notification(attendance, action='check_in'):
    """
    In-app notification for attendance check-in or check-out.
    """
    try:
        employee = attendance.employee
        if not employee:
            return False
        
        recipient_name = f"{employee.employee_first_name} {employee.employee_last_name}"
        time_str = attendance.check_in.strftime('%I:%M %p') if (action == 'check_in' and attendance.check_in) else (attendance.check_out.strftime('%I:%M %p') if attendance.check_out else 'N/A')
        date_str = attendance.check_in.strftime('%B %d, %Y') if attendance.check_in else timezone.now().strftime('%B %d, %Y')
        
        # 1. Notify Employee in-app
        emp_msg = f"Attendance {action.replace('_', '-').title()} recorded at {time_str} on {date_str}."
        create_in_app_notification(recipient_employee=employee, message=emp_msg)
        
        # 2. Notify Manager in-app if exists
        if employee.employee_reports_to:
            mgr = employee.employee_reports_to
            mgr_msg = f"Team Member {recipient_name} recorded {action.replace('_', '-').title()} at {time_str} ({date_str})."
            create_in_app_notification(recipient_manager=mgr, message=mgr_msg)
            
        return True
    except Exception as e:
        logger.exception("Error sending attendance in-app notification: %s", str(e))
        return False


def send_leave_applied_notification_to_manager(leave, manager):
    """
    In-app notification created for Manager when Employee applies for leave.
    """
    if not manager:
        return False
    try:
        from employee.models import Employee
        if not hasattr(leave, 'user') or not isinstance(leave.user, Employee):
            return False
        employee_name = f"{leave.user.employee_first_name} {leave.user.employee_last_name}"
        start_str = leave.startdate.strftime('%b %d, %Y') if leave.startdate else 'N/A'
        end_str = leave.enddate.strftime('%b %d, %Y') if leave.enddate else 'N/A'
        
        msg = f"Employee {employee_name} has applied for {leave.leavetype.title()} leave ({start_str} to {end_str}). Reason: {leave.reason or 'N/A'}."
        return create_in_app_notification(recipient_manager=manager, message=msg)
    except Exception as e:
        logger.exception("send_leave_applied_notification_to_manager in-app failed: %s", str(e))
        return False


def send_resignation_submission_notification_to_manager(resign, manager):
    """
    In-app notification created for Manager when Employee applies for resignation.
    """
    if not manager:
        return False
    try:
        from employee.models import Employee
        if not hasattr(resign, 'user') or not isinstance(resign.user, Employee):
            return False
        employee_name = f"{resign.user.employee_first_name} {resign.user.employee_last_name}"
        msg = f"Resignation Notice: Employee {employee_name} submitted a resignation request. Reason: {resign.reason or 'N/A'}."
        return create_in_app_notification(recipient_manager=manager, message=msg)
    except Exception as e:
        logger.exception("send_resignation_submission_notification_to_manager in-app failed: %s", str(e))
        return False


def send_leave_submission_notification(leave, manager=None):
    """
    In-app notification when leave request is submitted.
    """
    try:
        from managers.models import Manager
        from employee.models import Employee

        start_str = leave.startdate.strftime('%b %d, %Y') if leave.startdate else 'N/A'
        end_str = leave.enddate.strftime('%b %d, %Y') if leave.enddate else 'N/A'

        if hasattr(leave, 'user'):
            if isinstance(leave.user, Manager):
                mgr_msg = f"Your {leave.leavetype.title()} leave request ({start_str} to {end_str}) has been submitted successfully."
                create_in_app_notification(recipient_manager=leave.user, message=mgr_msg)
            elif isinstance(leave.user, Employee):
                emp_msg = f"Your {leave.leavetype.title()} leave request ({start_str} to {end_str}) has been submitted successfully."
                create_in_app_notification(recipient_employee=leave.user, message=emp_msg)
                
                mgr_target = manager or getattr(leave.user, 'employee_reports_to', None)
                if mgr_target:
                    send_leave_applied_notification_to_manager(leave, mgr_target)
        return True
    except Exception as e:
        logger.exception("Error in send_leave_submission_notification: %s", str(e))
        return False


def send_leave_approval_notification(leave, approved=True):
    """
    In-app notification when leave is approved or rejected.
    """
    try:
        status_str = "Approved" if approved else "Rejected"
        start_str = leave.startdate.strftime('%b %d, %Y') if leave.startdate else 'N/A'
        end_str = leave.enddate.strftime('%b %d, %Y') if leave.enddate else 'N/A'
        msg = f"Your {leave.leavetype.title()} leave request ({start_str} to {end_str}) has been {status_str}."

        if hasattr(leave, 'user'):
            from managers.models import Manager
            from employee.models import Employee
            
            if isinstance(leave.user, Manager):
                create_in_app_notification(recipient_manager=leave.user, message=msg)
            elif isinstance(leave.user, Employee):
                create_in_app_notification(recipient_employee=leave.user, message=msg)
        return True
    except Exception as e:
        logger.exception("Error in send_leave_approval_notification: %s", str(e))
        return False


def send_manager_change_notification(employee, old_manager=None, new_manager=None):
    """
    In-app notification when employee's manager is updated.
    """
    try:
        employee_name = f"{employee.employee_first_name} {employee.employee_last_name}"
        new_mgr_name = f"{new_manager.manager_first_name} {new_manager.manager_last_name}" if new_manager else "None"
        
        # Notify employee
        create_in_app_notification(recipient_employee=employee, message=f"Your reporting manager has been updated to {new_mgr_name}.")
        
        # Notify new manager
        if new_manager:
            create_in_app_notification(recipient_manager=new_manager, message=f"Employee {employee_name} has been assigned to your team.")
            
        # Notify old manager
        if old_manager:
            create_in_app_notification(recipient_manager=old_manager, message=f"Employee {employee_name} is no longer assigned to your team.")
            
        return True
    except Exception as e:
        logger.exception("Error in send_manager_change_notification: %s", str(e))
        return False


def send_new_user_notification(user, user_type='employee', password=None):
    """
    In-app notification when a new user is created.
    """
    try:
        from employee.models import Employee
        from managers.models import Manager
        
        msg = f"Welcome to {COMPANY_NAME}! Your account has been created successfully."
        if user_type == 'employee':
            emp = Employee.objects.filter(employee_email=user.email).first()
            if emp:
                create_in_app_notification(recipient_employee=emp, message=msg)
        elif user_type == 'manager':
            mgr = Manager.objects.filter(manager_email=user.email).first()
            if mgr:
                create_in_app_notification(recipient_manager=mgr, message=msg)
        create_in_app_notification(recipient_staff=user, message=msg)
        return True
    except Exception as e:
        logger.exception("Error in send_new_user_notification: %s", str(e))
        return False


def send_document_submission_notification(document, user_type='employee'):
    """
    In-app notification when documents are uploaded.
    """
    try:
        if user_type == 'employee':
            user = document.user
            user_name = f"{user.employee_first_name} {user.employee_last_name}"
            msg = f"Your document submission has been received successfully."
            create_in_app_notification(recipient_employee=user, message=msg)
            if user.employee_reports_to:
                mgr_msg = f"Employee {user_name} uploaded updated documents."
                create_in_app_notification(recipient_manager=user.employee_reports_to, message=mgr_msg)
        else:
            user = document.user
            msg = f"Your manager documents have been updated successfully."
            create_in_app_notification(recipient_manager=user, message=msg)
        return True
    except Exception as e:
        logger.exception("Error in send_document_submission_notification: %s", str(e))
        return False


def send_leave_manager_approval_notification(leave):
    """
    In-app notification when manager approves leave and forwards to HR.
    """
    try:
        from employee.models import Employee
        if not hasattr(leave, 'user') or not isinstance(leave.user, Employee):
            return False

        employee = leave.user
        employee_name = f"{employee.employee_first_name} {employee.employee_last_name}"
        manager = getattr(leave, 'manager', None) or getattr(employee, 'employee_reports_to', None)
        manager_name = f"{manager.manager_first_name} {manager.manager_last_name}" if manager else "Reporting Manager"

        # 1. Notify employee
        create_in_app_notification(
            recipient_employee=employee,
            message=f"Your {leave.leavetype.title()} leave request was approved by {manager_name} and is pending HR approval."
        )

        # 2. Notify HR staff in-app
        from account.models import CompanyStaff
        hr_staffs = CompanyStaff.objects.filter(is_admin=True, is_active=True)
        for hs in hr_staffs:
            create_in_app_notification(
                recipient_staff=hs,
                message=f"Action Required: Manager {manager_name} approved {employee_name}'s {leave.leavetype.title()} leave request (Pending HR approval)."
            )
        return True
    except Exception as e:
        logger.exception("send_leave_manager_approval_notification in-app failed: %s", str(e))
        return False


def send_promotion_notification(manager_instance, new_designation=None, new_department=None, promoted_by=None):
    """
    In-app notification when an employee is promoted to Manager.
    """
    try:
        designation = new_designation or manager_instance.manager_designation or 'Manager'
        dept_name = str(new_department or manager_instance.manager_department or 'Management')
        
        msg = f"Congratulations! You have been promoted to {designation} in the {dept_name} Department. Your account has been upgraded with Manager privileges."
        create_in_app_notification(recipient_manager=manager_instance, message=msg)
        return True
    except Exception as e:
        logger.exception("send_promotion_notification in-app failed: %s", str(e))
        return False
