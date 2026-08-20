import json

from django.http import JsonResponse
from django.utils import timezone
from django.views.decorators.csrf import csrf_exempt
from django.views.decorators.http import require_http_methods

from .models import BiometricDevice
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
