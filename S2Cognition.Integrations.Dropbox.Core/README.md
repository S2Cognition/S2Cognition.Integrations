#  S2Cognition.Integrations.Dropbox.Core

Subproject of [S2Cognition.Integrations](../readme.md)

## QuickStart Example

1. Include the necessary NuGet packages into your project: `S2Cognition.Integrations.Dropbox.Core`
2. Initialize your IoC container: `serviceCollection.AddDropboxIntegration();`
3. Create the appropriate configuration object:
    ```
    var configuration = new DropboxConfiguration(serviceProvider)
    {
        LoginEmailAddress = "your.email@example.com",
        AccessToken = "your dropbox access token"
    };
    ```
4. Initialize the integration:
    ```
    var integration = serviceProvider.GetRequiredService<IDropboxIntegration>();
    await integration.Initialize(configuration);
    ```
5. Call the integration Api's: `await GetTeamMembers(req);`

## Public objects

* Configuration Object: `DropboxConfiguration`
* Integration Service: `IDropboxIntegration`

## Api's

* `Task<GetTeamMembersResponse> GetTeamMembers(GetTeamMembersRequest req)`
  * Request Object: `GetTeamMembersRequest`
  * Response Object: `GetTeamMembersResponse`
* `Task<GetFolderResponse> GetFolder(GetFolderRequest req)`
  * Request Object: `GetFolderRequest`
  * Response Object: `GetFolderResponse`
* `Task<GetFilesResponse> GetFiles(GetFilesRequest req)`
  * Request Object: `GetFilesRequest`
  * Response Object: `GetFilesResponse`
* `Task<GetFileResponse> GetFile(GetFileRequest req)`
  * Request Object: `GetFileRequest`
  * Response Object: `GetFileResponse`
* `Task<GetSharedLinksResponse> GetSharedLinks(GetSharedLinksRequest req)`
  * Request Object: `GetSharedLinksRequest`
  * Response Object: `GetSharedLinksResponse`

