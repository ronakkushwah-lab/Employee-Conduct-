import os
import sys
import json
import time
import socket
import threading
import logging
import argparse
from datetime import datetime
from http.server import HTTPServer, BaseHTTPRequestHandler
import requests

RENDER_CLOUD_URL = os.getenv('RENDER_URL', 'https://employee-conduct-mcak.onrender.com').rstrip('/')
DEFAULT_DEVICE_ID = os.getenv('BIOMETRIC_DEVICE_ID', '1')

logging.basicConfig(
    level=logging.INFO,
    format='[%(asctime)s] %(message)s',
    datefmt='%H:%M:%S'
)
logger = logging.getLogger('BiometricService')


def get_all_local_ips():
    ips = []
    try:
        s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
        s.connect(('8.8.8.8', 80))
        primary = s.getsockname()[0]
        s.close()
        ips.append(primary)
    except Exception:
        pass

    try:
        hostname = socket.gethostname()
        for ip in socket.gethostbyname_ex(hostname)[2]:
            if not ip.startswith('127.') and ip not in ips:
                ips.append(ip)
    except Exception:
        pass

    return ips or ['127.0.0.1']


# =========================================================================
# ADMS HTTP HANDLER (For Port 8080 & Port 80)
# =========================================================================
class ADMSHandler(BaseHTTPRequestHandler):
    def _normalize_path(self, path):
        clean = path
        if not clean.startswith('/iclock/') and not clean.startswith('/api/'):
            for prefix in ('/cdata', '/getrequest', '/devicecmd', '/registry', '/fdata', '/querydata', '/push'):
                if clean.startswith(prefix):
                    clean = '/iclock' + clean
                    break
        return clean

    def do_GET(self):
        norm_path = self._normalize_path(self.path)
        target_url = f"{RENDER_CLOUD_URL}{norm_path}"
        client_ip = self.client_address[0]
        try:
            resp = requests.get(target_url, headers={'User-Agent': 'ADMS-Proxy'}, timeout=8)
            self.send_response(resp.status_code)
            for k, v in resp.headers.items():
                if k.lower() in ('content-type', 'content-length'):
                    self.send_header(k, v)
            self.end_headers()
            self.wfile.write(resp.content)
            logger.info(f"🟢 [ADMS CONNECTED] Machine at {client_ip} sent handshake -> {self.path} (HTTP {resp.status_code})")
        except Exception as e:
            logger.warning(f"⚠️ [ADMS GET] Forward to Render: {e}. Replying with default ADMS options.")
            fallback_response = (
                "GET OPTION FROM: 1\n"
                "Stamp=9999\n"
                "OpStamp=9999\n"
                "PhotoStamp=0\n"
                "ErrorDelay=30\n"
                "Delay=5\n"
                "TransTimes=00:00;14:05\n"
                "TransInterval=1\n"
                "TransFlag=1111000000\n"
                "TimeZone=5.5\n"
                "Realtime=1\n"
                "Encrypt=0\n"
                "PushProtVer=2.4.1\n"
                "ServerVersion=3.1.1\n"
            )
            self.send_response(200)
            self.send_header('Content-Type', 'text/plain')
            self.end_headers()
            self.wfile.write(fallback_response.encode('utf-8'))

    def do_POST(self):
        content_length = int(self.headers.get('Content-Length', 0))
        body = self.rfile.read(content_length) if content_length > 0 else b''
        norm_path = self._normalize_path(self.path)
        target_url = f"{RENDER_CLOUD_URL}{norm_path}"
        client_ip = self.client_address[0]

        body_decoded = body.decode('utf-8', errors='ignore').strip()
        lines = [l for l in body_decoded.split('\n') if l.strip()]
        logger.info(f"⚡ [PUNCH RECEIVED] Incoming punch from machine ({client_ip}): {len(lines)} record(s)")
        for l in lines[:5]:
            logger.info(f"   ↳ Punch Data: {l}")

        try:
            resp = requests.post(target_url, data=body, headers={'Content-Type': 'text/plain'}, timeout=10)
            self.send_response(resp.status_code)
            for k, v in resp.headers.items():
                if k.lower() in ('content-type', 'content-length'):
                    self.send_header(k, v)
            self.end_headers()
            self.wfile.write(resp.content)
            cloud_result = resp.text.strip()
            logger.info(f"✅ [RENDER SYNC SUCCESS] Cloud Response: {cloud_result} (HTTP {resp.status_code})")
        except Exception as e:
            logger.error(f"❌ [RENDER SYNC FAILED] Forwarding to Render failed: {e}")
            self.send_response(200)
            self.send_header('Content-Type', 'text/plain')
            self.end_headers()
            self.wfile.write(b"OK: 1\n")

    def log_message(self, format, *args):
        pass


