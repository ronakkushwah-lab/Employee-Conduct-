import json
from datetime import datetime as datetime_type

from django.utils import timezone


HTTP_METHOD_PREFIXES = (b'GET ', b'POST ', b'PUT ', b'DELETE ', b'HEAD ', b'OPTIONS ', b'PATCH ')


def looks_like_http_request(data):
    if not data:
        return False
    return any(data.startswith(prefix) for prefix in HTTP_METHOD_PREFIXES)


def parse_http_request(data):
    header_end = data.find(b'\r\n\r\n')
    if header_end == -1:
        raise ValueError('Incomplete HTTP request headers.')

    head = data[:header_end].decode('iso-8859-1', errors='ignore')
    body = data[header_end + 4:]
    lines = [line for line in head.split('\r\n') if line]
    if not lines:
        raise ValueError('HTTP request line missing.')

    request_line = lines[0].split()
    if len(request_line) != 3:
        raise ValueError('Malformed HTTP request line.')

    method, path, version = request_line
    headers = {}
    for line in lines[1:]:
        if ':' not in line:
            continue
        key, value = line.split(':', 1)
        headers[key.strip().lower()] = value.strip()

    return {
        'method': method.upper(),
        'path': path,
        'version': version,
        'headers': headers,
        'body': body,
    }


def decode_secureye_json_body(body):
    if not body:
        return {}

    raw_payload = body.rstrip(b'\x00').strip()
    if not raw_payload:
        return {}

    try:
        return json.loads(raw_payload.decode('utf-8', errors='ignore'))
    except json.JSONDecodeError:
        pass

    if len(body) >= 4:
        declared_length = int.from_bytes(body[:4], byteorder='little', signed=False)
        payload = body[4:4 + declared_length].rstrip(b'\x00').strip()
        if payload:
            return json.loads(payload.decode('utf-8', errors='ignore'))

    return json.loads(raw_payload.decode('utf-8', errors='ignore'))


def normalize_secureye_http_event(http_request, source_ip=None):
    headers = http_request['headers']
    body_data = decode_secureye_json_body(http_request['body'])
    request_code = (headers.get('request_code') or '').strip()
    event_type = 'TimeLog' if request_code == 'realtime_glog' else request_code or 'HttpPush'
    serial_number = (headers.get('dev_id') or body_data.get('dev_id') or '').strip()
    user_id = str(body_data.get('user_id') or '').strip()
    terminal_id = str(body_data.get('machine_id') or body_data.get('terminal_id') or '').strip()
    transaction_id = (headers.get('trans_id') or '').strip()

    payload = {
        'DeviceSerialNo': serial_number,
        'Event': event_type,
        'UserID': user_id,
        'VerifMode': str(body_data.get('verify_mode') or '').strip(),
        'AttendStat': str(body_data.get('io_mode') or '').strip(),
        'TransID': transaction_id,
        'TerminalID': terminal_id,
        'device_id': serial_number,
        'serial_number': serial_number,
        'event_type': event_type,
        'user_id': user_id,
        'verify_mode': str(body_data.get('verify_mode') or '').strip(),
        'attendance_status': str(body_data.get('io_mode') or '').strip(),
        'transaction_id': transaction_id,
        'terminal_id': terminal_id,
        'request_code': request_code,
        'push_vendor': 'secureye_http',
        'push_model': body_data.get('fk_name') or '',
        'body_json': body_data,
        'source_ip': source_ip or '',
    }

    io_time = str(body_data.get('io_time') or '').strip()
    if io_time:
        try:
            punch_dt = datetime_type.strptime(io_time, '%Y%m%d%H%M%S')
            if timezone.is_naive(punch_dt):
                punch_dt = timezone.make_aware(punch_dt, timezone.get_current_timezone())
            payload['punch_time'] = punch_dt.strftime('%Y-%m-%d %H:%M:%S')
        except Exception:
            payload['punch_time_raw'] = io_time

    return payload, body_data


def build_secureye_http_ack():
    # Secureye/FKDATAHS101-style terminals expect this vendor-specific header.
    # Returning a body like "OK" can leave logs queued on the device.
    response_body = b''
    headers = [
        b'HTTP/1.1 200 OK',
        b'Content-Length: 0',
        b'Connection: close',
        b'Cache-Control: no-store',
        b'response_code: OK',
        b'',
        b'',
    ]
    return b'\r\n'.join(headers) + response_body
