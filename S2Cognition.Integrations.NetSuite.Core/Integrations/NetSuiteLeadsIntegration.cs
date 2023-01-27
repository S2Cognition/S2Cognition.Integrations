using Oracle.NetSuite;
using S2Cognition.Integrations.NetSuite.Core.Data;

using SystemTask = System.Threading.Tasks.Task;

namespace S2Cognition.Integrations.NetSuite.Core.Integrations;

public interface INetSuiteLeadsIntegration
{
    Task<FindLeadsResponse> FindLeads(FindLeadsRequest request);
    Task<CreateLeadResponse> CreateLead(CreateLeadRequest request);
}

internal class NetSuiteLeadsIntegration : NetSuiteSubIntegrationBase, INetSuiteLeadsIntegration
{
    public NetSuiteLeadsIntegration(IServiceProvider serviceProvider, NetSuiteIntegration parent)
        : base(serviceProvider, parent)
    {
    }

    public async Task<FindLeadsResponse> FindLeads(FindLeadsRequest request)
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
                    searchValue = new[] { "LEAD" }
                }
            }
        };

        var response = await client.Search(options);
        await CheckResponseForErrors(response);

        var leads = response.searchResult.recordList
            .OfType<Customer>()
            .Select(record => new LeadRecord
            {
                CompanyName = record.companyName
            });

        return new FindLeadsResponse
        {
            Leads = leads.ToList()
        };
    }

    public async Task<CreateLeadResponse> CreateLead(CreateLeadRequest reqest)
    {
        /*
                public async Task<CreateLeadResponse> CreateLead(CreateLeadRequest req)
                {
                    _config?.Logger?.LogInformation("Building Client");

                    var client = await BuildClient();

                    var request = new Customer
                    {
                        comments = req.Comments,
                        email = req.Email,
                        firstName = req.FirstName,
                        lastName = req.LastName,
                        companyName = req.CompanyName,
                        phone = req.Phone,
                        entityStatus = LookupCustomerStatus(Data.CustomerStatus.ProspectInDiscussion),
                        isPerson = true,
                        isPersonSpecified = true,
                        externalId = req.ExternalId,
                        leadSource = LookupLeadSource(req.Source)
                    };

                    _config?.Logger?.LogInformation("Adding Lead (as prospect)");
                    var addResponse = await client.Add(request);
                    if (addResponse == null)
                    {
                        _config?.Logger?.LogError("Unable to add Lead (as prospect)");
                        throw new InvalidOperationException("Unable to add Lead (as prospect) -- client response was null.");
                    }

                    _config?.Logger?.LogInformation("Lead Added (as prospect)");

                    var response = ProcessResponse(new CreateLeadResponse(), addResponse);

                    if (addResponse.writeResponse.baseRef is RecordRef recordRef)
                    {
                        request.internalId = recordRef.internalId;
                    }

                    request.entityStatus = LookupCustomerStatus(Data.CustomerStatus.LeadInboundInquiry);

                    _config?.Logger?.LogInformation("Converting Prospect to Lead");
                    var updateResponse = await client.Update(request);
                    _config?.Logger?.LogInformation("Prospect converted to Lead");

                    return ProcessResponse(response, updateResponse);
                }
         */
        await SystemTask.CompletedTask;
        throw new InvalidOperationException();
    }
}
