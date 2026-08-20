from django import template
from django.core import serializers
from django.utils.safestring import mark_safe
from account.models import CompanyStaff
from employee.models import Employee

register=template.Library()

@register.filter('queryset_to_json')
def queryset_to_json(qs):
    json_data = serializers.serialize("json", qs)
    return mark_safe(json_data)

@register.simple_tag
def get_employee(company_staff_id):
    """Get employee object from company_staff_id"""
    try:
        company_staff = CompanyStaff.objects.get(id=company_staff_id)
        employee = Employee.objects.filter(user=company_staff).first()
        return employee
    except:
        return None
