using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;
using S2Cognition.Integrations.AmazonWebServices.Ses.Models;

namespace S2Cognition.Integrations.AmazonWebServices.Ses;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAmazonWebServicesS3Integration(this IServiceCollection sc)
    {
        return sc.AddSingleton<IAmazonWebServicesSesIntegration>(_ => new AmazonWebServicesSesIntegration(_))
            .AddSingleton<IAwsSesConfigFactory>(_ => new AwsSesConfigFactory())
            .AddSingleton<IAwsSesClientFactory>(_ => new AwsSesClientFactory())
            .AddSingleton<IAwsRegionFactory>(_ => new AwsRegionFactory());
    }
}
