param(
    [string]$HostAddress = "0.0.0.0",
    [int]$Port = 5005
)

$ErrorActionPreference = "Continue"
$projectRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $projectRoot

$pythonExe = Join-Path $projectRoot ".venv-fresh\Scripts\python.exe"
if (-not (Test-Path $pythonExe)) {
    $pythonExe = "python"
}

Write-Host "============================================================"
Write-Host "HRMS Biometric TCP/XML Live Logs"
Write-Host "Listening : ${HostAddress}:$Port"
Write-Host "Command   : python manage.py run_biometric_tcp_server --host $HostAddress --port $Port"
Write-Host "============================================================"
Write-Host ""
Write-Host "Configure the biometric machine server/push target to this laptop IP and port $Port."
Write-Host "Press Ctrl+C to stop."
Write-Host ""

& $pythonExe "manage.py" "run_biometric_tcp_server" "--host" $HostAddress "--port" $Port
