import json

from django.http import JsonResponse
from django.utils import timezone
from django.views.decorators.csrf import csrf_exempt
from django.views.decorators.http import require_http_methods

from .models import BiometricDevice, BiometricEventLog
from .services import get_payload_list, process_biometric_punch


def _json_body(request):
    if not request.body:
        return {}
    return json.loads(request.body.decode('utf-8'))


@csrf_exempt
@require_http_methods(['POST'])
def biometric_punch(request):
    default_protocol = getattr(request, 'biometric_protocol', None) or request.GET.get('protocol') or 'manual'
    try:
        payload = _json_body(request)
        items = get_payload_list(payload)
    except (UnicodeDecodeError, json.JSONDecodeError, ValueError) as exc:
        return JsonResponse({'status': 'FAILED', 'error': str(exc)}, status=400)

    source_ip = request.META.get('REMOTE_ADDR')
    results = [
        process_biometric_punch(
            item,
            protocol=item.get('protocol') or default_protocol,
            source_ip=source_ip,
        )
        for item in items
    ]
    inserted = sum(1 for item in results if item.get('status') == 'applied')
    duplicates = sum(1 for item in results if item.get('status') == 'duplicate')
    unmatched = sum(1 for item in results if item.get('status') == 'unmatched')
    invalid = sum(1 for item in results if item.get('status') == 'invalid')

    status_code = 207 if unmatched or invalid else 200
    return JsonResponse({
        'status': 'SUCCESS' if not invalid else 'PARTIAL',
        'received': len(results),
        'inserted': inserted,
        'duplicates': duplicates,
        'unmatched': unmatched,
        'invalid': invalid,
        'results': results,
    }, status=status_code)


@csrf_exempt
@require_http_methods(['POST'])
def bridge_punch(request):
    request.biometric_protocol = 'bridge'
    return biometric_punch(request)


@csrf_exempt
@require_http_methods(['POST'])
def manual_punch(request):
    request.biometric_protocol = 'manual'
    return biometric_punch(request)


@csrf_exempt
@require_http_methods(['POST'])
def http_push(request):
    request.biometric_protocol = 'http'
    return biometric_punch(request)


@csrf_exempt
@require_http_methods(['POST'])
def biometric_heartbeat(request):
    try:
        payload = _json_body(request)
    except (UnicodeDecodeError, json.JSONDecodeError) as exc:
        return JsonResponse({'status': 'FAILED', 'error': str(exc)}, status=400)

    device_id = str(payload.get('device_id') or payload.get('serial_number') or '').strip()
    if not device_id:
        return JsonResponse({'status': 'FAILED', 'error': 'Missing device_id.'}, status=400)

    device = (
        BiometricDevice.objects.filter(device_id=device_id).first()
        or BiometricDevice.objects.filter(serial_number=device_id).first()
    )
    if not device:
        return JsonResponse({'status': 'FAILED', 'error': 'Unknown device.'}, status=404)

    device.last_seen_at = timezone.now()
    device.save(update_fields=['last_seen_at', 'updated'])
    return JsonResponse({'status': 'SUCCESS', 'device_id': device.device_id})


from django.http import HttpResponse


def _extract_json_from_body(body_text):
    start = body_text.find('{')
    end = body_text.rfind('}')
    if start != -1 and end != -1 and end > start:
        try:
            return json.loads(body_text[start:end+1])
        except json.JSONDecodeError:
            return None
    return None


