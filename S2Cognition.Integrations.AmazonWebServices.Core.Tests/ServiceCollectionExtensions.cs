using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.Core.Data;

namespace S2Cognition.Integrations.AmazonWebServices.Core.Tests;

public static class ServiceCollectionExtensions
{
    public static void AddFakeAmazonWebServices(this IServiceCollection sc)
    {
        sc.AddScoped<IAwsRegionFactory, FakeAwsRegionFactory>();
        sc.AddScoped<IAwsRegionEndpoint, FakeAwsRegionEndpoint>();
    }
}

