import ipaddress

from django import forms

from .models import BiometricDevice


class BiometricDeviceForm(forms.ModelForm):
    ip_address = forms.CharField(required=False)
    allowed_source_ip = forms.CharField(required=False)

    class Meta:
        model = BiometricDevice
        fields = [
            'name',
            'site_label',
            'device_type',
            'integration_mode',
            'device_id',
            'serial_number',
            'terminal_id',
            'location',
            'ip_address',
            'allowed_source_ip',
            'port',
            'device_password',
            'machine_number',
            'bridge_server_url',
            'is_active',
            'notes',
        ]
        widgets = {
            'notes': forms.Textarea(attrs={'rows': 3}),
            'integration_mode': forms.Select(attrs={'class': 'form-select'}),
            'device_type': forms.Select(attrs={'class': 'form-select'}),
            'is_active': forms.CheckboxInput(attrs={'class': 'form-check-input'}),
        }

    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        for name, field in self.fields.items():
            if name in {'is_active'}:
                continue
            existing = field.widget.attrs.get('class', '')
            field.widget.attrs['class'] = f'{existing} form-control'.strip()

    def _normalize_ip(self, value):
        value = (value or '').strip()
        if not value:
            return None
        if value.count('.') == 3:
            parts = value.split('.')
            if all(part.isdigit() for part in parts):
                value = '.'.join(str(int(part)) for part in parts)
        try:
            return str(ipaddress.ip_address(value))
        except ValueError:
            raise forms.ValidationError('Enter a valid IP address.')

    def clean_ip_address(self):
        return self._normalize_ip(self.cleaned_data.get('ip_address'))

    def clean_allowed_source_ip(self):
        return self._normalize_ip(self.cleaned_data.get('allowed_source_ip'))
