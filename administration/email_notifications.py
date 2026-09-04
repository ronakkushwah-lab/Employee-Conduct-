"""
Email Notification Service for HRMS
Handles sending email notifications for various events in the system.
"""
import logging
from django.core.mail import send_mail, EmailMultiAlternatives
from django.template.loader import render_to_string
from django.conf import settings
from django.utils.html import strip_tags
from django.utils import timezone
from datetime import datetime

logger = logging.getLogger(__name__)

COMPANY_EMAIL = getattr(settings, 'GMAIL_EMAIL', 'eic.developer.testing@gmail.com')
COMPANY_NAME = "Employee Conduct HRMS"


def send_simple_email_to_manager(manager_email, subject, plain_message):
    """
    Sirf plain text email – koi template nahi. Manager tak notification pahunchane ke liye.
    Isse pakka pata chalega email ja raha hai ya nahi.
    """
    import sys
    if not manager_email or not str(manager_email).strip():
        print("MANAGER_EMAIL_EMPTY", flush=True)
        return False
    to_email = str(manager_email).strip()
    try:
        send_mail(
            subject=subject,
            message=plain_message,
            from_email=COMPANY_EMAIL,
            recipient_list=[to_email],
            fail_silently=False,
        )
        print("MANAGER_EMAIL_SENT_TO:", to_email, flush=True)
        return True
    except Exception as e:
        print("MANAGER_EMAIL_FAILED:", to_email, str(e), flush=True)
        logger.exception("send_simple_email_to_manager failed: %s", str(e))
        return False


def send_email_notification(subject, recipient_email, template_name, context, recipient_name=None):
    """
    Generic function to send email notifications with HTML templates.
    Tries EmailMultiAlternatives first; on failure tries plain send_mail as fallback.
    """
    if not recipient_email or not str(recipient_email).strip():
        logger.warning("send_email_notification: recipient_email is empty, skipping.")
        return False
    recipient_email = str(recipient_email).strip()
    try:
        context.update({
            'company_email': COMPANY_EMAIL,
            'company_name': COMPANY_NAME,
            'recipient_name': recipient_name or 'User',
            'current_date': timezone.now().strftime('%B %d, %Y'),
            'current_time': timezone.now().strftime('%I:%M %p'),
        })
        html_content = render_to_string(f'administration/emails/{template_name}.html', context)
        text_content = strip_tags(html_content)
        msg = EmailMultiAlternatives(
            subject=subject,
            body=text_content,
            from_email=COMPANY_EMAIL,
            to=[recipient_email],
        )
        msg.attach_alternative(html_content, "text/html")
        msg.send(fail_silently=False)
        logger.info("Email sent successfully to %s: %s", recipient_email, subject)
        print(f"[Email] Sent to {recipient_email}: {subject}")
        return True
    except Exception as e:
        logger.exception("Error sending email to %s: %s", recipient_email, str(e))
        print(f"[Email] First attempt failed to {recipient_email}: {e}")
        try:
            fallback_body = "You have a new notification. Please log in to the HRMS portal for details."
            send_mail(
                subject=subject,
                message=fallback_body,
                from_email=COMPANY_EMAIL,
                recipient_list=[recipient_email],
                fail_silently=False,
            )
            logger.info("Email sent (fallback) to %s: %s", recipient_email, subject)
            print(f"[Email] Fallback sent to {recipient_email}: {subject}")
            return True
        except Exception as e2:
            logger.exception("Fallback send also failed to %s: %s", recipient_email, str(e2))
            print(f"[Email] Fallback also failed to {recipient_email}: {e2}")
            return False


