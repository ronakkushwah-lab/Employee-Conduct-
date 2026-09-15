import logging
import socketserver
from datetime import datetime as datetime_type
from http.client import HTTPMessage

import requests
from django.conf import settings
from django.core.management.base import BaseCommand
from django.utils import timezone

def _forward_punch_to_cloud(payload):
    try:
        url = 'https://employee-conduct-mcak.onrender.com/api/attendance/biometric-punch/'
        clean_payload = {
            'user_id': payload.get('UserID') or payload.get('user_id') or payload.get('EnrollNumber'),
            'punch_time': payload.get('punch_time'),
            'device_id': payload.get('DeviceSerialNo') or payload.get('device_id') or '1',
            'verify_mode': payload.get('VerifMode') or payload.get('verify_mode') or '',
            'event_type': payload.get('Event') or payload.get('event_type') or 'TimeLog',
        }
        requests.post(url, json=clean_payload, timeout=5)
    except Exception:
        pass

from biometric.direct_push import (
    build_secureye_http_ack,
    looks_like_http_request,
    normalize_secureye_http_event,
    parse_http_request,
)
from biometric.services import (
    extract_message_frames,
    normalize_tcp_xml_payload,
    parse_tcp_xml_payload,
    process_biometric_punch,
)

logger = logging.getLogger(__name__)


