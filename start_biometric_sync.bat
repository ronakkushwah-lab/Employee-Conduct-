@echo off
title HRMS Biometric Live Cloud Sync
cd /d "%~dp0"
echo ============================================================
echo   Starting HRMS Biometric Sync Daemon...
echo ============================================================
if exist ".venv-fresh\Scripts\python.exe" (
    ".venv-fresh\Scripts\python.exe" unified_biometric_service.py %*
) else if exist ".venv\Scripts\python.exe" (
    ".venv\Scripts\python.exe" unified_biometric_service.py %*
) else (
    python unified_biometric_service.py %*
)
pause