def send_attendance_notification(attendance, action='check_in'):
    """
    Send email notification for attendance check-in or check-out.
    
    Args:
        attendance: Attendance model instance
        action: 'check_in' or 'check_out'
    """
    try:
        employee = attendance.employee
        if not employee:
            return False
        
        recipient_email = employee.employee_email
        recipient_name = f"{employee.employee_first_name} {employee.employee_last_name}"
        
        # Get manager email if exists
        manager_email = None
        manager_name = None
        if employee.employee_reports_to:
            manager = employee.employee_reports_to
            manager_email = manager.manager_email
            manager_name = f"{manager.manager_first_name} {manager.manager_last_name}"
        
        if action == 'check_in':
            subject = f"Attendance Check-In Confirmation - {timezone.now().strftime('%B %d, %Y')}"
            time_str = attendance.check_in.strftime('%I:%M %p') if attendance.check_in else 'N/A'
        else:
            subject = f"Attendance Check-Out Confirmation - {timezone.now().strftime('%B %d, %Y')}"
            time_str = attendance.check_out.strftime('%I:%M %p') if attendance.check_out else 'N/A'
        
        context = {
            'employee_name': recipient_name,
            'employee_id': employee.employee_id,
            'action': action,
            'time': time_str,
            'date': attendance.check_in.strftime('%B %d, %Y') if attendance.check_in else timezone.now().strftime('%B %d, %Y'),
            'check_in_time': attendance.check_in.strftime('%I:%M %p') if attendance.check_in else 'N/A',
            'check_out_time': attendance.check_out.strftime('%I:%M %p') if attendance.check_out else 'N/A',
            'manager_name': manager_name,
            'manager_email': manager_email,
        }
        
        # Send to employee
        send_email_notification(
            subject=subject,
            recipient_email=recipient_email,
            template_name='attendance_notification',
            context=context,
            recipient_name=recipient_name
        )
        
        # Also notify manager if exists
        if manager_email and manager_name:
            manager_subject = f"Employee {action.replace('_', '-').title()} Notification - {recipient_name}"
            send_email_notification(
                subject=manager_subject,
                recipient_email=manager_email,
                template_name='attendance_notification_manager',
                context=context,
                recipient_name=manager_name
            )
        
        return True
    except Exception as e:
        print(f"Error sending attendance notification: {str(e)}")
        return False


def send_leave_applied_notification_to_manager(leave, manager):
    """
    Leave apply hote hi selected manager ke email par notification bhejo.
    Manager wahi hota hai jo employee ne dropdown me select kiya (jiska email dropdown me dikh raha hai).
    """
    if not manager:
        return False
    manager_email = (getattr(manager, 'manager_email', None) or '').strip()
    if not manager_email and getattr(manager, 'user', None):
        manager_email = (getattr(manager.user, 'email', None) or '').strip()
    if not manager_email:
        logger.warning("Leave: manager has no email (manager_id=%s)", getattr(manager, 'id', None))
        return False
    print(f"[Leave] Manager ko bhej rahe hain: {manager_email}")
    try:
        from employee.models import Employee
        if not hasattr(leave, 'user') or not isinstance(leave.user, Employee):
            return False
        employee_name = f"{leave.user.employee_first_name} {leave.user.employee_last_name}"
        employee_id = getattr(leave.user, 'employee_id', 'N/A')
        manager_name = f"{manager.manager_first_name} {manager.manager_last_name}"
        context = {
            'manager_name': manager_name,
            'employee_name': employee_name,
            'employee_id': employee_id,
            'leave_type': (leave.leavetype or '').title(),
            'start_date': leave.startdate.strftime('%B %d, %Y') if leave.startdate else 'N/A',
            'end_date': leave.enddate.strftime('%B %d, %Y') if leave.enddate else 'N/A',
            'reason': leave.reason or 'N/A',
            'description': leave.description or 'N/A',
            'status': getattr(leave, 'status', 'pending'),
            'leave_days': (leave.enddate - leave.startdate).days + 1 if (leave.startdate and leave.enddate) else 0,
        }
        subject = f"Employee {employee_name} ne leave apply ki hai – Approve / Reject karein"
        logger.info("Sending leave-applied notification to manager: %s", manager_email)
        print(f"[Leave] Manager ko notification bhej rahe hain is email par: {manager_email}")
        return send_email_notification(
            subject=subject,
            recipient_email=manager_email,
            template_name='leave_submission_manager',
            context=context,
            recipient_name=manager_name
        )
    except Exception as e:
        logger.exception("send_leave_applied_notification_to_manager failed: %s", str(e))
        return False


