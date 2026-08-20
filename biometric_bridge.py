import os
import socket
import time
from datetime import datetime

import requests

try:
    from zk import ZK
except ImportError as exc:
    raise ImportError(
        "Missing dependency 'pyzk'. Install it with: pip install pyzk"
    ) from exc


DEVICE_IP = os.getenv('BIOMETRIC_DEVICE_IP', '192.168.0.150')
DEVICE_PORT = int(os.getenv('BIOMETRIC_DEVICE_PORT', '4370'))
DEVICE_ID = os.getenv('BIOMETRIC_DEVICE_ID', 'hrms-device-01')
SERVER_URL = os.getenv('BIOMETRIC_SERVER_URL', 'http://127.0.0.1:8000/api/attendance/biometric-punch/')
DEVICE_SECRET_KEY = os.getenv('BIOMETRIC_DEVICE_SECRET_KEY', '')
POLL_SECONDS = int(os.getenv('BIOMETRIC_POLL_SECONDS', '5'))
DEVICE_PASSWORD = int(os.getenv('BIOMETRIC_DEVICE_PASSWORD', '0'))
DEVICE_TIMEOUT = int(os.getenv('BIOMETRIC_DEVICE_TIMEOUT', '20'))
FORCE_UDP = os.getenv('BIOMETRIC_FORCE_UDP', '').strip().lower()


def print_network_diagnostics():
    print('[DIAG] Network diagnostics')
    print(f'[DIAG] Target device: {DEVICE_IP}:{DEVICE_PORT}')
    try:
        hostname = socket.gethostname()
        local_ips = socket.gethostbyname_ex(hostname)[2]
        print(f'[DIAG] Laptop host: {hostname}')
        print(f'[DIAG] Laptop IPs: {", ".join(local_ips)}')
    except Exception as exc:
        print(f'[DIAG] Could not read laptop IPs: {exc}')

    try:
        with socket.create_connection((DEVICE_IP, DEVICE_PORT), timeout=5):
            print(f'[DIAG] TCP port reachable: {DEVICE_IP}:{DEVICE_PORT}')
    except OSError as exc:
        print(f'[DIAG] TCP port not reachable: {DEVICE_IP}:{DEVICE_PORT} ({exc})')
        if DEVICE_PORT != 4370:
            print('[DIAG] Bridge-pull devices commonly use port 4370. If this machine is not in push mode, try port 4370.')


def post_punch(log):
    payload = {
        'user_id': str(log.user_id),
        'device_id': DEVICE_ID,
        'verify_mode': str(getattr(log, 'status', '') or getattr(log, 'punch', '') or ''),
        'punch_time': log.timestamp.strftime('%Y-%m-%d %H:%M:%S'),
    }
    headers = {'Content-Type': 'application/json'}
    if DEVICE_SECRET_KEY:
        headers['X-Device-Token'] = DEVICE_SECRET_KEY

    response = requests.post(SERVER_URL, json=payload, headers=headers, timeout=10)
    response.raise_for_status()
    return response.json()


def start_bridge():
    print('=' * 60)
    print(f'HRMS Biometric Bridge - Device ID: {DEVICE_ID}')
    print(f'Connecting to biometric device {DEVICE_IP}:{DEVICE_PORT}')
    print(f'Syncing punches to: {SERVER_URL}')
    print('=' * 60)
    print_network_diagnostics()

    if FORCE_UDP in {'true', '1', 'yes'}:
        udp_modes = [True]
    elif FORCE_UDP in {'false', '0', 'no'}:
        udp_modes = [False]
    else:
        udp_modes = [False, True]

    conn = None

    try:
        last_error = None
        for force_udp in udp_modes:
            try:
                print(f'[SDK] Trying pyzk connect force_udp={force_udp} password={DEVICE_PASSWORD} timeout={DEVICE_TIMEOUT}')
                zk = ZK(
                    DEVICE_IP,
                    port=DEVICE_PORT,
                    timeout=DEVICE_TIMEOUT,
                    password=DEVICE_PASSWORD,
                    force_udp=force_udp,
                    ommit_ping=True,
                )
                conn = zk.connect()
                print(f'[SUCCESS] Connected to biometric device with force_udp={force_udp}.')
                break
            except Exception as exc:
                last_error = exc
                print(f'[SDK] Connection attempt failed force_udp={force_udp}: {exc}')

        if not conn:
            raise last_error or RuntimeError('Could not connect to biometric device.')

        try:
            conn.voice_test(10)
        except Exception:
            pass

        last_checked_punch_time = datetime.now()

        while True:
            if not conn:
                print('[WARNING] Connection lost. Reconnecting...')
                conn = zk.connect()

            try:
                for log in conn.get_attendance():
                    if log.timestamp <= last_checked_punch_time:
                        continue

                    print(f'[PUNCH] user_id={log.user_id} time={log.timestamp}')
                    try:
                        data = post_punch(log)
                        print(f'  [SYNCED] {data}')
                    except Exception as post_err:
                        print(f'  [FAILED] Could not sync punch: {post_err}')
                        with open('biometric_bridge_errors.log', 'a') as error_log:
                            error_log.write(f'[{datetime.now()}] post error for {log.user_id}: {post_err}\n')

                    last_checked_punch_time = log.timestamp
            except Exception as loop_err:
                print(f'[ERROR] Device polling error: {loop_err}')
                time.sleep(POLL_SECONDS)

            time.sleep(POLL_SECONDS)

    except Exception as device_err:
        print(f'[CRITICAL] Device connection failed: {device_err}')
        with open('biometric_bridge_errors.log', 'a') as error_log:
            error_log.write(f'[{datetime.now()}] device error: {device_err}\n')
        time.sleep(30)
        start_bridge()
    finally:
        if conn:
            try:
                conn.disconnect()
            except Exception:
                pass


if __name__ == '__main__':
    while True:
        try:
            start_bridge()
        except KeyboardInterrupt:
            print('\n[STOPPED] Biometric bridge stopped.')
            break
        except Exception as exc:
            print(f'[RESTARTING] Bridge crashed: {exc}. Restarting in 10s...')
            time.sleep(10)
