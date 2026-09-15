from django.contrib import admin

from .models import BiometricDevice, BiometricEventLog


@admin.register(BiometricDevice)
class BiometricDeviceAdmin(admin.ModelAdmin):
    list_display = ('name', 'device_id', 'integration_mode', 'company', 'ip_address', 'port', 'is_active', 'last_seen_at', 'last_punch_at')
    list_filter = ('integration_mode', 'device_type', 'is_active', 'company')
    search_fields = ('name', 'device_id', 'serial_number', 'terminal_id', 'location')
    readonly_fields = ('secret_key', 'created', 'updated', 'last_seen_at', 'last_punch_at', 'last_tested_at', 'last_test_status', 'last_test_message')


@admin.register(BiometricEventLog)
class BiometricEventLogAdmin(admin.ModelAdmin):
    list_display = ('biometric_user_id', 'punch_time', 'protocol', 'status', 'device', 'employee', 'manager', 'received_at')
    list_filter = ('protocol', 'status', 'device', 'company')
    search_fields = ('biometric_user_id', 'verify_mode', 'message', 'device__device_id', 'device__serial_number', 'employee__employee_id', 'manager__manager_id')
    readonly_fields = ('raw_payload', 'received_at')
