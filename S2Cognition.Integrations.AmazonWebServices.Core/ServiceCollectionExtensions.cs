using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.Core.Data;

namespace S2Cognition.Integrations.AmazonWebServices.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAmazonWebServicesIntegration(this IServiceCollection sc)
    {
        sc.AddSingleton<IAmazonWebServicesIntegration, AmazonWebServicesIntegration>()
            .AddSingleton<IAwsRegionFactory, AwsRegionFactory>()
            .AddScoped<IAwsRegionEndpoint, AwsRegionEndpoint>();

        return sc;
    }
}