def run_adms_server(port):
    try:
        server = HTTPServer(('0.0.0.0', port), ADMSHandler)
        logger.info(f"🚀 [ACTIVE] ADMS Push Listener running on port {port}")
        server.serve_forever()
    except Exception as e:
        logger.warning(f"Could not bind ADMS listener on port {port}: {e}")


# =========================================================================
# SECUREYE & UNIVERSAL TCP / HTTP PUSH FORWARDER (Port 5005)
# =========================================================================
def run_tcp_server(port=5005):
    try:
        sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        sock.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
        sock.bind(('0.0.0.0', port))
        sock.listen(5)
        logger.info(f"🚀 [ACTIVE] Secureye / Realtime Listener running on port {port}")
        while True:
            client, addr = sock.accept()
            threading.Thread(target=handle_tcp_client, args=(client, addr), daemon=True).start()
    except Exception as e:
        logger.warning(f"TCP server error on port {port}: {e}")


def handle_tcp_client(client, addr):
    try:
        client.settimeout(6)
        data = client.recv(8192)
        if not data:
            return

        client_ip = addr[0]
        logger.info(f"⚡ [SECUREYE IN] Received {len(data)} bytes from {client_ip}")

        # Check if it's HTTP push (Secureye / Realtime)
        if data.startswith(b'POST ') or data.startswith(b'GET '):
            # Send Secureye HTTP ACK immediately so machine marks punch as delivered
            ack = (
                b'HTTP/1.1 200 OK\r\n'
                b'Content-Length: 0\r\n'
                b'Connection: close\r\n'
                b'Cache-Control: no-store\r\n'
                b'response_code: OK\r\n\r\n'
            )
            client.sendall(ack)

            # Parse Secureye HTTP Header and Body
            header_end = data.find(b'\r\n\r\n')
            if header_end != -1:
                head = data[:header_end].decode('iso-8859-1', errors='ignore')
                body = data[header_end + 4:]
                headers = {}
                for line in head.split('\r\n')[1:]:
                    if ':' in line:
                        k, v = line.split(':', 1)
                        headers[k.strip().lower()] = v.strip()

                raw_body = body.rstrip(b'\x00').strip()
                body_json = {}
                try:
                    body_json = json.loads(raw_body.decode('utf-8', errors='ignore'))
                except Exception:
                    if len(body) >= 4:
                        decl_len = int.from_bytes(body[:4], byteorder='little', signed=False)
                        try:
                            body_json = json.loads(body[4:4 + decl_len].rstrip(b'\x00').decode('utf-8', errors='ignore'))
                        except Exception:
                            pass

                user_id = str(body_json.get('user_id') or body_json.get('userId') or body_json.get('PIN') or body_json.get('EnrollNumber') or '').strip()
                dev_id = headers.get('dev_id') or body_json.get('dev_id') or '1'
                io_time = str(body_json.get('io_time') or '').strip()

                punch_time_str = datetime.now().strftime('%Y-%m-%d %H:%M:%S')
                if io_time and len(io_time) == 14:  # Format: YYYYMMDDHHMMSS
                    try:
                        dt = datetime.strptime(io_time, '%Y%m%d%H%M%S')
                        punch_time_str = dt.strftime('%Y-%m-%d %H:%M:%S')
                    except Exception:
                        pass

                if user_id:
                    logger.info(f"⚡ [SECUREYE PUNCH] Device: {dev_id} -> User PIN: {user_id}, Time: {punch_time_str}")
                    payload = {
                        'user_id': user_id,
                        'punch_time': punch_time_str,
                        'device_id': dev_id,
                        'verify_mode': str(body_json.get('verify_mode') or 1),
                    }
                    try:
                        resp = requests.post(f"{RENDER_CLOUD_URL}/api/attendance/biometric-punch/", json=payload, timeout=8)
                        logger.info(f"✅ [RENDER SYNC SUCCESS] Cloud Response: {resp.text.strip()} (HTTP {resp.status_code})")
                    except Exception as err:
                        logger.error(f"❌ [RENDER SYNC ERROR] {err}")
                else:
                    logger.info(f"ℹ️ [SECUREYE HEARTBEAT/DEVICE EVENT] Dev: {dev_id}, Data: {body_json}")
        else:
            # XML Push
            client.sendall(b"<Response><Status>OK</Status></Response>\r\n")
            try:
                url = f"{RENDER_CLOUD_URL}/api/attendance/http-push/"
                resp = requests.post(url, data=data, timeout=8)
                logger.info(f"✅ [XML-PUSH SYNCED] Saved to Render Cloud: {resp.text.strip()}")
            except Exception as e:
                logger.error(f"XML-push forward error: {e}")
    except Exception as exc:
        logger.warning(f"Client handler error from {addr}: {exc}")
    finally:
        try:
            client.close()
        except Exception:
            pass


