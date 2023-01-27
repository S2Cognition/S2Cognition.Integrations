#  S2Cognition.Integrations.Zoom.Core

Subproject of [S2Cognition.Integrations](../readme.md)

## QuickStart Example

1. Include the necessary NuGet packages into your project: `S2Cognition.Integrations.Zoom.Core`
2. Initialize your IoC container: `serviceCollection.AddZoomIntegration();`
3. Create the appropriate configuration object:
    ```
    var configuration = new ZoomConfiguration(serviceProvider)
    {
        AccountId = "your_account_id",
        ClientId = "your_client_id",
        ClientSecret = "your_client_secret"
    };
    ```
4. Initialize the integration:
    ```
    var zoomIntegration = serviceProvider.GetRequiredService<IZoomIntegration>();
    await zoomIntegration.Initialize(configuration);
    ```
5. Call the integration Api's: `var users = await zoomIntegration.GetUsers();`

## Public objects

* Configuration Object: `ZoomConfiguration`
* Integration Service: `IZoomIntegration`

## Api's

### Common Objects

* `PagedRequest`
  * PageSize (int) - defines the number of entries to be returned.
  * NextPageToken (string?) - if null, returns the first page of data; otherwise, returns the next page of data.  The value of NextPageToken must be set to the value of NextPageToken returned by the previous request's PagedResponse.
* PagedResponse
    PageSize (int) - the size of the page of data.
    TotalRecords (int) - the total number of records available from the request.
    NextPageToken (string?) - null when there are no more pages of data available; otherwise, when there are more pages of data available, NextPageToken is set to a key which can be provided in the next request to retrieve the next page of data.

### `Task<GetUsersResponse> GetUsers(GetUsersRequest)`

Returns Users in Zoom.

* GetUsersRequest : PagedRequest
* GetUsersResponse : PagedResponse
  * Users (ICollection<UserRecord>)- the page of UserRecords from the request
* UserRecord
  * Id (string)
  * FirstName (string?)
  * LastName (string?)
  * Email (string?)
  * Department (string?)
