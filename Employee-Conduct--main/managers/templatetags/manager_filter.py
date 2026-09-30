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


_manager_cache = {}


@register.simple_tag
def get_manager_display_name(company_staff_id):
    """Return the logged-in manager's full name for topbar/sidebar (e.g. Shalini Rajput)."""
    if not company_staff_id:
        return "User"
    if company_staff_id in _manager_cache:
        return _manager_cache[company_staff_id].get('name', 'User')
    try:
        manager = Manager.objects.filter(user_id=company_staff_id).first()
        if manager:
            name = f"{manager.manager_first_name or ''} {manager.manager_last_name or ''}".strip() or "User"
            avatar = manager.avatar_url if hasattr(manager, 'avatar_url') else "/static/asets/images/dummy-man.png"
            _manager_cache[company_staff_id] = {'name': name, 'avatar': avatar}
            return name
        return "User"
    except Exception:
        return "User"


@register.simple_tag
def get_manager_profile_image_url(company_staff_id):
    """Return the manager's profile image URL or gender-based avatar for topbar."""
    if not company_staff_id:
        return "/static/asets/images/dummy-man.png"
    if company_staff_id in _manager_cache and 'avatar' in _manager_cache[company_staff_id]:
        return _manager_cache[company_staff_id]['avatar']
    try:
        manager = Manager.objects.filter(user_id=company_staff_id).first()
        if manager:
            avatar = manager.avatar_url if hasattr(manager, 'avatar_url') else "/static/asets/images/dummy-man.png"
            name = f"{manager.manager_first_name or ''} {manager.manager_last_name or ''}".strip() or "User"
            _manager_cache[company_staff_id] = {'name': name, 'avatar': avatar}
            return avatar
    except Exception:
        pass
    return "/static/asets/images/dummy-man.png"
