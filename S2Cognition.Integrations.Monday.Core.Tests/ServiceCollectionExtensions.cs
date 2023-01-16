using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.Monday.Core.Tests.Fakes;

namespace S2Cognition.Integrations.Monday.Core.Tests;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFakeMondayIntegration(this IServiceCollection sc)
    {
        return sc.AddSingleton<IMondayIntegration, FakeMondayIntegration>();
    }
}
