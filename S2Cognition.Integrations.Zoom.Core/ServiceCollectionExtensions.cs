using Microsoft.Extensions.DependencyInjection;

namespace S2Cognition.Integrations.Zoom.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddZoomIntegration(this IServiceCollection sc)
    {
        sc.AddScoped<IZoomIntegration, ZoomIntegration>();

        return sc;
    }
}
