$sln = Get-ChildItem -Filter "*.sln" | Select-Object -First 1
$projects = Get-ChildItem -Recurse -Filter "*.csproj" | Select-Object -ExpandProperty FullName

foreach ($proj in $projects) {
    dotnet sln $sln.FullName add $proj 2>$null
}

Write-Host "Done!" -ForegroundColor Green