def send_resignation_submission_notification_to_manager(resign, manager):
    """
    Employee ne resignation apply karte hi Reporting to (selected manager) ke email par notification bhejo.
    """
    if not manager:
        return False
    manager_email = (getattr(manager, 'manager_email', None) or '').strip()
    if not manager_email and getattr(manager, 'user', None):
        manager_email = (getattr(manager.user, 'email', None) or '').strip()
    if not manager_email:
        logger.warning("Resignation: manager has no email (manager_id=%s)", getattr(manager, 'id', None))
        return False
    print(f"[Resignation] Manager ko bhej rahe hain: {manager_email}")
    try:
        from employee.models import Employee
        if not hasattr(resign, 'user') or not isinstance(resign.user, Employee):
            return False
        employee_name = f"{resign.user.employee_first_name} {resign.user.employee_last_name}"
        employee_id = getattr(resign.user, 'employee_id', 'N/A')
        manager_name = f"{manager.manager_first_name} {manager.manager_last_name}"
        context = {
            'manager_name': manager_name,
            'employee_name': employee_name,
            'employee_id': employee_id,
            'resignation_date': resign.startdate.strftime('%B %d, %Y') if resign.startdate else 'N/A',
            'reason': resign.reason or 'N/A',
            'status': getattr(resign, 'status', 'pending'),
        }
        subject = f"Resignation Application – {employee_name} ne resignation apply ki hai"
        logger.info("Sending resignation notification to manager: %s", manager_email)
        return send_email_notification(
            subject=subject,
            recipient_email=manager_email,
            template_name='resignation_submission_manager',
            context=context,
            recipient_name=manager_name
        )
    except Exception as e:
        logger.exception("send_resignation_submission_notification_to_manager failed: %s", str(e))
        return False


def send_leave_submission_notification(leave, manager=None):
    """
    Send email notification when a leave request is submitted.
    Employee gets confirmation; selected manager gets notification to approve/reject.

    Args:
        leave: Leave or ManagerLeave model instance
        manager: Optional Manager instance (pass from view when employee submits leave so manager gets email immediately)
    """
    try:
        # Handle both Leave and ManagerLeave models
        if hasattr(leave, 'user'):
            # Check if it's a ManagerLeave (user is Manager) or regular Leave (user is Employee)
            from managers.models import Manager
            from employee.models import Employee

            if isinstance(leave.user, Manager):
                # ManagerLeave
                employee_name = f"{leave.user.manager_first_name} {leave.user.manager_last_name}"
                employee_email = leave.user.manager_email
                employee_id = leave.user.manager_id
            elif isinstance(leave.user, Employee):
                # Regular Leave
                employee_name = f"{leave.user.employee_first_name} {leave.user.employee_last_name}"
                employee_email = leave.user.employee_email
                employee_id = leave.user.employee_id
            else:
                return False
        else:
            return False

        subject = f"Leave Request Submitted - {leave.leavetype.title()} Leave"

        context = {
            'employee_name': employee_name,
            'employee_id': employee_id,
            'leave_type': leave.leavetype.title(),
            'start_date': leave.startdate.strftime('%B %d, %Y') if leave.startdate else 'N/A',
            'end_date': leave.enddate.strftime('%B %d, %Y') if leave.enddate else 'N/A',
            'reason': leave.reason or 'N/A',
            'description': leave.description or 'N/A',
            'status': leave.status,
            'leave_days': (leave.enddate - leave.startdate).days + 1 if leave.startdate and leave.enddate else 0,
        }

        # 1. Send to employee (confirmation) – jese hi apply kare, employee ko confirmation
        send_email_notification(
            subject=subject,
            recipient_email=employee_email,
            template_name='leave_submission',
            context=context,
            recipient_name=employee_name
        )

        # 2. Manager ko notification – jab view se manager pass nahi hua (e.g. manager leave) tab yahan se bhejo
        # Jab employee leave apply kare to view send_leave_applied_notification_to_manager() call karta hai, isliye yahan duplicate na bhejein
        if manager is None and isinstance(leave.user, Employee):
            manager_to_notify = getattr(leave, 'manager', None) or (Manager.objects.filter(id=leave.manager_id).first() if getattr(leave, 'manager_id', None) else None)
            if not manager_to_notify and getattr(leave.user, 'employee_reports_to', None):
                manager_to_notify = leave.user.employee_reports_to
            if manager_to_notify and getattr(manager_to_notify, 'manager_email', None):
                manager_email = (manager_to_notify.manager_email or '').strip()
                if manager_email:
                    manager_subject = f"Leave Request – Approve or Reject: {employee_name}"
                    manager_context = context.copy()
                    manager_context['manager_name'] = f"{manager_to_notify.manager_first_name} {manager_to_notify.manager_last_name}"
                    send_email_notification(
                        subject=manager_subject,
                        recipient_email=manager_email,
                        template_name='leave_submission_manager',
                        context=manager_context,
                        recipient_name=manager_context['manager_name']
                    )
        
        return True
    except Exception as e:
        logger.exception("Error in send_leave_submission_notification: %s", str(e))
        print(f"Error sending leave submission notification: {str(e)}")
        return False


