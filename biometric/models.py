from django.db import models
from django.utils.crypto import get_random_string
from django.utils import timezone

from account.models import Company
from employee.models import Attendance, Employee
from managers.models import Manager, ManagerAttendance


def generate_device_secret_key():
    return get_random_string(48)


class BiometricDevice(models.Model):
    ONLINE_WINDOW_SECONDS = 300
    DIRECT_PUSH_INTEGRATION_MODES = ('tcp_xml_push', 'http_push')
    DEVICE_TYPE_CHOICES = (
        ('fingerprint', 'Fingerprint'),
        ('rfid', 'RFID'),
        ('hybrid', 'Hybrid'),
    )
    INTEGRATION_MODE_CHOICES = (
        ('bridge_pull', 'Bridge Pull'),
        ('tcp_xml_push', 'TCP XML Push'),
        ('http_push', 'HTTP/HTTPS Push'),
        ('manual', 'Manual/API Import'),
    )

    name = models.CharField(max_length=120)
    site_label = models.CharField(max_length=120, blank=True, default='')
    device_type = models.CharField(max_length=20, choices=DEVICE_TYPE_CHOICES, default='hybrid')
    integration_mode = models.CharField(max_length=20, choices=INTEGRATION_MODE_CHOICES, default='bridge_pull')
    device_id = models.CharField(max_length=100, unique=True)
    serial_number = models.CharField(max_length=120, blank=True, default='')
    terminal_id = models.CharField(max_length=100, blank=True, default='')
    company = models.ForeignKey(Company, null=True, blank=True, on_delete=models.CASCADE)
    location = models.CharField(max_length=150, blank=True, default='')
    ip_address = models.GenericIPAddressField(null=True, blank=True)
    allowed_source_ip = models.GenericIPAddressField(null=True, blank=True)
    port = models.PositiveIntegerField(default=5005)
    device_password = models.PositiveIntegerField(default=0)
    machine_number = models.PositiveIntegerField(default=1)
    bridge_server_url = models.URLField(blank=True, default='')
    secret_key = models.CharField(max_length=128, default=generate_device_secret_key)
    notes = models.TextField(blank=True, default='')
    is_active = models.BooleanField(default=True)
    last_seen_at = models.DateTimeField(null=True, blank=True)
    last_punch_at = models.DateTimeField(null=True, blank=True)
    last_tested_at = models.DateTimeField(null=True, blank=True)
    last_test_status = models.CharField(max_length=20, blank=True, default='')
    last_test_message = models.CharField(max_length=255, blank=True, default='')
    last_event_type = models.CharField(max_length=50, blank=True, default='')
    created = models.DateTimeField(auto_now_add=True)
    updated = models.DateTimeField(auto_now=True)

    class Meta:
        ordering = ['name']

    def __str__(self):
        return f'{self.name} ({self.device_id})'

    def get_effective_server_url(self, default_url):
        return (self.bridge_server_url or '').strip() or default_url

    def build_bridge_config(self, default_server_url):
        return {
            'device_name': self.name,
            'integration_mode': self.integration_mode,
            'device_ip': self.ip_address,
            'device_port': self.port,
            'device_password': self.device_password,
            'machine_number': self.machine_number,
            'device_id': self.device_id,
            'device_serial_number': self.serial_number,
            'terminal_id': self.terminal_id,
            'allowed_source_ip': self.allowed_source_ip,
            'server_url': self.get_effective_server_url(default_server_url),
            'device_secret_key': self.secret_key,
        }

    def mark_test_result(self, ok, message):
        self.last_tested_at = timezone.now()
        self.last_test_status = 'online' if ok else 'offline'
        self.last_test_message = str(message)[:255]
        self.save(update_fields=['last_tested_at', 'last_test_status', 'last_test_message', 'updated'])

    def latest_activity_at(self):
        values = [value for value in (self.last_seen_at, self.last_punch_at) if value]
        return max(values) if values else None

    @property
    def is_online(self):
        if not self.is_active:
            return False
        latest = self.latest_activity_at()
        if not latest:
            return False
        return latest >= timezone.now() - timezone.timedelta(seconds=self.ONLINE_WINDOW_SECONDS)

    @property
    def runtime_status_label(self):
        if not self.is_active:
            return 'Disabled'
        return 'Active' if self.is_online else 'Offline'

    @property
    def runtime_status_class(self):
        if not self.is_active:
            return 'bg-secondary'
        return 'bg-success' if self.is_online else 'bg-danger'


class BiometricEventLog(models.Model):
    PROTOCOL_CHOICES = (
        ('manual', 'Manual/API'),
        ('bridge', 'Bridge Pull'),
        ('http', 'HTTP/HTTPS Push'),
        ('tcp_xml', 'TCP XML Push'),
    )
    STATUS_RECEIVED = 'received'
    STATUS_APPLIED = 'applied'
    STATUS_DUPLICATE = 'duplicate'
    STATUS_UNMATCHED = 'unmatched'
    STATUS_INVALID = 'invalid'
    STATUS_ERROR = 'error'

    STATUS_CHOICES = [
        (STATUS_RECEIVED, 'Received'),
        (STATUS_APPLIED, 'Applied'),
        (STATUS_DUPLICATE, 'Duplicate'),
        (STATUS_UNMATCHED, 'Unmatched'),
        (STATUS_INVALID, 'Invalid'),
        (STATUS_ERROR, 'Error'),
    ]

    device = models.ForeignKey(BiometricDevice, null=True, blank=True, on_delete=models.SET_NULL, related_name='event_logs')
    company = models.ForeignKey(Company, null=True, blank=True, on_delete=models.SET_NULL)
    employee = models.ForeignKey(Employee, null=True, blank=True, on_delete=models.SET_NULL)
    manager = models.ForeignKey(Manager, null=True, blank=True, on_delete=models.SET_NULL)
    attendance = models.ForeignKey(Attendance, null=True, blank=True, on_delete=models.SET_NULL)
    manager_attendance = models.ForeignKey(ManagerAttendance, null=True, blank=True, on_delete=models.SET_NULL)
    protocol = models.CharField(max_length=20, choices=PROTOCOL_CHOICES, default='manual')
    source_ip = models.GenericIPAddressField(null=True, blank=True)
    event_type = models.CharField(max_length=80, blank=True, default='')
    transaction_id = models.CharField(max_length=120, blank=True, default='')
    biometric_user_id = models.CharField(max_length=100)
    verify_mode = models.CharField(max_length=100, blank=True, default='')
    punch_time = models.DateTimeField()
    status = models.CharField(max_length=20, choices=STATUS_CHOICES, default=STATUS_RECEIVED)
    message = models.CharField(max_length=255, blank=True, default='')
    raw_payload = models.JSONField(default=dict, blank=True)
    received_at = models.DateTimeField(auto_now_add=True)

    class Meta:
        ordering = ['-received_at']
        indexes = [
            models.Index(fields=['biometric_user_id', 'punch_time']),
            models.Index(fields=['status', 'received_at']),
            models.Index(fields=['device', 'received_at']),
        ]

    def __str__(self):
        return f'{self.biometric_user_id} @ {self.punch_time} ({self.status})'
