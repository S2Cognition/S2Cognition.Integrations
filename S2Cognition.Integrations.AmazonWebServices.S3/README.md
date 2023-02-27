#  S2Cognition.Integrations.AmazonWebServices.S3

Subproject of [S2Cognition.Integrations](../readme.md)

## QuickStart Example

1. Include the necessary NuGet packages into your project: `S2Cognition.Integrations.AmazonWebServices.S3`
2. Initialize your IoC container: `serviceCollection.AddAmazonWebServicesS3Integration();`
3. Create the appropriate configuration object:
    ```
    var configuration = new AmazonWebServicesS3Configuration(serviceProvider)
    {
        AccessKey = "your AccessKey",
        SecretKey = "your SecretKey",
        AwsRegion = "your AwsRegion",
        ServiceUrl = "your ServiceUrl"
    };
    ```
4. Initialize the integration:
    ```
    var integration = serviceProvider.GetRequiredService<IAmazonWebServicesS3Integration>();
    await integration.Initialize(configuration);
    ```
5. Call the integration Api's: `await integration.DownloadS3File(new DownloadS3FileRequest{...});`

## Public objects

* Configuration Object: `AmazonWebServicesS3Configuration`
* Integration Service: `IAmazonWebServicesS3Integration`

## Api's

* `async Task<DownloadS3FileResponse?> DownloadS3File(DownloadS3FileRequest req)`
* `async Task<UploadS3FileResponse?> UploadS3File(UploadS3FileRequest req)`
