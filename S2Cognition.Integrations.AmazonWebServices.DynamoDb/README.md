#  S2Cognition.Integrations.AmazonWebServices.DynamoDb

Subproject of [S2Cognition.Integrations](../readme.md)

## QuickStart Example

1. Include the necessary NuGet packages into your project: `S2Cognition.Integrations.AmazonWebServices.DynamoDb`
2. Initialize your IoC container: `serviceCollection.AddAmazonWebServicesDynamoDbIntegration();`
3. Create the appropriate configuration object:
    ```
    var configuration = new AmazonWebServicesDynamoDbConfiguration(serviceProvider)
    {
        AccessKey = "your AccessKey",
        SecretKey = "your SecretKey",
        AwsRegion = "your AwsRegion",
        ServiceUrl = "your ServiceUrl"
    };
    ```
4. Initialize the integration:
    ```
    var integration = serviceProvider.GetRequiredService<IAmazonWebServicesDynamoDbIntegration>();
    await integration.Initialize(configuration);
    ```
5. Call the integration Api's: `await integration.Create(newRow);`

## Public objects

* Configuration Object: `AmazonWebServicesDynamoDbConfiguration`
* Integration Service: `IAmazonWebServicesDynamoDbIntegration`

## Api's

* `async Task Create<T>(T data)`
* `async Task<T?> Read<T>(T data)`
