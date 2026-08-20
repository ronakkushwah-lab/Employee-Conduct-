param(
    [string]$DeviceIp = "192.168.1.224",
    [int]$DevicePort = 4370,
    [string]$DeviceId = "hrms-device-01",
    [int]$MachineNumber = 1,
    [string]$ServerUrl = "http://127.0.0.1:8000/api/attendance/biometric-punch/",
    [int]$DevicePassword = 0,
    [int]$PollSeconds = 5
)

$ErrorActionPreference = "Continue"
$projectRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $projectRoot

$env:BIOMETRIC_DEVICE_IP = $DeviceIp
$env:BIOMETRIC_DEVICE_PORT = [string]$DevicePort
$env:BIOMETRIC_DEVICE_ID = $DeviceId
$env:BIOMETRIC_MACHINE_NUMBER = [string]$MachineNumber
$env:BIOMETRIC_SERVER_URL = $ServerUrl
$env:BIOMETRIC_DEVICE_PASSWORD = [string]$DevicePassword
$env:BIOMETRIC_POLL_SECONDS = [string]$PollSeconds

Write-Host "============================================================"
Write-Host "HRMS Biometric Bridge Live Logs"
Write-Host "Device ID : $DeviceId"
Write-Host "Machine # : $MachineNumber"
Write-Host "Device    : ${DeviceIp}:$DevicePort"
Write-Host "Password  : $DevicePassword"
Write-Host "Server    : $ServerUrl"
Write-Host "============================================================"
Write-Host ""
Write-Host "Quick network probe:"
Test-Connection -ComputerName $DeviceIp -Count 2 -Quiet | ForEach-Object {
    if ($_ -eq $true) { Write-Host "PING      : reachable" -ForegroundColor Green }
    else { Write-Host "PING      : no reply" -ForegroundColor Yellow }
}

try {
    $probe = Test-NetConnection -ComputerName $DeviceIp -Port $DevicePort -WarningAction SilentlyContinue
    if ($probe.TcpTestSucceeded) {
        Write-Host "TCP PORT  : open" -ForegroundColor Green
    } else {
        Write-Host "TCP PORT  : closed/timeout" -ForegroundColor Yellow
        if ($DevicePort -ne 4370) {
            Write-Host "TIP       : Bridge-pull machines usually listen on 4370, not $DevicePort." -ForegroundColor Cyan
        }
    }
} catch {
    Write-Host "TCP PORT  : probe failed - $($_.Exception.Message)" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "Starting bridge. Press Ctrl+C to stop."
Write-Host ""

$sdkBridge = Join-Path $projectRoot "sdk_bridge\bin\Release\net10.0-windows\win-x64\publish\HrmsSdkBridge.exe"
if (Test-Path $sdkBridge) {
    Write-Host "Bridge engine: Vendor SDK bridge" -ForegroundColor Green
    & $sdkBridge `
        --ip $DeviceIp `
        --port $DevicePort `
        --device-id $DeviceId `
        --machine-number $MachineNumber `
        --password $DevicePassword `
        --server-url $ServerUrl `
        --poll-seconds $PollSeconds
} else {
    Write-Host "Bridge engine: Python fallback (SDK bridge executable not built yet)" -ForegroundColor Yellow
    Write-Host "Expected SDK bridge at: $sdkBridge" -ForegroundColor Yellow
    & ".\.venv-fresh\Scripts\python.exe" -u "biometric_bridge.py"
}
