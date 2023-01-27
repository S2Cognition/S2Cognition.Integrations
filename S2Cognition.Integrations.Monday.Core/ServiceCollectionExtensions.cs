using Microsoft.Extensions.DependencyInjection;

namespace S2Cognition.Integrations.Monday.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMondayIntegration(this IServiceCollection sc)
    {
        return sc.AddScoped<IMondayIntegration>(_ => new MondayIntegration(_));
    }
}
