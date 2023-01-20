using S2Cognition.Integrations.NetSuite.Core.Data;

namespace S2Cognition.Integrations.NetSuite.Core.Models;

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
