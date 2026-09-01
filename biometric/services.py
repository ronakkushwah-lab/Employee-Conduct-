from datetime import datetime, timedelta
from xml.etree import ElementTree

from django.db import transaction
from django.utils import timezone
from django.utils.dateparse import parse_datetime

from employee.models import Attendance, Employee
from managers.models import Manager, ManagerAttendance

from .models import BiometricDevice, BiometricEventLog


def _clean(value):
    if value is None:
        return ''
    return str(value).strip()


def parse_punch_time(value):
    if not value:
        return timezone.now()
    if hasattr(value, 'date') and hasattr(value, 'time'):
        parsed = value
    else:
        val_str = str(value).strip()
        parsed = parse_datetime(val_str)
        if parsed is None:
            formats = [
                '%Y-%m-%d %H:%M:%S',
                '%Y/%m/%d %H:%M:%S',
                '%d-%m-%Y %H:%M:%S',
                '%d/%m/%Y %H:%M:%S',
                '%Y-%m-%d %H:%M',
                '%Y/%m/%d %H:%M',
                '%d-%m-%Y %H:%M',
                '%d/%m/%Y %H:%M',
                '%Y%m%d%H%M%S',
                '%Y-%m-%dT%H:%M:%S',
                '%Y-%m-%dT%H:%M:%S.%f',
            ]
            for fmt in formats:
                try:
                    parsed = datetime.strptime(val_str, fmt)
                    break
                except ValueError:
                    continue
        if parsed is None:
            parsed = timezone.now()
    if timezone.is_naive(parsed):
        parsed = timezone.make_aware(parsed, timezone.get_current_timezone())
    return parsed


def get_payload_list(payload):
    if isinstance(payload, list):
        return payload
    if isinstance(payload, dict):
        for key in ('logs', 'events', 'data', 'punches'):
            if isinstance(payload.get(key), list):
                return payload[key]
        return [payload]
    raise ValueError('Payload must be a JSON object or list.')


def resolve_biometric_identity(biometric_user_id):
    raw_id = _clean(biometric_user_id)
    if not raw_id:
        return None, None

    # 1. Exact match for Employee
    emp = (
        Employee.objects.filter(biometric_id__iexact=raw_id).first()
        or Employee.objects.filter(employee_id__iexact=raw_id).first()
    )
    if emp:
        return emp, None

    # 2. Exact match for Manager
    mgr = (
        Manager.objects.filter(biometric_id__iexact=raw_id).first()
        or Manager.objects.filter(manager_id__iexact=raw_id).first()
    )
    if mgr:
        return None, mgr

    # 3. Cleaned digits and variations (e.g. '1', '001', 'EIC-001', 'EIC-1')
    clean_id = raw_id.upper().replace('EIC-', '').strip()
    digits_only = ''.join(c for c in clean_id if c.isdigit())

    if clean_id:
        emp = (
            Employee.objects.filter(biometric_id__iexact=clean_id).first()
            or Employee.objects.filter(employee_id__iexact=clean_id).first()
            or Employee.objects.filter(biometric_id__iexact=f"EIC-{clean_id}").first()
            or Employee.objects.filter(employee_id__iexact=f"EIC-{clean_id}").first()
        )
        if emp:
            return emp, None

        mgr = (
            Manager.objects.filter(biometric_id__iexact=clean_id).first()
            or Manager.objects.filter(manager_id__iexact=clean_id).first()
            or Manager.objects.filter(biometric_id__iexact=f"EIC-{clean_id}").first()
            or Manager.objects.filter(manager_id__iexact=f"EIC-{clean_id}").first()
        )
        if mgr:
            return None, mgr

    # 4. Numeric value match (e.g. machine sends 1, employee has 001 or vice-versa)
    if digits_only:
        num_val = int(digits_only)
        for e in Employee.objects.all():
            for field_val in (e.biometric_id, e.employee_id):
                if field_val:
                    e_digits = ''.join(c for c in str(field_val) if c.isdigit())
                    if e_digits and int(e_digits) == num_val:
                        return e, None

        for m in Manager.objects.all():
            for field_val in (m.biometric_id, m.manager_id):
                if field_val:
                    m_digits = ''.join(c for c in str(field_val) if c.isdigit())
                    if m_digits and int(m_digits) == num_val:
                        return None, m

    return None, None


