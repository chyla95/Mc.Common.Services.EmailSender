# .\Create-Packages.ps1

param(
    [string]$configuration = "Release"  # Default configuration is Release
)

$projectFilePaths = @(
    "..\..\src\Mc.Common.Services.EmailSender\Mc.Common.Services.EmailSender.csproj",
    "..\..\src\Mc.Common.Services.EmailSender.Abstractions\Mc.Common.Services.EmailSender.Abstractions.csproj",
    "..\..\src\Mc.Common.Services.EmailSender.DependencyInjection\Mc.Common.Services.EmailSender.DependencyInjection.csproj"
)

foreach ($projectFilePath in $projectFilePaths) {
    Write-Host "Building package for $projectFilePath in $configuration mode..."
    dotnet pack $projectFilePath --configuration $configuration
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Failed to pack $projectFilePath" -ForegroundColor Red
        exit 1
    }
}

Write-Host "All packages built successfully in '$configuration' mode!" -ForegroundColor Green
