using Microsoft.Extensions.DependencyInjection;

namespace S2Cognition.Integrations.AmazonWebServices.Core.Data;

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
