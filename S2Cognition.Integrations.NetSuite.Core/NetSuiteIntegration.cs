using Microsoft.Extensions.DependencyInjection;
using Oracle.NetSuite;
using S2Cognition.Integrations.Core;
using S2Cognition.Integrations.NetSuite.Core.Data;
using S2Cognition.Integrations.NetSuite.Core.Data.Requests;
using S2Cognition.Integrations.NetSuite.Core.Data.Responses;
using S2Cognition.Integrations.NetSuite.Core.Models;

using SystemTask = System.Threading.Tasks.Task;

namespace S2Cognition.Integrations.NetSuite.Core;

public interface INetSuiteIntegration : IIntegration<NetSuiteConfiguration>
{
    Task<ListProspectsResponse> ListProspects(ListProspectsRequest? req = null);
}

public class NetSuiteIntegration : Integration<NetSuiteConfiguration>, INetSuiteIntegration
{
    private INetSuiteService? _service = null;

    public NetSuiteIntegration(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    protected async Task<INetSuiteService> BuildClient()
    {
        if (_service == null)
        {
            var factory = _ioc.GetRequiredService<INetSuiteServiceFactory>();
            _service = await factory.Create(Configuration);
        }

        return await SystemTask.FromResult(_service);
    }

    public async Task<ListProspectsResponse> ListProspects(ListProspectsRequest? req = null)
    {
        var client = await BuildClient();
        var request = new RecordRef
        {
            type = RecordType.customer,
            typeSpecified = true,
            internalId = String.Empty,
            externalId = String.Empty
        };

        var response = await client.List(request);

        if (response == null)
            throw new InvalidOperationException();

        if (!response.readResponseList.status.isSuccessSpecified
            || !response.readResponseList.status.isSuccess)
        {
            throw new InvalidOperationException();
        }

        var prospects = new List<Prospect>();
        foreach (var resp in response.readResponseList.readResponse)
        {
            if (!resp.status.isSuccessSpecified
                || !resp.status.isSuccess)
            {
                prospects.Add(new Prospect());
            }
            else
            {
                if (resp.record.nullFieldList.Length == 123)
                    throw new InvalidOperationException();

                var prospect = new Prospect {
                };
                prospects.Add(prospect);
            }
        }

        return await SystemTask.FromResult(new ListProspectsResponse {
            Prospects = prospects
        });
    }
}

