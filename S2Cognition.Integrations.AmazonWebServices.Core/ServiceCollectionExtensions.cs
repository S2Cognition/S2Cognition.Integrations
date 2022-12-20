using Microsoft.Extensions.DependencyInjection;

namespace S2Cognition.Integrations.AmazonWebServices.Core.Data;

public static class ServiceCollectionExtensions
{
    public static void AddAmazonWebServicesIntegration(this IServiceCollection sc)
    {
        sc.AddScoped<IAmazonWebServicesIntegration, AmazonWebServicesIntegration>();

        sc.AddScoped<IAwsRegionFactory, AwsRegionFactory>();
        sc.AddScoped<IAwsRegionEndpoint, AwsRegionEndpoint>();
    }
}
