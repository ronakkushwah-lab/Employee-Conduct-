from django.shortcuts import redirect
from .models import CompanyStaff

def custom_login_required(
    function=None, login_url=None):
    """
    Decorator to extend login required to also check if a notebook auth is
    desired first (but you could customize this to be another check!)
    """

    print('custom login called')
    def wrap(request, *args, **kwargs):
        # First try to get company_staff_id from URL kwargs, then from session
        company_staff_id = kwargs.get('company_staff_id') or request.session.get('company_staff_id')
        
        if company_staff_id:
            try:
                company_staff = CompanyStaff.objects.get(pk=company_staff_id)
                if company_staff.is_authenticated:
                    print('Company Staff successfully authenticated....')
                    # Update session with the company_staff_id from URL if it's different
                    if kwargs.get('company_staff_id') and request.session.get('company_staff_id') != kwargs.get('company_staff_id'):
                        request.session['company_staff_id'] = kwargs.get('company_staff_id')
                    return function(request, *args, **kwargs)
                else:
                    return redirect('/')
            except CompanyStaff.DoesNotExist:
                # CompanyStaff doesn't exist, clear session and redirect
                request.session.flush()
                return redirect('/')
        else:
            return redirect('/')
    return wrap
