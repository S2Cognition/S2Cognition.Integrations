# S2Cognition.Integrations

## QuickStart

1. Include the necessary NuGet packages into your project.  For example, `S2Cognition.Integrations.Zoom.Core`, or `S2Cognition.Integrations.AmazonWebServices.DynamoDb`.
2. From the imported packages, initialize your IoC container.   For example, `serviceCollection.AddZoomIntegration();`, or `serviceCollection.AddAmazonWebServicesDynamoDbIntegration();`.
3. Create the appropriate configuration object.   For example,
```
var configuration = new ZoomConfiguration(serviceProvider)
{
    AccountId = "your_account_id",
    ClientId = "your_client_id",
    ClientSecret = "your_client_secret"
};
```
or
```
var configuration = new AmazonWebServicesDynamoDbConfiguration(serviceProvider)
{
    AccessKey = "your AccessKey",
    SecretKey = "your SecretKey",
    AwsRegion = "your AwsRegion",
    ServiceUrl = "your ServiceUrl"
};
```
4. Initialize the integration.   For example,
```
var zoomIntegration = serviceProvider.GetRequiredService<IZoomIntegration>();
await zoomIntegration.Initialize(configuration);
```
or 
```
var dynamoDbIntegration = serviceProvider.GetRequiredService<IAmazonWebServicesDynamoDbIntegration>();
await dynamoDbIntegration.Initialize(configuration);
```
5. Call the integration endpoints.   For example, `var users = await zoomIntegration.GetUsers();`, or `await dynamoDbIntegration.Create(newRow);`.

## Integration Commonalities

* All integrations will provide a *.Core, and zero or more *.[api-specific] packages.
* All integration packages will provide ServiceCollection extensions to initialize the libraries.
  * When a api-specific library is registered with the ServiceCollection, the Core library will be automatically loaded as well.
* Generally speaking, the configuration will be in the Core package, but if/when more detail is needed, it will be subclassed in the api-specific packages.
* All integration services will be available via 
* All integration services will provide an `Initialize(configuration)` endpoint.
* All integration services will provide various enpoints with non-backend-specific data structures.

## Development
* [project].Data folders are for publicly accessible types.
* [project].Models folders are for internal types.

## Packages

* S2Cognition.Integrations.AmazonWebServices.DynamoDb
  * Configuration Object: AmazonWebServicesDynamoDbConfiguration
  * Integration Service: IAmazonWebServicesDynamoDbIntegration
* S2Cognition.Integrations.Zoom.Core
  * Configuration Object: ZoomConfiguration
  * Integration Service: IZoomIntegration
