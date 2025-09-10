param(
    [switch]$NoBuild,
    [string]$ResultsDir = "TestResultsCoverage",
    [string]$ReportDir = "coverage-report"
)

$ErrorActionPreference = 'Stop'

Push-Location (Split-Path $MyInvocation.MyCommand.Path -Parent) | Out-Null
# Move to repo root (scripts folder sibling of src)
Set-Location (Join-Path (Get-Location) "..")

if (-not (Test-Path src)) {
    Write-Error "Could not find src directory at repo root. Abort."; exit 1
}

$solution = Join-Path "src" "MapleLeaf.sln"
if (-not (Test-Path $solution)) {
    Write-Error "Solution file not found at $solution"; exit 1
}

$collectArg = '--collect:"XPlat Code Coverage"'
$resultsPath = Join-Path "src" $ResultsDir

$testArgs = @('test', $solution, '--results-directory', $resultsPath)
if ($NoBuild) { $testArgs += '--no-build' }
if ($collectArg) { $testArgs += $collectArg }

Write-Host "Running: dotnet $($testArgs -join ' ')" -ForegroundColor Cyan
dotnet @testArgs

if ($LASTEXITCODE -ne 0) { Write-Error "Tests failed."; exit $LASTEXITCODE }

# Find cobertura file
$cobertura = Get-ChildItem -Path $resultsPath -Recurse -Filter "coverage.cobertura.xml" | Select-Object -First 1
if (-not $cobertura) { Write-Error "Cobertura file not found under $resultsPath"; exit 1 }

Write-Host "Generating report from $($cobertura.FullName)" -ForegroundColor Cyan

# Ensure tool is available
dotnet tool run reportgenerator -reports:"$($cobertura.FullName)" -targetdir:"$ReportDir" -reporttypes:Html
if ($LASTEXITCODE -ne 0) { Write-Error "Report generation failed"; exit $LASTEXITCODE }

Write-Host "Report generated at $ReportDir/index.htm" -ForegroundColor Green

Pop-Location | Out-Null
