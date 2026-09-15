from django import template
from django.core import serializers
from django.utils.safestring import mark_safe

register=template.Library()

@register.filter('queryset_to_json')
def queryset_to_json(qs):
    json_data = serializers.serialize("json", qs)
    return mark_safe(json_data)
