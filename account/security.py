"""
Security utilities for protecting URL parameters and preventing IDOR vulnerabilities.
This module provides decorators and utilities to ensure users can only access their own data.
"""
from functools import wraps
from django.shortcuts import redirect
from django.contrib import messages
from django.http import HttpResponseForbidden
from .models import CompanyStaff


def get_current_user(request):
    """
    Safely get the current logged-in user from session.
    Returns CompanyStaff instance or None if not authenticated.
    """
    company_staff_id = request.session.get('company_staff_id')
    if not company_staff_id:
        return None
    
    try:
        company_staff = CompanyStaff.objects.get(id=company_staff_id, is_active=True)
        if not company_staff.is_authenticated:
            return None
        return company_staff
    except CompanyStaff.DoesNotExist:
        return None


def verify_user_access(request, company_staff_id, company_id=None):
    """
    Verify that the logged-in user matches the company_staff_id in the URL.
    Also optionally verify company_id matches.
    
    Returns:
        tuple: (is_authorized: bool, company_staff: CompanyStaff or None)
    """
    current_user = get_current_user(request)
    
    if not current_user:
        return False, None
    
    # Verify the company_staff_id matches the logged-in user
    if current_user.id != company_staff_id:
        return False, None
    
    # Optionally verify company_id matches
    if company_id is not None:
        if current_user.company_id != company_id:
            return False, None
    
    return True, current_user


def require_authentication(view_func):
    """
    Decorator to ensure user is authenticated before accessing a view.
    """
    @wraps(view_func)
    def wrapper(request, *args, **kwargs):
        current_user = get_current_user(request)
        if not current_user:
            messages.error(request, 'Please login to access this page.')
            return redirect('/')
        return view_func(request, *args, **kwargs)
    return wrapper


def verify_url_parameters(view_func):
    """
    Decorator to verify that URL parameters (company_id, company_staff_id) 
    match the logged-in user. Prevents IDOR (Insecure Direct Object Reference) attacks.
    
    Usage:
        @verify_url_parameters
        def my_view(request, company_id, company_staff_id):
            # company_staff_id and company_id are guaranteed to match logged-in user
            ...
    """
    @wraps(view_func)
    def wrapper(request, *args, **kwargs):
        # Get parameters from kwargs (URL parameters)
        company_staff_id = kwargs.get('company_staff_id')
        company_id = kwargs.get('company_id')
        
        if company_staff_id is None:
            # If no company_staff_id in URL, try to get from session
            current_user = get_current_user(request)
            if not current_user:
                messages.error(request, 'Please login to access this page.')
                return redirect('/')
            # Redirect to URL with proper parameters
            if company_id:
                return redirect(f'{request.path.replace(str(company_id), str(current_user.company_id))}/{current_user.id}')
            return redirect('/')
        
        # Verify access
        is_authorized, company_staff = verify_user_access(request, company_staff_id, company_id)
        
        if not is_authorized:
            messages.error(request, 'You do not have permission to access this resource.')
            # Redirect to user's own dashboard
            current_user = get_current_user(request)
            if current_user:
                if current_user.is_employee:
                    return redirect('employee_dashboard', 
                                  company_id=current_user.company_id, 
                                  company_staff_id=current_user.id)
                elif current_user.is_manager:
                    return redirect('dashboard',
                                  company_id=current_user.company_id,
                                  company_staff_id=current_user.id)
                elif current_user.is_company_admin:
                    return redirect('index',
                                  company_id=current_user.company_id,
                                  company_staff_id=current_user.id)
            return redirect('/')
        
        # Add verified user to request for convenience
        request.verified_user = company_staff
        
        return view_func(request, *args, **kwargs)
    return wrapper


def require_role(*allowed_roles):
    """
    Decorator to require specific user roles.
    
    Usage:
        @require_role('employee')
        def employee_view(request, ...):
            ...
        
        @require_role('manager', 'company_admin')
        def admin_view(request, ...):
            ...
    """
    def decorator(view_func):
        @wraps(view_func)
        def wrapper(request, *args, **kwargs):
            current_user = get_current_user(request)
            if not current_user:
                messages.error(request, 'Please login to access this page.')
                return redirect('accounts:login')
            
            # Check if user has one of the allowed roles
            has_role = False
            if 'employee' in allowed_roles and current_user.is_employee:
                has_role = True
            if 'manager' in allowed_roles and current_user.is_manager:
                has_role = True
            if 'company_admin' in allowed_roles and current_user.is_company_admin:
                has_role = True
            
            if not has_role:
                messages.error(request, 'You do not have permission to access this page.')
                # Redirect based on user's actual role
                if current_user.is_employee:
                    return redirect('employee_dashboard',
                                  company_id=current_user.company_id,
                                  company_staff_id=current_user.id)
                elif current_user.is_manager:
                    return redirect('dashboard',
                                  company_id=current_user.company_id,
                                  company_staff_id=current_user.id)
                elif current_user.is_company_admin:
                    return redirect('index',
                                  company_id=current_user.company_id,
                                  company_staff_id=current_user.id)
                return redirect('/')
            
            return view_func(request, *args, **kwargs)
        return wrapper
    return decorator

