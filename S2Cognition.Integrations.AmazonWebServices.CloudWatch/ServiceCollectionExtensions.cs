using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAmazonWebServicesCloudWatchIntegration(this IServiceCollection sc)
    {
        return sc.AddSingleton<IAmazonWebServicesCloudWatchIntegration>(_ => new AmazonWebServicesCloudWatchIntegration(_))
            .AddSingleton<IAwsCloudWatchConfigFactory>(_ => new AwsCloudWatchConfigFactory())
            .AddSingleton<IAwsCloudWatchClientFactory>(_ => new AwsCloudWatchClientFactory())
            .AddSingleton<IAwsRegionFactory>(_ => new AwsRegionFactory());
    }
}



