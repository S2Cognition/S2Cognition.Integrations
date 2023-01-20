using S2Cognition.Integrations.NetSuite.Core.Data;

namespace S2Cognition.Integrations.NetSuite.Core.Models;

public interface INetSuiteServiceFactory
{
    Task<INetSuiteService> Create(NetSuiteConfiguration configuration);
}

public class NetSuiteServiceFactory : INetSuiteServiceFactory
{
    public NetSuiteServiceFactory()
    {
    }

    public async Task<INetSuiteService> Create(NetSuiteConfiguration configuration)
    {
        return await NetSuiteService.Create(configuration);
    }
}
