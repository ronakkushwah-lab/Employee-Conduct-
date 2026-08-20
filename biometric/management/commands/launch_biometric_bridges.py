import os
import subprocess
import sys

from django.conf import settings
from django.core.management.base import BaseCommand, CommandError

from biometric.models import BiometricDevice


class Command(BaseCommand):
    help = 'Launch local polling bridge processes for active bridge_pull biometric devices.'

    def add_arguments(self, parser):
        parser.add_argument('--device-id', help='Launch one registered device_id only.')
        parser.add_argument('--visible', action='store_true', help='Open bridge windows visibly on Windows.')

    def handle(self, *args, **options):
        bridge_path = os.path.join(settings.BASE_DIR, 'biometric_bridge.py')
        if not os.path.exists(bridge_path):
            raise CommandError(f'Bridge script not found: {bridge_path}')

        devices = BiometricDevice.objects.filter(is_active=True, integration_mode='bridge_pull')
        if options.get('device_id'):
            devices = devices.filter(device_id=options['device_id'])

        devices = list(devices.order_by('name', 'id'))
        if not devices:
            self.stdout.write(self.style.WARNING('No active bridge_pull biometric devices found.'))
            return

        server_url = f"{getattr(settings, 'PUBLIC_API_BASE_URL', 'http://127.0.0.1:8000')}/api/attendance/biometric-punch/"
        for device in devices:
            if not device.ip_address:
                self.stdout.write(self.style.WARNING(f'Skipped {device.device_id}: missing ip_address.'))
                continue

            env = os.environ.copy()
            env.update({
                'BIOMETRIC_DEVICE_IP': str(device.ip_address),
                'BIOMETRIC_DEVICE_PORT': str(device.port),
                'BIOMETRIC_DEVICE_ID': device.device_id,
                'BIOMETRIC_SERVER_URL': device.get_effective_server_url(server_url),
                'BIOMETRIC_DEVICE_SECRET_KEY': device.secret_key,
            })

            creationflags = 0
            if os.name == 'nt':
                creationflags = subprocess.CREATE_NEW_CONSOLE if options['visible'] else subprocess.CREATE_NO_WINDOW

            subprocess.Popen(
                [sys.executable, bridge_path],
                cwd=settings.BASE_DIR,
                env=env,
                creationflags=creationflags,
            )
            self.stdout.write(f'Launched bridge for {device.name} ({device.device_id}) at {device.ip_address}:{device.port}')

        self.stdout.write(self.style.SUCCESS('Bridge launch complete.'))
