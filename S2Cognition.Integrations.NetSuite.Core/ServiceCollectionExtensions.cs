using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.Core;
using S2Cognition.Integrations.NetSuite.Core.Models;

namespace S2Cognition.Integrations.NetSuite.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNetSuiteIntegration(this IServiceCollection sc)
    {
        return sc.AddIntegrationUtilities()
            .AddScoped<INetSuiteIntegration, NetSuiteIntegration>()
            .AddSingleton<INetSuiteServiceFactory, NetSuiteServiceFactory>()
        ;
    }
}
