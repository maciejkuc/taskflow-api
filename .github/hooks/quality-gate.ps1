$errors = 0
Write-Host "🔬 TaskFlow Quality Gate"
dotnet format TaskFlow.sln --verify-no-changes
if ($LASTEXITCODE -ne 0) { Write-Host "❌ Format"; $errors++ } else { Write-Host "✅ Format OK" }
dotnet build TaskFlow.sln --no-restore -p:TreatWarningsAsErrors=true --verbosity quiet
if ($LASTEXITCODE -ne 0) { Write-Host "❌ Build"; $errors++ } else { Write-Host "✅ Build OK" }
if ($errors -gt 0) { Write-Host "🚫 NIEUDANY ($errors)"; exit 1 }
Write-Host "✅ ZALICZONY"