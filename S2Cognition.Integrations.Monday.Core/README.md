#  S2Cognition.Integrations.AmazonWebServices.Monday

Subproject of [S2Cognition.Integrations](../readme.md)

## QuickStart Example

1. Include the necessary NuGet packages into your project: `S2Cognition.Integrations.AmazonWebServices.Monday`
2. Initialize your IoC container: `serviceCollection.AddAmazonWebServicesMondayIntegration();`
3. Create the appropriate configuration object:
    ```
    var configuration = new AmazonWebServicesMondayConfiguration(serviceProvider)
    {
        ApiKey = "your ApiKey"
    };
    ```
4. Initialize the integration:
    ```
    var integration = serviceProvider.GetRequiredService<IAmazonWebServicesMondayIntegration>();
    await integration.Initialize(configuration);
    ```
5. Call the integration Api's: `await integration.GetUsers(req);`

## Public objects

* Configuration Object: `AmazonWebServicesMondayConfiguration`
* Integration Service: `IAmazonWebServicesMondayIntegration`

## Api's

* `async Task<GetUsersResponse> GetUsers(GetUsersRequest request)`
* `async Task<GetItemsResponse> GetItems(GetItemsRequest request)`
* `async Task<CreateItemResponse> CreateItem(CreateItemRequest request)`
* `async Task<CreateSubItemResponse> CreateSubItem(CreateSubItemRequest request)`