@csrf_exempt
def iclock_cdata(request):
    """
    Standard ZKTeco / eSSL ADMS protocol endpoint for /iclock/cdata.
    Handles:
    - GET: Device handshake, heartbeat & config option sync
    - POST: Punch log uploads (ATTLOG / table data) or JSON Push logs
    """
    sn = (request.GET.get('SN') or request.GET.get('sn') or '').strip().replace('\x00', '')
    table = (request.GET.get('table') or '').upper().replace('\x00', '')
    source_ip = request.META.get('HTTP_X_FORWARDED_FOR', '').split(',')[0].strip() or request.META.get('REMOTE_ADDR')

    device = None
    if sn:
        device = (
            BiometricDevice.objects.filter(device_id=sn).first()
            or BiometricDevice.objects.filter(serial_number=sn).first()
            or BiometricDevice.objects.filter(terminal_id=sn).first()
        )
    if not device:
        device = BiometricDevice.objects.filter(is_active=True).first()

    if device:
        device.last_seen_at = timezone.now()
        device.save(update_fields=['last_seen_at', 'updated'])

    if request.method == 'GET':
        response_text = (
            f"GET OPTION FROM: {sn}\n"
            f"Stamp=9999\n"
            f"OpStamp=9999\n"
            f"PhotoStamp=0\n"
            f"ErrorDelay=30\n"
            f"Delay=5\n"
            f"TransTimes=00:00;14:05\n"
            f"TransInterval=1\n"
            f"TransFlag=1111000000\n"
            f"TimeZone=5.5\n"
            f"Realtime=1\n"
            f"Encrypt=0\n"
            f"PushProtVer=2.4.1\n"
            f"ServerVersion=3.1.1\n"
        )
        return HttpResponse(response_text, content_type='text/plain')

    if request.method == 'POST':
        body_text = request.body.decode('utf-8', errors='ignore')
        inserted_count = 0

        # Try to parse as JSON Push Protocol (Secureye / ZK Cloud Push)
        json_data = _extract_json_from_body(body_text)
        if json_data:
            user_id = str(json_data.get('user_id') or json_data.get('UserID') or '').strip()
            # If user_id is present and not empty, it is an Attendance Punch!
            if user_id:
                io_time = str(json_data.get('io_time') or json_data.get('time') or '').strip()
                payload = {
                    'user_id': user_id,
                    'punch_time': io_time or str(timezone.now()),
                    'verify_mode': str(json_data.get('verify_mode') or ''),
                    'device_id': sn or (device.device_id if device else '1'),
                    'source_ip': source_ip,
                }
                process_biometric_punch(payload, protocol='adms_push', source_ip=source_ip, device=device)
                inserted_count += 1

            if device:
                device.last_punch_at = timezone.now()
                device.save(update_fields=['last_punch_at', 'updated'])

            response = HttpResponse('result=OK', content_type='text/plain')
            response['response_code'] = 'OK'
            response['result'] = 'OK'
            response['status'] = 'SUCCESS'
            response['Connection'] = 'close'
            return response
        else:
            # Fall back to standard ADMS text parser (table = ATTLOG)
            body_text_clean = body_text.replace('\x00', '')
            if table == 'ATTLOG' and body_text_clean:
                lines = [l.strip() for l in body_text_clean.split('\n') if l.strip()]
                for line in lines:
                    # Handle key-value style (e.g. PIN=101\tTime=...)
                    if '=' in line:
                        kv = {}
                        for item in line.replace('\t', ' ').split():
                            if '=' in item:
                                k, v = item.split('=', 1)
                                kv[k.upper()] = v
                        user_id = kv.get('PIN') or kv.get('USERID') or kv.get('USER_ID') or kv.get('ENROLLNUMBER')
                        punch_time_str = kv.get('TIME') or kv.get('PUNCHTIME') or kv.get('DATETIME') or str(timezone.now())
                        verify_mode = kv.get('STATUS') or kv.get('VERIFY') or ''
                    elif '\t' in line:
                        # Tab separated: user_id \t punch_time \t verify_mode
                        parts = [p.strip() for p in line.split('\t') if p.strip()]
                        user_id = parts[0] if len(parts) > 0 else ''
                        punch_time_str = parts[1] if len(parts) > 1 else str(timezone.now())
                        verify_mode = parts[2] if len(parts) > 2 else ''
                    elif ',' in line:
                        # Comma separated
                        parts = [p.strip() for p in line.split(',') if p.strip()]
                        user_id = parts[0] if len(parts) > 0 else ''
                        punch_time_str = parts[1] if len(parts) > 1 else str(timezone.now())
                        verify_mode = parts[2] if len(parts) > 2 else ''
                    else:
                        # Space separated: "101 2026-08-25 13:10:00 1 1"
                        parts = line.split()
                        if len(parts) >= 3 and '-' in parts[1] and ':' in parts[2]:
                            user_id = parts[0]
                            punch_time_str = f"{parts[1]} {parts[2]}"
                            verify_mode = parts[3] if len(parts) > 3 else ''
                        elif len(parts) >= 1:
                            user_id = parts[0]
                            punch_time_str = str(timezone.now())
                            verify_mode = ''
                        else:
                            continue

                    if user_id:
                        payload = {
                            'user_id': user_id,
                            'punch_time': punch_time_str,
                            'verify_mode': verify_mode,
                            'device_id': sn or (device.device_id if device else '1'),
                            'source_ip': source_ip,
                        }
                        process_biometric_punch(payload, protocol='adms_push', source_ip=source_ip, device=device)
                        inserted_count += 1

        if device:
            device.last_punch_at = timezone.now()
            device.save(update_fields=['last_punch_at', 'updated'])

        return HttpResponse("OK\n" if inserted_count == 0 else f"OK: {inserted_count}\n", content_type='text/plain')


