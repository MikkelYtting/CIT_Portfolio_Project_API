# Runs all tests in the solution from the repo root
param(
    [string]$Filter
)

$solution = Join-Path $PSScriptRoot 'CIT_Portfolio_Project_API.sln'
if (-not (Test-Path $solution)) {
    Write-Error "Solution file not found: $solution"
    exit 1
}

$cmd = @('dotnet', 'test', $solution)
if ($Filter) { $cmd += @('--filter', $Filter) }

Write-Host "Running: $($cmd -join ' ')" -ForegroundColor Cyan
& $cmd[0] $cmd[1..($cmd.Length-1)]
exit $LASTEXITCODE
