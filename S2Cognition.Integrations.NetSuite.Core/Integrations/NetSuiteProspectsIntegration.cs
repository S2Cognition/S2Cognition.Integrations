using Oracle.NetSuite;
using S2Cognition.Integrations.NetSuite.Core.Data;

namespace S2Cognition.Integrations.NetSuite.Core.Integrations;

public interface INetSuiteProspectsIntegration
{
    Task<FindProspectsResponse> FindProspects(FindProspectsRequest request);
    Task<CreateProspectResponse> CreateProspect(CreateProspectRequest request);
}

internal class NetSuiteProspectsIntegration : NetSuiteSubIntegrationBase, INetSuiteProspectsIntegration
{
    public NetSuiteProspectsIntegration(IServiceProvider serviceProvider, NetSuiteIntegration parent)
        : base(serviceProvider, parent)
    {
    }

    public async Task<FindProspectsResponse> FindProspects(FindProspectsRequest request)
    {
        var client = await BuildClient();

        var options = new CustomerSearch
        {
            basic = new CustomerSearchBasic
            {
                stage = new SearchEnumMultiSelectField
                {
                    @operator = SearchEnumMultiSelectFieldOperator.anyOf,
                    operatorSpecified = true,
                    searchValue = new[] { "PROSPECT" }
                }
            }
        };

        var response = await client.Search(options);
        await CheckResponseForErrors(response);

        var prospects = response.searchResult.recordList
            .OfType<Customer>()
            .Select(record => new ProspectRecord
            {
                CompanyName = record.companyName
            });

        return new FindProspectsResponse
        {
            Prospects = prospects.ToList()
        };
    }

    public async Task<CreateProspectResponse> CreateProspect(CreateProspectRequest request)
    {
        var client = await BuildClient();

        var options = new Customer
        {
            externalId = request.ExternalId,
            comments = request.Comments,
            email = request.Email,
            firstName = request.FirstName,
            lastName = request.LastName,
            companyName = request.CompanyName,
            phone = request.Phone,

            isPerson = true,
            // entityStatus = LookupCustomerStatus(Data.CustomerStatus.ProspectInDiscussion),
            // leadSource = LookupLeadSource(req.Source)
        };

        var response = await client.Add(options);
        await CheckResponseForErrors(response);

        return new CreateProspectResponse
        {
        };
    }
}
