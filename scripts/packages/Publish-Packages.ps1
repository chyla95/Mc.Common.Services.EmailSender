# .\Publish-Packages.ps1 -nugetKey "ghp_..." -nugetSource "github" -packageVersion "1.0.1"

param(
    [string]$nugetKey,
    [string]$nugetSource = "github",   # Default source is "github"
    [string]$packageVersion = "1.0.0"  # Default version is "1.0.0"
)

$nugetConfigFilePath = "C:\Users\Mateusz\Documents\nuget.config";
$packageFilePaths = @(
    "..\..\src\Mc.Common.Services.EmailSender\bin\Release\Mc.Common.Services.EmailSender.$packageVersion.nupkg",
    "..\..\src\Mc.Common.Services.EmailSender.Abstractions\bin\Release\Mc.Common.Services.EmailSender.Abstractions.$packageVersion.nupkg",
    "..\..\src\Mc.Common.Services.EmailSender.DependencyInjection\bin\Release\Mc.Common.Services.EmailSender.DependencyInjection.$packageVersion.nupkg"
)

foreach ($packageFilePath in $packageFilePaths) {
    Write-Host "Pushing $packageFilePath to $nugetSource..."
    dotnet nuget push $packageFilePath --source $nugetSource --api-key $nugetKey --skip-duplicate
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Failed to push $packageFilePath" -ForegroundColor Red
        exit 1
    }
}

Write-Host "All packages pushed successfully to $nugetSource!" -ForegroundColor Green
