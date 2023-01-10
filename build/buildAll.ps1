
pushd .

try
{
    $buildConfiguration = "Release"

    $versionJson = Get-Content version.json  -Raw | ConvertFrom-Json 
    $versionMajor = $versionJson.Version.VersionMajor
    $versionMinor = $versionJson.Version.VersionMinor
    $versionPatch = [int]$versionJson.Version.VersionPatch + 1

    $newVersion = "$versionMajor.$versionMinor.$versionPatch"

    Write-Host "Creating version: $newVersion"

    cd ..
    
    dotnet build  --configuration $buildConfiguration
    if($? -eq 0)
    {
        throw "Build failed."
    }

    dotnet test --configuration $buildConfiguration 
    if($? -eq 0)
    {
        throw "Tests failed."
    }

    dotnet pack --configuration $buildConfiguration --no-build -p:PackageVersion=$newVersion
    if($? -eq 0)
    {
        throw "Nuget packaging failed."
    }

    #dotnet push --configuration $buildConfiguration --no-build -p:PackageVersion=$newVersion
    #if($? -eq 0)
    #{
    #    throw "Nuget package push failed."
    #}

    $versionJson.Version.VersionPatch = [string]$versionPatch
    $versionJson | ConvertTo-Json -Depth 4 | Out-File ./build/version.json  
}
finally
{
    popd
}