def send_leave_approval_notification(leave, approved=True):
    """
    Send email notification when a leave request is approved or rejected.
    
    Args:
        leave: Leave or ManagerLeave model instance
        approved: True if approved, False if rejected
    """
    try:
        # Handle both Leave and ManagerLeave models
        if hasattr(leave, 'user'):
            from managers.models import Manager
            from employee.models import Employee
            
            if isinstance(leave.user, Manager):
                # ManagerLeave
                employee_name = f"{leave.user.manager_first_name} {leave.user.manager_last_name}"
                employee_email = leave.user.manager_email
                employee_id = leave.user.manager_id
            elif isinstance(leave.user, Employee):
                # Regular Leave
                employee_name = f"{leave.user.employee_first_name} {leave.user.employee_last_name}"
                employee_email = leave.user.employee_email
                employee_id = leave.user.employee_id
            else:
                return False
        else:
            return False
        
        status = 'Approved' if approved else 'Rejected'
        subject = f"Leave Request {status} - {leave.leavetype.title()} Leave"
        
        context = {
            'employee_name': employee_name,
            'employee_id': employee_id,
            'leave_type': leave.leavetype.title(),
            'start_date': leave.startdate.strftime('%B %d, %Y') if leave.startdate else 'N/A',
            'end_date': leave.enddate.strftime('%B %d, %Y') if leave.enddate else 'N/A',
            'reason': leave.reason or 'N/A',
            'status': status.lower(),
            'approved': approved,
            'leave_days': (leave.enddate - leave.startdate).days + 1 if leave.startdate and leave.enddate else 0,
        }
        
        send_email_notification(
            subject=subject,
            recipient_email=employee_email,
            template_name='leave_approval',
            context=context,
            recipient_name=employee_name
        )
        
        return True
    except Exception as e:
        print(f"Error sending leave approval notification: {str(e)}")
        return False


