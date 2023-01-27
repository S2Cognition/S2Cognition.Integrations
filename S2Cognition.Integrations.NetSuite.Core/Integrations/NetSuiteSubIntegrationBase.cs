using Microsoft.Extensions.DependencyInjection;
using Oracle.NetSuite;
using S2Cognition.Integrations.NetSuite.Core.Data;
using S2Cognition.Integrations.NetSuite.Core.Models;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using SystemTask = System.Threading.Tasks.Task;

namespace S2Cognition.Integrations.NetSuite.Core.Integrations;

internal class NetSuiteSubIntegrationBase
{
    protected INetSuiteService? _service = null;
    protected readonly IServiceProvider _ioc;
    protected readonly NetSuiteIntegration _parent;

    protected internal NetSuiteSubIntegrationBase(IServiceProvider serviceProvider, NetSuiteIntegration parent)
    {
        _ioc = serviceProvider;
        _parent = parent;
    }

    protected async Task<INetSuiteService> BuildClient()
    {
        if (_service == null)
        {
            var factory = _ioc.GetRequiredService<INetSuiteServiceFactory>();
            _service = await factory.Create(_parent.Configuration);
        }

        return await SystemTask.FromResult(_service);
    }

    protected static async Task<string> BuildErrorResponse(Status? status)
    {
        if (status == null)
            return "Unexpected null response from NetSuite";

        var errorMessage = new StringBuilder();
        errorMessage.AppendLine("NetSuite returned error(s):");

        foreach (var message in status.statusDetail.Select(_ => _.message))
            errorMessage.AppendLine($"* {message}");

        return await SystemTask.FromResult(errorMessage.ToString());
    }

    protected static async SystemTask CheckResponseForErrors([NotNull] addResponse? response)
    {
        if (response == null
            || !response.writeResponse.status.isSuccessSpecified
            || !response.writeResponse.status.isSuccess)
        {
            throw new InvalidOperationException(await BuildErrorResponse(response?.writeResponse?.status));
        }
    }

    protected static async SystemTask CheckResponseForErrors([NotNull] searchResponse? response)
    {
        if (response == null
            || !response.searchResult.status.isSuccessSpecified
            || !response.searchResult.status.isSuccess)
        {
            throw new InvalidOperationException(await BuildErrorResponse(response?.searchResult?.status));
        }
    }
}
