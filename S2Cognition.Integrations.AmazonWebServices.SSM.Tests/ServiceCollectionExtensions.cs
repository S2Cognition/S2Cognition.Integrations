using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.SSM.Models;
using S2Cognition.Integrations.AmazonWebServices.SSM.Tests.Fakes;

namespace S2Cognition.Integrations.AmazonWebServices.SSM.Tests;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFakeAmazonWebServicesSsm(this IServiceCollection sc)
    {
        return sc.AddSingleton<IAwsSsmConfigFactory>(_ => new FakeAwsSsmConfigFactory())
                   .AddSingleton<IAwsSsmClientFactory>(_ => new FakeAwsSsmClientFactory());
    }
}
