#  S2Cognition.Integrations.AmazonWebServices.Ses

Subproject of [S2Cognition.Integrations](../readme.md)

## QuickStart Example

1. Include the necessary NuGet packages into your project: `S2Cognition.Integrations.AmazonWebServices.Ses`
2. Initialize your IoC container: `serviceCollection.AddAmazonWebServicesSesIntegration();`
3. Create the appropriate configuration object:
    ```
    var configuration = new AmazonWebServicesSesConfiguration(serviceProvider)
    {
        AccessKey = "your AccessKey",
        SecretKey = "your SecretKey",
        AwsRegion = "your AwsRegion",
        ServiceUrl = "your ServiceUrl"
    };
    ```
4. Initialize the integration:
    ```
    var integration = serviceProvider.GetRequiredService<IAmazonWebServicesSesIntegration>();
    await integration.Initialize(configuration);
    ```
5. Call the integration Api's: `await integration.DownloadSesFile(new DownloadSesFileRequest{...});`

## Public objects

* Configuration Object: `AmazonWebServicesSesConfiguration`
* Integration Service: `IAmazonWebServicesSesIntegration`

## Api's

* `async Task<DownloadSesFileResponse?> DownloadSesFile(DownloadSesFileRequest req)`
* `async Task<UploadSesFileResponse?> UploadSesFile(UploadSesFileRequest req)`
