from django import template
from django.core import serializers
from django.utils.safestring import mark_safe

from account.models import CompanyStaff
from managers.models import Manager

register = template.Library()


@register.filter('string')
def string(value):
    return "+value+"


@register.filter('queryset_to_json')
def queryset_to_json(qs):
    json_data = serializers.serialize("json", qs)
    return mark_safe(json_data)


@register.simple_tag
def get_manager_display_name(company_staff_id):
    """Return the logged-in manager's full name for topbar/sidebar (e.g. Shalini Rajput)."""
    if not company_staff_id:
        return "User"
    try:
        staff = CompanyStaff.objects.get(id=company_staff_id)
        manager = staff.manager
        return f"{manager.manager_first_name} {manager.manager_last_name}".strip() or "User"
    except (CompanyStaff.DoesNotExist, Manager.DoesNotExist, AttributeError):
        return "User"


@register.simple_tag
def get_manager_profile_image_url(company_staff_id):
    """Return the manager's profile image URL for topbar; empty string if no image."""
    if not company_staff_id:
        return ""
    try:
        staff = CompanyStaff.objects.get(id=company_staff_id)
        manager = staff.manager
        if manager.manager_image:
            return manager.manager_image.url
    except (CompanyStaff.DoesNotExist, Manager.DoesNotExist, AttributeError):
        pass
    return ""
