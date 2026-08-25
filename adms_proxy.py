import os
import sys
import socket
from http.server import HTTPServer, BaseHTTPRequestHandler
import requests

RENDER_BASE_URL = os.getenv('RENDER_URL', 'https://employee-conduct-mcak.onrender.com').rstrip('/')
LOCAL_PORT = int(os.getenv('ADMS_LOCAL_PORT', '8080'))

def get_laptop_ip():
    try:
        s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
        s.connect(('8.8.8.8', 80))
        ip = s.getsockname()[0]
        s.close()
        return ip
    except Exception:
        return '127.0.0.1'

class ADMSProxyHandler(BaseHTTPRequestHandler):
    def do_GET(self):
        target_url = f"{RENDER_BASE_URL}{self.path}"
        try:
            print(f"[ADMS IN] GET {self.path}")
            resp = requests.get(target_url, headers={'User-Agent': 'ADMS-Proxy'}, timeout=10)
            self.send_response(resp.status_code)
            for k, v in resp.headers.items():
                if k.lower() in ('content-type', 'content-length'):
                    self.send_header(k, v)
            self.end_headers()
            self.wfile.write(resp.content)
            print(f"[ADMS OUT] Handshake response sent to machine (HTTP {resp.status_code})")
        except Exception as e:
            print(f"[ERROR] Forwarding GET failed: {e}")
            self.send_response(500)
            self.end_headers()

    def do_POST(self):
        content_length = int(self.headers.get('Content-Length', 0))
        body = self.rfile.read(content_length) if content_length > 0 else b''
        target_url = f"{RENDER_BASE_URL}{self.path}"
        try:
            print(f"[ADMS IN] POST {self.path} (Payload: {len(body)} bytes)")
            resp = requests.post(target_url, data=body, headers={'Content-Type': 'text/plain'}, timeout=10)
            self.send_response(resp.status_code)
            for k, v in resp.headers.items():
                if k.lower() in ('content-type', 'content-length'):
                    self.send_header(k, v)
            self.end_headers()
            self.wfile.write(resp.content)
            print(f"[ADMS OUT] Punch response sent to machine: {resp.text.strip()}")
        except Exception as e:
            print(f"[ERROR] Forwarding POST failed: {e}")
            self.send_response(500)
            self.end_headers()

    def log_message(self, format, *args):
        pass  # Custom logging in do_GET/do_POST

def run():
    laptop_ip = get_laptop_ip()
    print("=" * 65)
    print(f"       HRMS LOCAL ADMS PROXY FOR ZKTECO / ESSL / REALTIME")
    print("=" * 65)
    print(f" Laptop Local IP Address : {laptop_ip}")
    print(f" Listening on Port       : {LOCAL_PORT}")
    print(f" Forwarding to Cloud     : {RENDER_BASE_URL}")
    print("-" * 65)
    print(f" ON YOUR BIOMETRIC MACHINE:")
    print(f"   1. Menu -> Comm. -> Cloud Server / ADMS")
    print(f"   2. Server Address : {laptop_ip}")
    print(f"   3. Server Port    : {LOCAL_PORT}")
    print(f"   4. Enable SSL     : OFF")
    print("=" * 65)
    print(" Waiting for machine connection...\n")

    server = HTTPServer(('0.0.0.0', LOCAL_PORT), ADMSProxyHandler)
    try:
        server.serve_forever()
    except KeyboardInterrupt:
        print("\nStopping ADMS Proxy...")
        server.server_close()

if __name__ == '__main__':
    run()
