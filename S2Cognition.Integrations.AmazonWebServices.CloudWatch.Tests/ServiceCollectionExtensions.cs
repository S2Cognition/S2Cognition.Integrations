using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Tests.Fakes;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Tests
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFakeAmazonWebServicesCloudWatch(this IServiceCollection sc)
        {
            sc.AddSingleton<IAwsCloudWatchConfigFactory, FakeAwsCloudWatchConfigFactory>()
                       .AddScoped<IAwsCloudWatchConfig, FakeAwsCloudWatchConfig>()
                       .AddSingleton<IAwsCloudWatchClientFactory, FakeAwsCloudWatchClientFactory>()
                       .AddScoped<IAwsCloudWatchClient, FakeAwsCloudWatchClient>();


            return sc;
        }
    }
}