def send_manager_change_notification(employee, old_manager=None, new_manager=None):
    """
    Send email notification when an employee's manager is changed.
    
    Args:
        employee: Employee model instance
        old_manager: Previous Manager instance (optional)
        new_manager: New Manager instance (optional)
    """
    try:
        employee_name = f"{employee.employee_first_name} {employee.employee_last_name}"
        employee_email = employee.employee_email
        
        old_manager_name = f"{old_manager.manager_first_name} {old_manager.manager_last_name}" if old_manager else 'N/A'
        new_manager_name = f"{new_manager.manager_first_name} {new_manager.manager_last_name}" if new_manager else 'N/A'
        old_manager_email = old_manager.manager_email if old_manager else None
        new_manager_email = new_manager.manager_email if new_manager else None
        
        subject = f"Manager Assignment Update - {employee_name}"
        
        context = {
            'employee_name': employee_name,
            'employee_id': employee.employee_id,
            'old_manager_name': old_manager_name,
            'new_manager_name': new_manager_name,
            'old_manager_email': old_manager_email,
            'new_manager_email': new_manager_email,
        }
        
        # Notify employee
        send_email_notification(
            subject=subject,
            recipient_email=employee_email,
            template_name='manager_change',
            context=context,
            recipient_name=employee_name
        )
        
        # Notify old manager if exists
        if old_manager_email and old_manager:
            old_manager_subject = f"Employee Manager Assignment Changed - {employee_name}"
            send_email_notification(
                subject=old_manager_subject,
                recipient_email=old_manager_email,
                template_name='manager_change_old_manager',
                context=context,
                recipient_name=old_manager_name
            )
        
        # Notify new manager if exists
        if new_manager_email and new_manager:
            new_manager_subject = f"New Employee Assignment - {employee_name}"
            send_email_notification(
                subject=new_manager_subject,
                recipient_email=new_manager_email,
                template_name='manager_change_new_manager',
                context=context,
                recipient_name=new_manager_name
            )
        
        return True
    except Exception as e:
        print(f"Error sending manager change notification: {str(e)}")
        return False


def send_new_user_notification(user, user_type='employee', password=None):
    """
    Send email notification when a new user is added to the system.
    
    Args:
        user: CompanyStaff model instance
        user_type: 'employee', 'manager', or 'admin'
        password: Optional password (for new accounts)
    """
    try:
        recipient_email = user.email
        recipient_name = user.full_name or user.email
        
        # Get additional details based on user type
        if user_type == 'employee':
            try:
                employee = user.employee
                recipient_name = f"{employee.employee_first_name} {employee.employee_last_name}"
                employee_id = employee.employee_id
                department = employee.employee_department.name if employee.employee_department else 'N/A'
                designation = employee.employee_designation
            except:
                employee_id = 'N/A'
                department = 'N/A'
                designation = 'N/A'
        elif user_type == 'manager':
            try:
                manager = user.manager
                recipient_name = f"{manager.manager_first_name} {manager.manager_last_name}"
                employee_id = manager.manager_id
                department = manager.manager_department.name if manager.manager_department else 'N/A'
                designation = manager.manager_designation
            except:
                employee_id = 'N/A'
                department = 'N/A'
                designation = 'N/A'
        else:
            employee_id = 'N/A'
            department = 'N/A'
            designation = 'N/A'
        
        subject = f"Welcome to {COMPANY_NAME} - Account Created"
        
        context = {
            'user_name': recipient_name,
            'user_email': recipient_email,
            'user_type': user_type.title(),
            'employee_id': employee_id,
            'department': department,
            'designation': designation,
            'password': password,  # Only include if provided
            'has_password': password is not None,
        }
        
        send_email_notification(
            subject=subject,
            recipient_email=recipient_email,
            template_name='new_user',
            context=context,
            recipient_name=recipient_name
        )
        
        return True
    except Exception as e:
        print(f"Error sending new user notification: {str(e)}")
        return False


