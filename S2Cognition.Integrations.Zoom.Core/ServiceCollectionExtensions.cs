using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.Core;

namespace S2Cognition.Integrations.Zoom.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddZoomIntegration(this IServiceCollection sc)
    {
        return sc.AddIntegrationUtilities()
            .AddScoped<IZoomIntegration>(_ => new ZoomIntegration(_));
    }
}
