using Oracle.NetSuite;
using S2Cognition.Integrations.NetSuite.Core.Data;

using SystemTask = System.Threading.Tasks.Task;

namespace S2Cognition.Integrations.NetSuite.Core.Integrations;

public interface INetSuiteCustomersIntegration
{
    Task<FindCustomersResponse> FindCustomers(FindCustomersRequest request);
    Task<GetCustomerResponse> GetCustomer(GetCustomerRequest request);
    Task<UpdateCustomerResponse> UpdateCustomer(UpdateCustomerRequest request);
}

internal class NetSuiteCustomersIntegration : NetSuiteSubIntegrationBase, INetSuiteCustomersIntegration
{
    public NetSuiteCustomersIntegration(IServiceProvider serviceProvider, NetSuiteIntegration parent)
        : base(serviceProvider, parent)
    {
    }

    public async Task<FindCustomersResponse> FindCustomers(FindCustomersRequest request)
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
                    searchValue = new[] { "CUSTOMER" }
                }
            }
        };

        if (request.Category != null)
        {
            options.basic.category = new SearchMultiSelectField
            {
                @operator = SearchMultiSelectFieldOperator.anyOf,
                operatorSpecified = true,
                searchValue = new[] { new RecordRef { internalId = request.Category.InternalId } }
            };
        }

        if (request.Status != null)
        {
            options.basic.custStatus = new SearchMultiSelectField
            {
                @operator = SearchMultiSelectFieldOperator.anyOf,
                operatorSpecified = true,
                searchValue = new[] { new RecordRef { internalId = request.Status.InternalId } }
            };
        }

        if (request.CustomFields != null)
        {
            if (request.CustomFields.DataType == typeof(string))
            {
                options.basic.customFieldList = new SearchCustomField[]
                {
                        new SearchStringCustomField
                        {
                            @operator = SearchStringFieldOperator.notEmpty,
                            operatorSpecified = true,
                            scriptId = request.CustomFields.Key
                        }
                };
            }
            else if (request.CustomFields.DataType == typeof(int))
            {
                options.basic.customFieldList = new SearchCustomField[]
                {
                        new SearchLongCustomField
                        {
                            @operator = SearchLongFieldOperator.notEmpty,
                            operatorSpecified = true,
                            scriptId = request.CustomFields.Key
                        }
                };
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        var response = await client.Search(options);
        await CheckResponseForErrors(response);

        if (response == null)
            throw new InvalidOperationException();

        var recordCount = response.searchResult.totalRecordsSpecified ? (int?)response.searchResult.totalRecords : null;
        var customers = response.searchResult.recordList
            .OfType<Customer>()
            .Select(record => new CustomerRecord
            {
                CompanyName = record.companyName
            });

        return new FindCustomersResponse
        {
            TotalRecords = recordCount,
            Customers = customers.ToList()
        };
    }

    public async Task<UpdateCustomerResponse> UpdateCustomer(UpdateCustomerRequest reqest)
    {
        /*
                public async Task<UpdateCustomerResponse> UpdateCustomer(UpdateCustomerRequest req)
                {
                    var client = await BuildClient();

                    var response = await client.Add(new Customer
                    {
                        entityStatus = LookupCustomerStatus(req.CustomerStatus)
                    });

                    var result = ProcessResponse(new UpdateCustomerResponse(), response);

                    if (req.CustomerAction != null)
                    {
                        if (req.CustomerAction == CustomerAction.EvaluationComplete)
                        {
                            ProcessResponse(result, await CreateNote(new CreateNoteRequest
                            {
                                ExternalId = req.ExternalId,
                                Title = "Evaluation Complete",
                                Note = $"Evaluation Completed: {DateTime.UtcNow}"
                            }));
                        }
                        else if (req.CustomerAction == CustomerAction.ReportPurchase)
                        {
                            ProcessResponse(result, await CreateNote(new CreateNoteRequest
                            {
                                ExternalId = req.ExternalId,
                                Title = "Report Purchase",
                                Note = $"Report Purchased: {DateTime.UtcNow}"
                            }));
                        }
                    }

                    return result;
                }
         */
        await SystemTask.CompletedTask;
        throw new InvalidOperationException();
    }

    public async Task<GetCustomerResponse> GetCustomer(GetCustomerRequest reqest)
    {
        /*
                public async Task<GetCustomerResponse> GetCustomer(GetCustomerRequest req)
                {
                    var client = await BuildClient();

                    var request = new RecordRef { externalId = req.ExternalId, type = RecordType.customer, typeSpecified = true };

                    var response = await client.Get(request);

                    return ProcessResponse(new GetCustomerResponse(), response);
                }
         */
        await SystemTask.CompletedTask;
        throw new InvalidOperationException();
    }
}