def resolve_device(payload):
    identifier = _clean(
        payload.get('device_id')
        or payload.get('device')
        or payload.get('serial_number')
        or payload.get('device_serial_number')
        or payload.get('DeviceSerialNo')
        or payload.get('DeviceUID')
        or payload.get('DeviceUniqueID')
        or payload.get('SerialNumber')
        or payload.get('DeviceID')
    )
    terminal_id = _clean(
        payload.get('terminal_id')
        or payload.get('TerminalID')
        or payload.get('TerminalId')
        or payload.get('MachineID')
        or payload.get('MachineId')
        or payload.get('MachineNumber')
    )
    source_ip = _clean(payload.get('source_ip'))
    if not identifier:
        identifier = terminal_id
    if not identifier:
        return None

    device = (
        BiometricDevice.objects.filter(device_id=identifier).first()
        or BiometricDevice.objects.filter(serial_number=identifier).first()
        or BiometricDevice.objects.filter(terminal_id=identifier).first()
    )
    if not device and terminal_id and source_ip:
        device = BiometricDevice.objects.filter(terminal_id=terminal_id, allowed_source_ip=source_ip).first()
    if device:
        if device.allowed_source_ip and source_ip and device.allowed_source_ip != source_ip:
            raise PermissionError(f'Source IP {source_ip} is not allowed for device {device.device_id}.')
        now = timezone.now()
        device.last_seen_at = now
        device.last_event_type = _clean(payload.get('event_type') or payload.get('Event'))[:50]
        device.save(update_fields=['last_seen_at', 'last_event_type', 'updated'])
    return device


def extract_message_frames(buffer):
    frames = []
    remainder = buffer
    while True:
        start = remainder.find('<Message')
        if start == -1:
            return frames, remainder[-2048:]
        end = remainder.find('</Message>', start)
        if end == -1:
            return frames, remainder[start:]
        end += len('</Message>')
        frames.append(remainder[start:end])
        remainder = remainder[end:]


def parse_tcp_xml_payload(raw_payload):
    root = ElementTree.fromstring(raw_payload.strip())
    payload = {}
    for child in root:
        payload[child.tag] = (child.text or '').strip()
    return payload


def normalize_tcp_xml_payload(payload):
    serial_number = (
        payload.get('DeviceSerialNo')
        or payload.get('DeviceUID')
        or payload.get('DeviceUniqueID')
        or payload.get('SerialNumber')
        or payload.get('DeviceID')
        or ''
    )
    terminal_id = (
        payload.get('TerminalID')
        or payload.get('TerminalId')
        or payload.get('MachineID')
        or payload.get('MachineId')
        or payload.get('MachineNumber')
        or ''
    )
    user_id = (
        payload.get('UserID')
        or payload.get('UserId')
        or payload.get('PIN')
        or payload.get('EnrollNumber')
        or payload.get('EnrollNo')
        or payload.get('rfid_code')
    )
    event_type = payload.get('Event') or payload.get('EventType') or payload.get('event') or 'TcpXmlPush'
    compact_event_type = ''.join(ch for ch in _clean(event_type).lower() if ch.isalnum())
    event_aliases = {
        'timelog': 'TimeLog',
        'attendancelog': 'TimeLog',
        'generallog': 'TimeLog',
        'managementlog': 'ManagementLog',
        'verificationfailure': 'VerificationFailure',
        'verificationsuccess': 'VerificationSuccess',
    }
    event_type = event_aliases.get(compact_event_type, event_type)

    normalized = {
        'DeviceSerialNo': serial_number,
        'TerminalID': terminal_id,
        'Event': event_type,
        'UserID': user_id,
        'VerifMode': payload.get('VerifMode') or payload.get('VerifyMode') or payload.get('VerificationMode') or '',
        'AttendStat': payload.get('AttendStat') or payload.get('AttendanceStatus') or payload.get('IOStatus') or '',
        'TransID': payload.get('TransID') or payload.get('trans_id') or '',
        'device_id': serial_number or terminal_id,
        'serial_number': serial_number,
        'terminal_id': terminal_id,
        'event_type': event_type,
        'user_id': user_id,
        'verify_mode': payload.get('VerifMode') or payload.get('VerifyMode') or payload.get('VerificationMode') or '',
        'attendance_status': payload.get('AttendStat') or payload.get('AttendanceStatus') or payload.get('IOStatus') or '',
        'transaction_id': payload.get('TransID') or payload.get('trans_id') or '',
    }

    if payload.get('punch_time') or payload.get('PunchTime'):
        normalized['punch_time'] = payload.get('punch_time') or payload.get('PunchTime')
        return normalized

    try:
        punch_dt = datetime(
            int(payload.get('Year')),
            int(payload.get('Month')),
            int(payload.get('Day')),
            int(payload.get('Hour') or 0),
            int(payload.get('Minute') or 0),
            int(payload.get('Second') or 0),
        )
        normalized['punch_time'] = punch_dt.strftime('%Y-%m-%d %H:%M:%S')
    except Exception:
        pass
    return normalized


def _apply_employee_punch(employee, punch_time):
    day_start = punch_time.replace(hour=0, minute=0, second=0, microsecond=0)
    day_end = day_start + timedelta(days=1)
    attendance = Attendance.objects.filter(
        employee=employee,
        check_in__gte=day_start,
        check_in__lt=day_end,
    ).order_by('check_in').first()

    if not attendance:
        return Attendance.objects.create(
            employee=employee,
            check_in=punch_time,
            source='biometric',
        ), 'check_in'

    attendance.check_out = punch_time
    attendance.source = 'biometric'
    attendance.save(update_fields=['check_out', 'source', 'updated'])
    return attendance, 'check_out'


