using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;
using S2Cognition.Integrations.AmazonWebServices.Core.Tests.Fakes;

namespace S2Cognition.Integrations.AmazonWebServices.Core.Tests;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFakeAmazonWebServices(this IServiceCollection sc)
    {
        return sc.AddSingleton<IAwsRegionFactory>(_ => new FakeAwsRegionFactory());
    }
}

