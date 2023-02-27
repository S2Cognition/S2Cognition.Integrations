# S2Cognition.Integrations

## Overview

This set of packages provides simple and consistent integrations with several common 3rd party packages.

## Usage

1. Include NuGet references to the desired packages
2. Use ServiceCollection extensions to add capabilites 
3. Create configuration object
4. Initialize integration
5. Call integration methods

## Packages

* [Aws Cloudwatch](S2Cognition.Integrations.AmazonWebServices.Cloudwatch\Readme.md)
* [Aws DynamoDb](S2Cognition.Integrations.AmazonWebServices.DynamoDb\Readme.md)
* [Aws S3](S2Cognition.Integrations.AmazonWebServices.S3\Readme.md)
* [Aws Ses](S2Cognition.Integrations.AmazonWebServices.Ses\Readme.md)
* [NetSuite](S2Cognition.Integrations.NetSuite.Core\Readme.md)
* [Zoom](S2Cognition.Integrations.Zoom.Core\Readme.md)
* [Monday.com](S2Cognition.Integrations.Monday.Core\Readme.md)

### Coming Soon:
* StreamDeck
* Google Analytics
* Microsoft Azure DevOps
* Zoom Phone
* Popl
* Auth.Net
* MailChimp

## Integration Commonalities

* All integrations will provide a *.Core, and zero or more *.[api-specific] packages.
* All integration packages will provide ServiceCollection extensions to initialize the libraries.
  * When a api-specific library is registered with the ServiceCollection, the Core library will be automatically loaded as well.
* Generally speaking, the configuration will be in the Core package, but if/when more detail is needed, it will be subclassed in the api-specific packages.
* All integration services will be configurable via a package-specific configuration object.
* All integration services will provide an `Initialize(configuration)` endpoint.
* All integration services will provide various enpoints with non-backend-specific data structures.
* No Api's will expose the underlying implementation types.

## Development Overview
* [project].Data folders are for publicly accessible types.
  * These objects should be public
* [project].Models folders are for internal types.
  * These objects should be internal
* The public interface to the library will be `I[PackageType]Integration : IIntegration<[PackageType]Configuration>`.  For example: `IZoomIntegration : IIntegration<ZoomConfiguration>`, where ZoomConfiguration is the public configuration object defined in `S2Cognition.Integrations.Zoom.Core\Data`.
  * The implementation (and it's constructor) for the public interface will be internal.
* All projects will have a corresponding test assembly
  * The public interface implementation should be fully tested.  All other meaningful objects should also be tested.

### Project Layout Example

Solution: S2Cognition.Integrations
* Folder: Zoom
  * Project: S2Cognition.Integrations.Zoom.Core
    * Folder: Data
      * All "Request" objects
      * All "Response" objects
      * All "Record" objects
      * ZoomConfiguration.cs
    * Folder: Models
      * All non-public objects used in the low-level service calls
      * This will include Client wrappers (which will likely be Faked as well), Factory implementations, etc.
    * LICENSE
      * MIT license
    * README.md
      * Should fully document the ZoomConfiguration object and IZoomIntegration interface
      * Should fully document all Request and Response objects
    * Usings.cs
      * Should expose internals to the unit test project
    * ServiceCollectionExtensions.cs
    * ZoomIntegration.cs
  * Project: S2Cognition.Integrations.Zoom.Core.Tests
    * Folder: Fakes
      * Should provide fakes for all necessary objects used in testing
    * Usings.cs
    * ServiceCollectionExtensions.cs
      * Used to provide fakes, factories, etc
    * All test files

### Standards

#### General
* Do not expose implementation details.  For example, instead of `public async Task<ZoomUserCollection> GetUsers()` where ZoomUserCollection is tightly coupled with the Zoom Sdk, prefer `public async Task<GetUsersResponse> GetUsers(GetUsersRequest)` where `GetUsersResponse` and `GetUsersRequest` are defined in the Data folder.
* Document the interface using /// comments.

#### API Arguments
* Always provide a distinct Request and Response object for each Api.   For example, even though the current implementation doesn't have any query options, provide a GetUsersRequest object and require it on the GetUsers call.
* All Request objects should have meaningful defaults for properties whenever possible.  For example, the GetUsersRequest supports paging, and the PageSize property has a default of 25.
  * Note: Common defaults are defined in S2Cognition.Integrations.Core.Data.Configuration; in this example: `S2Cognition.Integrations.Core.Data.Configuration.DefaultPageSize`.

#### Async

* All methods should be Async when possible.  For example, instead of `public GetUsersResponse GetUsers(GetUsersRequest)`, prefer `public async Task<GetUsersResponse> GetUsers(GetUsersRequest)`.
* Do not suffix Api's with `Async`.  For example, instead of `public async Task<GetUsersResponse> GetUsersAsync(GetUsersRequest)`, prefer `public async Task<GetUsersResponse> GetUsers(GetUsersRequest)`.

#### Types
* Collections should be IList<> when order is meaningful, and ICollection<> when order is not meaningful.

#### Exceptions
* When ensuring proper arguments for an API call, throw an ArgumentException with the name of the property which is invalid.