# =========================================================================
# PYZK DIRECT MEMORY PULLER (Port 4370)
# =========================================================================
def run_pyzk_auto_scanner():
    try:
        from zk import ZK
    except ImportError:
        return

    last_synced_time = datetime.now()
    local_ips = get_all_local_ips()
    primary = local_ips[0]
    prefix = '.'.join(primary.split('.')[:3])
    known_ips = [
        f'{prefix}.2', f'{prefix}.100', f'{prefix}.105', f'{prefix}.106',
        f'{prefix}.116', f'{prefix}.150', f'{prefix}.201', f'{prefix}.224',
        '192.168.0.100', '192.168.0.105', '192.168.0.106', '192.168.0.116',
        '192.168.1.2', '192.168.1.3', '192.168.1.150', '192.168.1.201'
    ]

    while True:
        for ip in known_ips:
            for port in [4370, 5005]:
                for force_udp in [False, True]:
                    try:
                        zk = ZK(ip, port=port, timeout=2, password=0, force_udp=force_udp, ommit_ping=True)
                        conn = zk.connect()
                        records = conn.get_attendance()
                        new_records = [r for r in records if r.timestamp > last_synced_time]
                        if new_records:
                            for r in new_records:
                                logger.info(f"⚡ [PYZK PUNCH] Machine {ip} -> User PIN: {r.user_id} at {r.timestamp}")
                                payload = {
                                    'user_id': str(r.user_id),
                                    'punch_time': r.timestamp.strftime('%Y-%m-%d %H:%M:%S'),
                                    'device_id': DEFAULT_DEVICE_ID,
                                    'verify_mode': str(getattr(r, 'status', 1)),
                                }
                                url = f"{RENDER_CLOUD_URL}/api/attendance/biometric-punch/"
                                resp = requests.post(url, json=payload, timeout=5)
                                last_synced_time = r.timestamp
                                logger.info(f"✅ [PYZK SYNC SUCCESS] Render Cloud: {resp.text.strip()}")
                        conn.disconnect()
                        break
                    except Exception:
                        pass
        time.sleep(3)


# =========================================================================
# MAIN ENTRYPOINT
# =========================================================================
def main():
    local_ips = get_all_local_ips()
    primary_ip = local_ips[0]

    print("\n" + "=" * 72)
    print("   HRMS SECUREYE & MULTI-BRAND LIVE BIOMETRIC DAEMON -> RENDER")
    print("=" * 72)
    print(f"  [1] Your Laptop Wi-Fi IP Address : {primary_ip}")
    if len(local_ips) > 1:
        print(f"      Alternative Local IPs        : {', '.join(local_ips[1:])}")
    print(f"  [2] Active Listening Ports       : 5005 (Secureye), 8080, 80")
    print(f"  [3] Render Cloud Destination     : {RENDER_CLOUD_URL}")
    print("-" * 72)
    print("  ON YOUR SECUREYE BIOMETRIC MACHINE MENU:")
    print(f"   Step 1: Press Menu -> Network (or Comm.) -> Server (or Push Server)")
    print(f"   Step 2: Set Server IP           : {primary_ip}")
    print(f"   Step 3: Set Server Port         : 5005   (or 8080)")
    print(f"   Step 4: Set Push / Realtime     : Enable (ON)")
    print(f"   Step 5: RESTART the machine (Power OFF and ON)")
    print("=" * 72)
    print("  Listeners are ACTIVE. Waiting for incoming punches...\n")

    # Start Secureye / TimeOffice on port 5005
    t1 = threading.Thread(target=run_tcp_server, args=(5005,), daemon=True)
    t1.start()

    # Start TimeOffice BG PC on port 5555
    t1b = threading.Thread(target=run_tcp_server, args=(5555,), daemon=True)
    t1b.start()

    # Start ADMS on port 8080
    t2 = threading.Thread(target=run_adms_server, args=(8080,), daemon=True)
    t2.start()

    # Start ADMS on port 80 (standard HTTP)
    t3 = threading.Thread(target=run_adms_server, args=(80,), daemon=True)
    t3.start()

    # Start PyZK Auto-scanner on port 4370
    t4 = threading.Thread(target=run_pyzk_auto_scanner, daemon=True)
    t4.start()

    try:
        while True:
            time.sleep(1)
    except KeyboardInterrupt:
        print("\n[STOPPED] Biometric Sync Daemon stopped.")


if __name__ == '__main__':
    main()
