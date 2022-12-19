using Microsoft.Extensions.DependencyInjection;

namespace S2Cognition.Integrations.AmazonWebServices.Core;

public static class ServiceCollectionExtensions
{
    public static void AddAmazonWebServicesIntegration(this IServiceCollection sc)
    {
        sc.AddScoped<IAmazonWebServicesIntegration, AmazonWebServicesIntegration>();
    }
}
