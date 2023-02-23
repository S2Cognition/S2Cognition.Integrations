using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;
using S2Cognition.Integrations.AmazonWebServices.SSM.Models;

namespace S2Cognition.Integrations.AmazonWebServices.SSM;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAmazonWebServicesSsmIntegration(this IServiceCollection sc)
    {
        return sc.AddSingleton<IAmazonWebServicesSsmIntegration>(_ => new AmazonWebServicesSsmIntegration(_))
            .AddSingleton<IAwsSsmConfigFactory>(_ => new AwsSsmConfigFactory())
            .AddSingleton<IAwsSsmClientFactory>(_ => new AwsSsmClientFactory())
            .AddSingleton<IAwsRegionFactory>(_ => new AwsRegionFactory());
    }
}
