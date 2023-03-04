param (
	[String] $apikey = $null,
    [Switch] $pushNuget = $false,
    [Switch] $help = $false
)

pushd .

try
{
    cd ..

    if($help) {
        Write-Host 
        Write-Host "-help:"
        Write-Host "    .\buildAll.ps1"
        Write-Host 
        Write-Host "        -apiKey"
        Write-Host "            The script will accept the ApiKey for the push to Nuget."
        Write-Host 
        Write-Host "        -pushNuget"
        Write-Host "            If supplied, will increment the version of the generated Nuget packages"
        Write-Host "            and push the generated packages to Nuget."
        Write-Host 
        Write-Host "        -help"
        Write-Host "            Display help for options available to the script."
        Write-Host 
        exit;
    }

    $buildConfiguration = "Release"
    $nugetPackageFolder = ".\Nupkgs";
    $nugetFileGlob = "*.nupkg"

    $includePackages = @(
        "S2Cognition.Integrations.Core*.nupkg", `
        "S2Cognition.Integrations.AmazonWebServices.Core*.nupkg", `
        "S2Cognition.Integrations.AmazonWebServices.CloudWatch*.nupkg", `
        "S2Cognition.Integrations.AmazonWebServices.DynamoDb*.nupkg", `
        "S2Cognition.Integrations.AmazonWebServices.S3*.nupkg", `
        "S2Cognition.Integrations.AmazonWebServices.Ses*.nupkg", `
        "S2Cognition.Integrations.AmazonWebServices.Ssm*.nupkg", `
        "S2Cognition.Integrations.Google.Core*.nupkg", `
        "S2Cognition.Integrations.Google.Analytics*.nupkg", `
        "S2Cognition.Integrations.Microsoft.Core*.nupkg", `
        "S2Cognition.Integrations.Microsoft.AzureDevOps*.nupkg", `
        "S2Cognition.Integrations.Monday.Core*.nupkg", `
        "S2Cognition.Integrations.NetSuite.Core*.nupkg", `
        "S2Cognition.Integrations.StreamDeck.Core*.nupkg", `
        "S2Cognition.Integrations.StreamDeck.AwsAlarmMonitor*.nupkg", `
        "S2Cognition.Integrations.StreamDeck.AzdoPipelineMonitor*.nupkg", `
        "S2Cognition.Integrations.Zoom.Core*.nupkg", `
        "S2Cognition.Integrations.Zoom.Phones*.nupkg" `
    )

    Write-Host
    Write-Host "Building Projects"

    dotnet build  --configuration $buildConfiguration
    if($? -eq 0)
    {
        throw "Build failed."
    }

    Write-Host
    Write-Host "Testing Projects"

    dotnet test --configuration $buildConfiguration 
    if($? -eq 0)
    {
        throw "Tests failed."
    }

    if($pushNuget)
    {
        Write-Host
        Write-Host "Deleting old packages"
    
        if(Test-Path -Path $nugetPackageFolder)
        {
            Get-ChildItem "$nugetPackageFolder\$nugetFileGlob" | Remove-Item -Force
        }
        else
        {
            New-Item -ItemType Directory $nugetPackageFolder
        }

        Write-Host
        Write-Host "Creating Nuget Packages" 

        $versionJson = Get-Content .\build\version.json  -Raw | ConvertFrom-Json 
        $versionMajor = $versionJson.Version.VersionMajor
        $versionMinor = $versionJson.Version.VersionMinor
        $versionPatch = [int]$versionJson.Version.VersionPatch
        $newVersion = "$versionMajor.$versionMinor.$versionPatch"
        
        Write-Host
        Write-Host "Creating version: $newVersion"

        if([String]::IsNullOrWhiteSpace($apikey))
        {
            $apiKey =  read-host "Enter API Key"
        }

        dotnet pack --configuration $buildConfiguration --no-build -p:PackageVersion=$newVersion --output ./Nupkgs
        if($? -eq 0)
        {
            throw "Nuget packaging failed."
        }

        Write-Host
        Write-Host "Pushing Nuget Packages"

        $packages = Get-ChildItem "$nugetPackageFolder\$nugetFileGlob" -Include $includePackages 
        foreach($package in $packages)
        {
            Write-Host $package
            dotnet nuget push $package --api-key $apikey --source https://api.nuget.org/v3/index.json  --skip-duplicate --no-symbols 
        }

        if($? -eq 0)
        {
            throw "Nuget package push failed."
        }

        $versionJson.Version.VersionPatch = [string]($versionPatch + 1)
        $versionJson | ConvertTo-Json -Depth 4 | Out-File ./build/version.json  
    }
}
finally
{
    popd
}