def send_document_submission_notification(document, user_type='employee'):
    """
    Send email notification when documents are submitted.
    
    Args:
        document: Post or ManagerPost model instance
        user_type: 'employee' or 'manager'
    """
    try:
        if user_type == 'employee':
            user = document.user
            user_name = f"{user.employee_first_name} {user.employee_last_name}"
            user_email = user.employee_email
            user_id = user.employee_id
            
            # Get manager if exists
            manager_email = None
            manager_name = None
            if user.employee_reports_to:
                manager = user.employee_reports_to
                manager_email = manager.manager_email
                manager_name = f"{manager.manager_first_name} {manager.manager_last_name}"
        else:
            user = document.user
            user_name = f"{user.manager_first_name} {user.manager_last_name}"
            user_email = user.manager_email
            user_id = user.manager_id
            manager_email = None
            manager_name = None
        
        # Check which documents were uploaded
        documents_uploaded = []
        if document.experience_letter:
            documents_uploaded.append('Experience Letter')
        if document.offer_letter:
            documents_uploaded.append('Offer Letter')
        if document.education_certificate:
            documents_uploaded.append('Education Certificate')
        if document.skill_certificate:
            documents_uploaded.append('Skill Certificate')
        
        subject = f"Document Submission Confirmation - {user_name}"
        
        context = {
            'user_name': user_name,
            'user_id': user_id,
            'user_type': user_type.title(),
            'documents_uploaded': documents_uploaded,
            'submission_date': timezone.now().strftime('%B %d, %Y at %I:%M %p'),
            'manager_name': manager_name,
            'manager_email': manager_email,
        }
        
        # Notify user
        send_email_notification(
            subject=subject,
            recipient_email=user_email,
            template_name='document_submission',
            context=context,
            recipient_name=user_name
        )
        
        # Notify manager if employee submitted documents
        if user_type == 'employee' and manager_email and manager_name:
            manager_subject = f"Employee Document Submission - {user_name}"
            send_email_notification(
                subject=manager_subject,
                recipient_email=manager_email,
                template_name='document_submission_manager',
                context=context,
                recipient_name=manager_name
            )
        
        return True
    except Exception as e:
        print(f"Error sending document submission notification: {str(e)}")
        return False


def send_leave_manager_approval_notification(leave):
    """
    Called when reporting manager approves an employee's leave.
    1. Notifies employee that manager has approved and leave is forwarded to HR.
    2. Notifies HR/Admin that leave is approved by manager and awaiting final HR action.
    """
    try:
        from employee.models import Employee
        if not hasattr(leave, 'user') or not isinstance(leave.user, Employee):
            return False

        employee = leave.user
        employee_name = f"{employee.employee_first_name} {employee.employee_last_name}"
        employee_email = employee.employee_email

        manager = getattr(leave, 'manager', None) or getattr(employee, 'employee_reports_to', None)
        manager_name = f"{manager.manager_first_name} {manager.manager_last_name}" if manager else "Reporting Manager"

        context = {
            'employee_name': employee_name,
            'employee_id': getattr(employee, 'employee_id', 'N/A'),
            'manager_name': manager_name,
            'leave_type': (leave.leavetype or '').title(),
            'start_date': leave.startdate.strftime('%B %d, %Y') if leave.startdate else 'N/A',
            'end_date': leave.enddate.strftime('%B %d, %Y') if leave.enddate else 'N/A',
            'reason': leave.reason or 'N/A',
            'leave_days': (leave.enddate - leave.startdate).days + 1 if (leave.startdate and leave.enddate) else 0,
            'status': 'Pending HR Approval',
        }

        # 1. Notify employee
        subject_emp = f"Leave Approved by Manager ({manager_name}) - Pending HR Approval"
        send_email_notification(
            subject=subject_emp,
            recipient_email=employee_email,
            template_name='leave_manager_approved_employee',
            context=context,
            recipient_name=employee_name
        )

        # 2. Notify HR admin (CompanyStaff is_admin=True)
        hr_emails = []
        try:
            from administration.models import CompanyStaff
            hr_staffs = CompanyStaff.objects.filter(is_admin=True, is_active=True).exclude(email__isnull=True).exclude(email='')
            for hs in hr_staffs:
                if hs.email and hs.email.strip() not in hr_emails:
                    hr_emails.append(hs.email.strip())
        except Exception:
            pass

        if not hr_emails:
            hr_emails = [COMPANY_EMAIL]

        subject_hr = f"Action Required: Manager {manager_name} Approved Leave for {employee_name} (Pending HR)"
        for hr_email in hr_emails:
            send_email_notification(
                subject=subject_hr,
                recipient_email=hr_email,
                template_name='leave_manager_approved_hr',
                context=context,
                recipient_name="HR Administrator"
            )
        return True
    except Exception as e:
        logger.exception("send_leave_manager_approval_notification failed: %s", str(e))
        return False

