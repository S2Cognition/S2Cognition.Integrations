using S2Cognition.Integrations.NetSuite.Core.Data;

namespace S2Cognition.Integrations.NetSuite.Core.Models;

internal interface INetSuiteServiceFactory
{
    Task<INetSuiteService> Create(NetSuiteConfiguration configuration);
}

internal class NetSuiteServiceFactory : INetSuiteServiceFactory
{
    internal NetSuiteServiceFactory()
    {
    }

    public async Task<INetSuiteService> Create(NetSuiteConfiguration configuration)
    {
        return await NetSuiteService.Create(configuration);
    }
}
