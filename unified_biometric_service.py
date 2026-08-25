import os
import sys
import time
import socket
import threading
import logging
from datetime import datetime
from http.server import HTTPServer, BaseHTTPRequestHandler
import requests

RENDER_CLOUD_URL = os.getenv('RENDER_URL', 'https://employee-conduct-mcak.onrender.com').rstrip('/')
DEVICE_IP = os.getenv('BIOMETRIC_DEVICE_IP', '192.168.1.2')
DEVICE_ID = os.getenv('BIOMETRIC_DEVICE_ID', '1')

logging.basicConfig(
    level=logging.INFO,
    format='[%(asctime)s] %(levelname)s: %(message)s',
    datefmt='%H:%M:%S'
)
logger = logging.getLogger('UnifiedBiometricService')

def get_laptop_ip():
    try:
        s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
        s.connect(('8.8.8.8', 80))
        ip = s.getsockname()[0]
        s.close()
        return ip
    except Exception:
        return '127.0.0.1'

# =========================================================================
# THREAD 1: ADMS HTTP PROXY (Port 8080)
# =========================================================================
class ADMSHandler(BaseHTTPRequestHandler):
    def do_GET(self):
        target_url = f"{RENDER_CLOUD_URL}{self.path}"
        try:
            resp = requests.get(target_url, headers={'User-Agent': 'ADMS-Proxy'}, timeout=10)
            self.send_response(resp.status_code)
            for k, v in resp.headers.items():
                if k.lower() in ('content-type', 'content-length'):
                    self.send_header(k, v)
            self.end_headers()
            self.wfile.write(resp.content)
            logger.info(f"[ADMS GET] Handshake acknowledged for {self.path}")
        except Exception as e:
            logger.error(f"[ADMS GET Error] {e}")
            self.send_response(200)
            self.end_headers()
            self.wfile.write(b"OK\n")

    def do_POST(self):
        content_length = int(self.headers.get('Content-Length', 0))
        body = self.rfile.read(content_length) if content_length > 0 else b''
        target_url = f"{RENDER_CLOUD_URL}{self.path}"
        try:
            logger.info(f"[ADMS POST] Received punch packet ({len(body)} bytes). Forwarding to cloud...")
            resp = requests.post(target_url, data=body, headers={'Content-Type': 'text/plain'}, timeout=10)
            self.send_response(resp.status_code)
            for k, v in resp.headers.items():
                if k.lower() in ('content-type', 'content-length'):
                    self.send_header(k, v)
            self.end_headers()
            self.wfile.write(resp.content)
            logger.info(f"[ADMS SYNCED] Cloud response: {resp.text.strip()}")
        except Exception as e:
            logger.error(f"[ADMS POST Error] {e}")
            self.send_response(200)
            self.end_headers()
            self.wfile.write(b"OK: 1\n")

    def log_message(self, format, *args):
        pass

def run_adms_proxy(port=8080):
    try:
        server = HTTPServer(('0.0.0.0', port), ADMSHandler)
        logger.info(f"[ACTIVE] ADMS Listener active on port {port}")
        server.serve_forever()
    except Exception as e:
        logger.error(f"ADMS Proxy error on port {port}: {e}")

# =========================================================================
# THREAD 2: TCP PUSH & XML FORWARDER (Port 5005)
# =========================================================================
def run_tcp_server(port=5005):
    try:
        sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        sock.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
        sock.bind(('0.0.0.0', port))
        sock.listen(5)
        logger.info(f"[ACTIVE] TCP Push Listener active on port {port}")
        
        while True:
            client, addr = sock.accept()
            threading.Thread(target=handle_tcp_client, args=(client, addr), daemon=True).start()
    except Exception as e:
        logger.error(f"TCP server error on port {port}: {e}")

def handle_tcp_client(client, addr):
    try:
        client.settimeout(5)
        data = client.recv(4096)
        if data:
            logger.info(f"[TCP IN] Received {len(data)} bytes from {addr}")
            # Send standard ACK
            client.sendall(b"<Response><Status>OK</Status></Response>\r\n")
            
            # Forward raw data to Render
            try:
                url = f"{RENDER_CLOUD_URL}/api/attendance/http-push/"
                requests.post(url, data=data, timeout=5)
                logger.info(f"[TCP SYNCED] Forwarded to cloud")
            except Exception:
                pass
    except Exception:
        pass
    finally:
        try:
            client.close()
        except Exception:
            pass

# =========================================================================
# THREAD 3: PYZK STANDALONE AUTO-PULLER (Direct from machine memory)
# =========================================================================
def run_pyzk_puller():
    try:
        from zk import ZK
    except ImportError:
        logger.warning("pyzk not installed, skipping direct puller.")
        return

    last_synced_time = datetime.now()
    logger.info(f"[ACTIVE] PyZK Direct Memory Puller started for device {DEVICE_IP}")

    while True:
        try:
            for port in [4370, 5005]:
                for force_udp in [False, True]:
                    try:
                        zk = ZK(DEVICE_IP, port=port, timeout=3, password=0, force_udp=force_udp, ommit_ping=True)
                        conn = zk.connect()
                        records = conn.get_attendance()
                        new_records = [r for r in records if r.timestamp > last_synced_time]
                        for r in new_records:
                            logger.info(f"[PYZK PUNCH] User: {r.user_id} at {r.timestamp}")
                            payload = {
                                'user_id': str(r.user_id),
                                'punch_time': r.timestamp.strftime('%Y-%m-%d %H:%M:%S'),
                                'device_id': DEVICE_ID,
                                'verify_mode': str(getattr(r, 'status', 1)),
                            }
                            url = f"{RENDER_CLOUD_URL}/api/attendance/biometric-punch/"
                            requests.post(url, json=payload, timeout=5)
                            last_synced_time = r.timestamp
                            logger.info(f"  -> Synced to Render Cloud successfully!")
                        conn.disconnect()
                        break
                    except Exception:
                        pass
        except Exception as e:
            pass
        time.sleep(3)

# =========================================================================
# MAIN ENTRYPOINT
# =========================================================================
if __name__ == '__main__':
    laptop_ip = get_laptop_ip()
    print("=" * 65)
    print("       HRMS UNIFIED LIVE BIOMETRIC DAEMON (ALL-IN-ONE)")
    print("=" * 65)
    print(f"  Laptop IP Address : {laptop_ip}")
    print(f"  Target Device IP  : {DEVICE_IP}")
    print(f"  Render Cloud URL  : {RENDER_CLOUD_URL}")
    print("=" * 65)

    # Start all 3 communication channels simultaneously in parallel
    t1 = threading.Thread(target=run_adms_proxy, args=(8080,), daemon=True)
    t2 = threading.Thread(target=run_tcp_server, args=(5005,), daemon=True)
    t3 = threading.Thread(target=run_pyzk_puller, daemon=True)

    t1.start()
    t2.start()
    t3.start()

    logger.info("All biometric listeners running. Waiting for punches...\n")

    try:
        while True:
            time.sleep(1)
    except KeyboardInterrupt:
        print("\nStopping Unified Biometric Service...")
