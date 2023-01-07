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
            _service = await factory.Create();
        }

        return await SystemTask.FromResult(_service);
    }

    public async Task<ListProspectsResponse> ListProspects(ListProspectsRequest? req = null)
    {
        var client = await BuildClient();

        var request = new RecordRef { type = RecordType.customer, typeSpecified = true };

        var response = await client.List(request);

        return await Task.FromResult(new ListProspectsResponse {
            // map response
        });
    }
}
