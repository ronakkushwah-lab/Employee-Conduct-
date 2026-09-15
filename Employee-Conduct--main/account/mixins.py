"""
Mixins for role-based access in class-based views.
"""
from django.core.exceptions import PermissionDenied
from django.contrib.auth.mixins import LoginRequiredMixin
from .permissions import get_role, staff_can_view_staff, is_admin, is_manager


class StaffAccessMixin(LoginRequiredMixin):
    """
    Use in DetailView/UpdateView: allow access only if current staff can view the object.
    Object must have .staff (FK to CompanyStaff) or be CompanyStaff.
    Set staff_attr = 'staff' or 'user' etc. if different.
    """
    staff_attr = 'staff'

    def get_current_staff(self):
        staff = getattr(self.request, 'company_staff', None)
        if staff:
            return staff
        company_staff_id = self.kwargs.get('company_staff_id')
        if company_staff_id:
            from account.models import CompanyStaff
            try:
                return CompanyStaff.objects.get(pk=company_staff_id)
            except CompanyStaff.DoesNotExist:
                pass
        return None

    def get_target_staff(self, obj):
        if hasattr(obj, 'role') and hasattr(obj, 'pk'):
            return obj
        return getattr(obj, self.staff_attr, None)

    def dispatch(self, request, *args, **kwargs):
        current = self.get_current_staff()
        if not current:
            raise PermissionDenied
        request.company_staff = current
        obj = self.get_object()
        target = self.get_target_staff(obj)
        if target and not staff_can_view_staff(target, current):
            raise PermissionDenied
        return super().dispatch(request, *args, **kwargs)


class RoleRequiredMixin(LoginRequiredMixin):
    """Allow only given roles. Set required_roles = ['admin'] or ['admin', 'manager']."""
    required_roles = ['admin']

    def get_current_staff(self):
        staff = getattr(self.request, 'company_staff', None)
        if staff:
            return staff
        company_staff_id = self.kwargs.get('company_staff_id')
        if company_staff_id:
            from account.models import CompanyStaff
            try:
                return CompanyStaff.objects.get(pk=company_staff_id)
            except CompanyStaff.DoesNotExist:
                pass
        return None

    def dispatch(self, request, *args, **kwargs):
        current = self.get_current_staff()
        if not current:
            raise PermissionDenied
        request.company_staff = current
        if get_role(current) not in self.required_roles:
            raise PermissionDenied
        return super().dispatch(request, *args, **kwargs)


class AdminRequiredMixin(RoleRequiredMixin):
    required_roles = ['admin']


class ManagerOrAdminMixin(RoleRequiredMixin):
    required_roles = ['admin', 'manager']
