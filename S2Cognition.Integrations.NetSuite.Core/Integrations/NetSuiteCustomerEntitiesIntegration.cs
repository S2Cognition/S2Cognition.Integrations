using Oracle.NetSuite;
using S2Cognition.Integrations.NetSuite.Core.Data;

namespace S2Cognition.Integrations.NetSuite.Core.Integrations;

public interface INetSuiteCustomerEntitiesIntegration
{
    Task<FindCustomerCategoriesResponse> FindCustomerCategories(FindCustomerCategoriesRequest request);
    Task<FindCustomerStatusesResponse> FindCustomerStatuses(FindCustomerStatusesRequest request);
}

internal class NetSuiteCustomerEntitiesIntegration : NetSuiteSubIntegrationBase, INetSuiteCustomerEntitiesIntegration
{
    public NetSuiteCustomerEntitiesIntegration(IServiceProvider serviceProvider, NetSuiteIntegration parent)
        : base(serviceProvider, parent)
    {
    }

    public async Task<FindCustomerCategoriesResponse> FindCustomerCategories(FindCustomerCategoriesRequest request)
    {
        var client = await BuildClient();

        var options = new CustomerCategorySearch
        {
            basic = new CustomerCategorySearchBasic
            {
            }
        };

        var response = await client.Search(options);
        await CheckResponseForErrors(response);

        var pickList = response.searchResult.recordList
            .OfType<CustomerCategory>()
            .Select(record => new PickListRecord
            {
                Name = record.name,
                InternalId = record.internalId,
                ExternalId = record.externalId
            });

        return new FindCustomerCategoriesResponse
        {
            Results = pickList.ToList()
        };
    }

    public async Task<FindCustomerStatusesResponse> FindCustomerStatuses(FindCustomerStatusesRequest request)
    {
        var client = await BuildClient();

        var options = new CustomerStatusSearch
        {
            basic = new CustomerStatusSearchBasic
            {
            }
        };

        var response = await client.Search(options);
        await CheckResponseForErrors(response);

        var pickList = response.searchResult.recordList
            .OfType<CustomerStatus>()
            .Select(record => new PickListRecord
            {
                Name = record.name,
                InternalId = record.internalId,
                ExternalId = record.externalId
            });

        return new FindCustomerStatusesResponse
        {
            Results = pickList.ToList()
        };
    }
}
