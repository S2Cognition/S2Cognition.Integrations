#  S2Cognition.Integrations.AmazonWebServices.NetSuite

Subproject of [S2Cognition.Integrations](../readme.md)

## QuickStart Example

1. Include the necessary NuGet packages into your project: `S2Cognition.Integrations.AmazonWebServices.NetSuite`
2. Initialize your IoC container: `serviceCollection.AddAmazonWebServicesNetSuiteIntegration();`
3. Create the appropriate configuration object:
    ```
    var configuration = new AmazonWebServicesNetSuiteConfiguration(serviceProvider)
    {
        AccountId = "dummyValue",
        ConsumerKey = "dummyValue",
        ConsumerSecret = "dummyValue",
        TokenId = "dummyValue",
        TokenSecret = "dummyValue"
    };
    ```
4. Initialize the integration:
    ```
    var integration = serviceProvider.GetRequiredService<IAmazonWebServicesNetSuiteIntegration>();
    await integration.Initialize(configuration);
    ```
5. Call the integration Api's: `await integration.Leads.FindLeads(req);`

## Public objects

* Configuration Object: `AmazonWebServicesNetSuiteConfiguration`
* Integration Service: `IAmazonWebServicesNetSuiteIntegration`

## Api's

* CustomerEntities
  * `async Task<FindCustomerCategoriesResponse> FindCustomerCategories(FindCustomerCategoriesRequest request)`
  * `async Task<FindCustomerStatusesResponse> FindCustomerStatuses(FindCustomerStatusesRequest request)`
* Leads
  * `async Task<FindLeadsResponse> FindLeads(FindLeadsRequest request)`
  * `async Task<CreateLeadResponse> CreateLead(CreateLeadRequest request)`
* Prospects
  * `async Task<FindProspectsResponse> FindProspects(FindProspectsRequest request)`
  * `async Task<CreateProspectResponse> CreateProspect(CreateProspectRequest request)`
* Customers
  * `async Task<FindCustomersResponse> FindCustomers(FindCustomersRequest request)`
  * `async Task<GetCustomerResponse> GetCustomer(GetCustomerRequest request)`
  * `async Task<UpdateCustomerResponse> UpdateCustomer(UpdateCustomerRequest request)`
* Miscellaneous
  * `async Task<GetCustomRecordResponse> GetCustomRecord(GetCustomRecordRequest request)`
  * `async Task<CreateNoteResponse> CreateNote(CreateNoteRequest request)`
