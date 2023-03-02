#  S2Cognition.Integrations.AmazonWebServices.Ssm

Subproject of [S2Cognition.Integrations](../readme.md)

## QuickStart Example

1. Include the necessary NuGet packages into your project: `S2Cognition.Integrations.AmazonWebServices.Ssm`
2. Initialize your IoC container: `serviceCollection.AddAmazonWebServicesSsmIntegration();`
3. Create the appropriate configuration object:
    ```
    var configuration = new AmazonWebServicesSsmConfiguration(serviceProvider)
    {
        AccessKey = "your AccessKey",
        SecretKey = "your SecretKey",
        AwsRegion = "your AwsRegion",
        ServiceUrl = "your ServiceUrl",
        Environment = EnvironmentType.Production or EnvironmentType.Staging or EnvironmentType.Development 
    };
    ```
4. Initialize the integration:
    ```
    var integration = serviceProvider.GetRequiredService<IAmazonWebServicesSsmIntegration>();
    await integration.Initialize(configuration);
    ```
5. Call the integration Api's: `await integration.GetSsmParameter(new GetSsmParameterRequest{...});`

## Public objects

* Configuration Object: `AmazonWebServicesSsmConfiguration`
* Integration Service: `IAmazonWebServicesSsmIntegration`

## Api's

* `async Task<GetSsmParameterResponse> GetSsmParameter(GetSsmParameterRequest req)`
* `async Task<PutSsmParameterResponse> PutSsmParameter(PutSsmParameterRequest req)`