@csrf_exempt
def iclock_getrequest(request):
    """
    Standard ZKTeco / eSSL ADMS command polling endpoint for /iclock/getrequest.
    """
    sn = (request.GET.get('SN') or request.GET.get('sn') or '').strip().replace('\x00', '')
    if sn:
        device = (
            BiometricDevice.objects.filter(device_id=sn).first()
            or BiometricDevice.objects.filter(serial_number=sn).first()
            or BiometricDevice.objects.filter(terminal_id=sn).first()
        )
        if not device:
            device = BiometricDevice.objects.filter(is_active=True).first()
        if device:
            device.last_seen_at = timezone.now()
            device.save(update_fields=['last_seen_at', 'updated'])

    return HttpResponse("OK\n", content_type='text/plain')


@csrf_exempt
def iclock_devicecmd(request):
    """
    Standard ZKTeco / eSSL ADMS command result endpoint for /iclock/devicecmd.
    """
    return HttpResponse("OK\n", content_type='text/plain')


@csrf_exempt
def iclock_registry(request):
    """
    Standard ZKTeco / eSSL ADMS device registration endpoint for /iclock/registry.
    """
    sn = (request.GET.get('SN') or request.GET.get('sn') or '').strip().replace('\x00', '')
    if sn:
        device = (
            BiometricDevice.objects.filter(device_id=sn).first()
            or BiometricDevice.objects.filter(serial_number=sn).first()
            or BiometricDevice.objects.filter(terminal_id=sn).first()
        )
        if not device:
            device = BiometricDevice.objects.filter(is_active=True).first()
        if device:
            device.last_seen_at = timezone.now()
            device.save(update_fields=['last_seen_at', 'updated'])
    return HttpResponse("OK\n", content_type='text/plain')


def latest_events_api(request):
    """
    API endpoint for auto-refreshing biometric events and attendance in real time.
    Returns latest event ID and event details as JSON.
    """
    events = BiometricEventLog.objects.select_related('employee', 'manager').order_by('-id')[:20]
    data = []
    latest_id = 0
    if events:
        latest_id = events[0].id

    for e in events:
        person_name = "Unmatched"
        if e.employee:
            person_name = f"{e.employee.employee_first_name} {e.employee.employee_last_name}"
        elif e.manager:
            person_name = f"{e.manager.manager_first_name} {e.manager.manager_last_name}"

        data.append({
            'id': e.id,
            'user_id': e.biometric_user_id,
            'person': person_name,
            'protocol': e.get_protocol_display(),
            'status': e.get_status_display(),
            'punch_time': e.punch_time.strftime('%b. %d, %Y, %I:%M %p') if e.punch_time else '',
        })

    return JsonResponse({
        'status': 'success',
        'latest_id': latest_id,
        'count': len(data),
        'events': data,
    })
