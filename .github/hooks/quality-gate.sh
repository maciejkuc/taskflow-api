#!/usr/bin/env bash
set -euo pipefail
ERRORS=0
echo "🔬 TaskFlow Quality Gate"

echo "▶ [1/2] dotnet format..."
if ! dotnet format TaskFlow.sln --verify-no-changes 2>&1; then
    echo "❌ Błędy formatowania – uruchom: dotnet format TaskFlow.sln"
    ERRORS=$((ERRORS+1))
else echo "✅ Format OK"; fi

echo "▶ [2/2] dotnet build (analyzers)..."
if ! dotnet build TaskFlow.sln --no-restore -p:TreatWarningsAsErrors=true --verbosity quiet 2>&1; then
    echo "❌ Build / analyzery nieudane"
    ERRORS=$((ERRORS+1))
else echo "✅ Build OK"; fi

[ "$ERRORS" -gt 0 ] && { echo "🚫 Quality gate NIEUDANY ($ERRORS problem(y))"; exit 1; }
echo "✅ Quality gate ZALICZONY"