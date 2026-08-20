"""
Context processor to add notification count to all employee templates
"""
from managers.models import EmployeeNotification
from account.models import CompanyStaff
from employee.models import Employee


def employee_notifications(request):
    """
    Add unread notification count to context for employee templates
    """
    context = {
        'unread_notification_count': 0
    }
    
    try:
        # Try to get company_staff_id from session first
        company_staff_id = None
        if hasattr(request, 'session') and 'company_staff_id' in request.session:
            company_staff_id = request.session.get('company_staff_id')
        
        # If not in session, try to get from URL kwargs (for employee views)
        if not company_staff_id and hasattr(request, 'resolver_match') and request.resolver_match:
            kwargs = request.resolver_match.kwargs
            company_staff_id = kwargs.get('company_staff_id')
        
        if company_staff_id:
            try:
                company_staff_id = int(company_staff_id)
                company_staff = CompanyStaff.objects.filter(id=company_staff_id).first()
                if company_staff and company_staff.is_employee:
                    employee = Employee.objects.filter(user=company_staff).first()
                    if employee:
                        unread_count = EmployeeNotification.objects.filter(
                            user=employee, 
                            is_read=False
                        ).count()
                        context['unread_notification_count'] = unread_count
            except (ValueError, TypeError):
                pass
    except Exception:
        # Silently fail if there's any error
        pass
    
    return context