def _apply_manager_punch(manager, punch_time):
    day_start = punch_time.replace(hour=0, minute=0, second=0, microsecond=0)
    day_end = day_start + timedelta(days=1)
    attendance = ManagerAttendance.objects.filter(
        manager=manager,
        check_in__gte=day_start,
        check_in__lt=day_end,
    ).order_by('check_in').first()

    if not attendance:
        return ManagerAttendance.objects.create(
            manager=manager,
            check_in=punch_time,
        ), 'check_in'

    attendance.check_out = punch_time
    attendance.save(update_fields=['check_out'])
    return attendance, 'check_out'


@transaction.atomic
def process_biometric_punch(payload, protocol='manual', source_ip=None, device=None):
    if source_ip and 'source_ip' not in payload:
        payload = {**payload, 'source_ip': source_ip}
    user_id = _clean(
        payload.get('user_id')
        or payload.get('UserID')
        or payload.get('UserId')
        or payload.get('biometric_id')
        or payload.get('biometric_user_id')
        or payload.get('rfid_code')
        or payload.get('EnrollNumber')
        or payload.get('EnrollNo')
        or payload.get('PIN')
    )
    if not user_id:
        return {
            'status': BiometricEventLog.STATUS_INVALID,
            'message': 'Missing user_id/rfid_code.',
        }

    try:
        punch_time = parse_punch_time(
            payload.get('timestamp')
            or payload.get('punch_time')
            or payload.get('PunchTime')
            or payload.get('time')
        )
    except ValueError as exc:
        return {
            'status': BiometricEventLog.STATUS_INVALID,
            'biometric_user_id': user_id,
            'message': str(exc),
        }

    try:
        device = device or resolve_device(payload)
    except PermissionError as exc:
        BiometricEventLog.objects.create(
            protocol=protocol,
            source_ip=source_ip,
            biometric_user_id=user_id or 'unknown',
            punch_time=punch_time,
            status=BiometricEventLog.STATUS_INVALID,
            message=str(exc)[:255],
            raw_payload=payload,
        )
        return {
            'status': BiometricEventLog.STATUS_INVALID,
            'biometric_user_id': user_id,
            'message': str(exc),
        }
    employee, manager = resolve_biometric_identity(user_id)
    company = None
    if employee and employee.user:
        company = employee.user.company
    elif manager and manager.user:
        company = manager.user.company
    elif device and device.company:
        company = device.company
    else:
        from administration.models import Company
        company = Company.objects.first()

    event = BiometricEventLog.objects.create(
        device=device,
        company=company,
        employee=employee,
        manager=manager,
        protocol=protocol,
        source_ip=source_ip,
        event_type=_clean(payload.get('event_type') or payload.get('Event') or payload.get('EventType')),
        transaction_id=_clean(payload.get('transaction_id') or payload.get('TransID') or payload.get('trans_id')),
        biometric_user_id=user_id,
        verify_mode=_clean(payload.get('verify_mode') or payload.get('VerifMode') or payload.get('VerifyMode')),
        punch_time=punch_time,
        raw_payload=payload,
    )

    if not employee and not manager:
        event.status = BiometricEventLog.STATUS_UNMATCHED
        event.message = 'No employee or manager matched this biometric user ID.'
        event.save(update_fields=['status', 'message'])
        return {
            'status': event.status,
            'biometric_user_id': user_id,
            'message': event.message,
        }

    # Process punches within reasonable date window (up to 30 days old)
    max_past_days = timezone.now().date() - timedelta(days=30)
    if punch_time.date() < max_past_days:
        event.status = BiometricEventLog.STATUS_IGNORED
        event.message = f'Historical punch from {punch_time.date()} ignored (older than 30 days).'
        event.save(update_fields=['status', 'message'])
        return {
            'status': event.status,
            'biometric_user_id': user_id,
            'message': event.message,
        }

    if employee:
        attendance, action = _apply_employee_punch(employee, punch_time)
        event.attendance = attendance
    else:
        attendance, action = _apply_manager_punch(manager, punch_time)
        event.manager_attendance = attendance

    event.status = BiometricEventLog.STATUS_APPLIED
    event.message = f'Biometric {action} recorded.'
    event.save(update_fields=['attendance', 'manager_attendance', 'status', 'message'])

    if device:
        device.last_punch_at = punch_time
        device.save(update_fields=['last_punch_at', 'updated'])

    return {
        'status': event.status,
        'biometric_user_id': user_id,
        'action': action,
        'employee_id': employee.id if employee else None,
        'manager_id': manager.id if manager else None,
        'attendance_id': attendance.id,
        'punch_time': punch_time.isoformat(),
        'message': event.message,
    }
