using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Tests.Fakes;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Tests;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFakeAmazonWebServicesCloudWatch(this IServiceCollection sc)
    {
        return sc.AddSingleton<IAwsCloudWatchConfigFactory>(_ => new FakeAwsCloudWatchConfigFactory())
                   .AddSingleton<IAwsCloudWatchClientFactory>(_ => new FakeAwsCloudWatchClientFactory());
    }
}
