using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;

namespace S2Cognition.Integrations.AmazonWebServices.Core.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAmazonWebServicesIntegration(this IServiceCollection sc)
    {
        return sc.AddSingleton<IAmazonWebServicesIntegration>(_ => new AmazonWebServicesIntegration(_))
            .AddSingleton<IAwsRegionFactory>(_ => new AwsRegionFactory());
    }
}