class BiometricTCPRequestHandler(socketserver.BaseRequestHandler):
    def handle(self):
        self.request.settimeout(settings.BIOMETRIC_TCP_SOCKET_TIMEOUT)
        max_payload_bytes = settings.BIOMETRIC_TCP_MAX_PAYLOAD_BYTES
        source_ip = self.client_address[0] if self.client_address else None
        self.processed_frame_count = 0
        self._handle_push_stream(max_payload_bytes, source_ip)

    @staticmethod
    def _expected_http_bytes(raw_request):
        header_end = raw_request.find(b'\r\n\r\n')
        if header_end == -1:
            return None

        header_text = raw_request[:header_end].decode('iso-8859-1', errors='ignore')
        headers = HTTPMessage()
        for line in header_text.split('\r\n')[1:]:
            if ':' not in line:
                continue
            key, value = line.split(':', 1)
            headers[key.strip()] = value.strip()
        content_length = int(headers.get('Content-Length', '0') or 0)
        return header_end + 4 + content_length

    def _handle_push_stream(self, max_payload_bytes, source_ip):
        chunks = []
        buffer = ''
        total_bytes = 0

        while True:
            try:
                chunk = self.request.recv(4096)
            except TimeoutError:
                break
            except OSError as exc:
                logger.warning('Biometric socket error from %s: %s', self.client_address, exc)
                break

            if not chunk:
                break

            chunks.append(chunk)
            total_bytes += len(chunk)
            if total_bytes > max_payload_bytes:
                logger.warning('Biometric payload exceeded max size from %s; truncating connection.', self.client_address)
                break

            joined = b''.join(chunks)
            if looks_like_http_request(joined):
                expected_http_bytes = self._expected_http_bytes(joined)
                if expected_http_bytes is not None and len(joined) >= expected_http_bytes:
                    self._handle_http_push(joined[:expected_http_bytes], source_ip)
                    return
                continue

            buffer += chunk.decode('utf-8', errors='ignore')
            frames, buffer = extract_message_frames(buffer)
            for frame in frames:
                self._process_tcp_xml_frame(frame, source_ip)
                self.processed_frame_count += 1

        if chunks and self.processed_frame_count == 0:
            self._log_unrecognized_payload(b''.join(chunks), source_ip)
        elif buffer.strip():
            self._log_unrecognized_payload(buffer.encode('utf-8', errors='replace'), source_ip, label='trailing or incomplete')

    @staticmethod
    def _log_unrecognized_payload(raw_payload, source_ip, label='unrecognized'):
        preview_size = getattr(settings, 'BIOMETRIC_TCP_DIAGNOSTIC_PREVIEW_BYTES', 512)
        preview = raw_payload[:preview_size]
        logger.warning(
            'Captured %s biometric TCP payload source=%s bytes=%s text=%r hex=%s',
            label,
            source_ip or 'unknown',
            len(raw_payload),
            preview.decode('utf-8', errors='backslashreplace'),
            preview.hex(' '),
        )

    def _handle_tcp_xml_push(self, raw_request, source_ip):
        ack_bytes = settings.BIOMETRIC_TCP_ACK_MESSAGE.encode('utf-8')
        buffer = raw_request.decode('utf-8', errors='ignore')
        frames, _remainder = extract_message_frames(buffer)
        if not frames and buffer.strip().startswith('<'):
            frames = [buffer.strip()]

        for frame in frames:
            self._process_tcp_xml_frame(frame, source_ip, ack_bytes)

    def _process_tcp_xml_frame(self, frame, source_ip, ack_bytes=None):
        try:
            payload = normalize_tcp_xml_payload(parse_tcp_xml_payload(frame))
            result = process_biometric_punch(
                payload,
                protocol='tcp_xml',
                source_ip=source_ip,
            )
            _forward_punch_to_cloud(payload)
            logger.info(
                'Processed biometric TCP XML event serial=%s event=%s status=%s',
                payload.get('DeviceSerialNo', ''),
                payload.get('Event', ''),
                result.get('status'),
            )
            ack_bytes = ack_bytes if ack_bytes is not None else settings.BIOMETRIC_TCP_ACK_MESSAGE.encode('utf-8')
            if ack_bytes:
                self.request.sendall(ack_bytes)
        except Exception as exc:
            logger.exception('Failed to process biometric TCP XML payload from %s: %s', source_ip, exc)
            self._log_unrecognized_payload(frame.encode('utf-8', errors='replace'), source_ip, label='failed')

    def _handle_http_push(self, raw_request, source_ip):
        try:
            http_request = parse_http_request(raw_request)
            payload, _body_data = normalize_secureye_http_event(http_request, source_ip=source_ip)

            punch_time = payload.get('punch_time')
            is_stale_timelog = False
            if payload.get('request_code') == 'realtime_glog' and punch_time:
                try:
                    punch_date = datetime_type.strptime(punch_time, '%Y-%m-%d %H:%M:%S').date()
                    is_stale_timelog = punch_date < timezone.localdate()
                except (TypeError, ValueError):
                    pass

            if is_stale_timelog:
                self.request.sendall(build_secureye_http_ack())
                logger.info(
                    'Fast-acknowledged stale biometric HTTP log serial=%s user=%s punch_time=%s',
                    payload.get('DeviceSerialNo', ''),
                    payload.get('UserID', ''),
                    punch_time,
                )
                return

            result = process_biometric_punch(
                payload,
                protocol='http',
                source_ip=source_ip,
            )
            _forward_punch_to_cloud(payload)
            logger.info(
                'Processed biometric HTTP push serial=%s request_code=%s user=%s trans_id=%s punch_time=%s status=%s',
                payload.get('DeviceSerialNo', ''),
                payload.get('request_code', ''),
                payload.get('UserID', ''),
                payload.get('TransID', ''),
                payload.get('punch_time', ''),
                result.get('status'),
            )
            self.request.sendall(build_secureye_http_ack())
        except Exception as exc:
            logger.exception('Failed to process biometric HTTP push payload from %s: %s', source_ip, exc)
            self.request.sendall(
                b'HTTP/1.1 500 Internal Server Error\r\n'
                b'Content-Length: 5\r\n'
                b'Connection: close\r\n\r\nERROR'
            )


class ThreadedTCPServer(socketserver.ThreadingMixIn, socketserver.TCPServer):
    allow_reuse_address = True
    daemon_threads = True


class Command(BaseCommand):
    help = 'Run the direct-push listener for biometric machines using TCP XML or Secureye-style HTTP push.'

    def add_arguments(self, parser):
        parser.add_argument('--host', default=settings.BIOMETRIC_TCP_HOST)
        parser.add_argument('--port', type=int, default=settings.BIOMETRIC_TCP_PORT)

    def handle(self, *args, **options):
        host = options['host']
        port = options['port']

        with ThreadedTCPServer((host, port), BiometricTCPRequestHandler) as server:
            self.stdout.write(self.style.SUCCESS(f'Biometric direct-push server listening on {host}:{port}'))
            try:
                server.serve_forever()
            except KeyboardInterrupt:
                self.stdout.write(self.style.WARNING('Biometric direct-push server stopped.'))
