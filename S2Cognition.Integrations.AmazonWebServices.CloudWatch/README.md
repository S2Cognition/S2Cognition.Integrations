#  S2Cognition.Integrations.AmazonWebServices.Cloudwatch

Subproject of [S2Cognition.Integrations](../readme.md)

## QuickStart Example

1. Include the necessary NuGet packages into your project: `S2Cognition.Integrations.AmazonWebServices.Cloudwatch`
2. Initialize your IoC container: `serviceCollection.AddAmazonWebServicesCloudwatchIntegration();`
3. Create the appropriate configuration object:
    ```
    var configuration = new AmazonWebServicesCloudwatchConfiguration(serviceProvider)
    {
        AccessKey = "your AccessKey",
        SecretKey = "your SecretKey",
        AwsRegion = "your AwsRegion",
        ServiceUrl = "your ServiceUrl"
    };
    ```
4. Initialize the integration:
    ```
    var integration = serviceProvider.GetRequiredService<IAmazonWebServicesCloudwatchIntegration>();
    await integration.Initialize(configuration);
    ```
5. Call the integration Api's: `await integration.GetAlarmsState(new GetAlarmsStateRequest{...});`

## Public objects

* Configuration Object: `AmazonWebServicesCloudwatchConfiguration`
* Integration Service: `IAmazonWebServicesCloudwatchIntegration`

## Api's

* `async Task<GetAlarmsStateResponse> GetAlarmsState(GetAlarmsStateRequest request)`
