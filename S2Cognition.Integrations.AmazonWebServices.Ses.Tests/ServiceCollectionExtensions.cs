using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.Ses.Models;
using S2Cognition.Integrations.AmazonWebServices.Ses.Tests.Fakes;

namespace S2Cognition.Integrations.AmazonWebServices.Ses.Tests;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFakeAmazonWebServicesSes(this IServiceCollection sc)
    {
        return sc.AddSingleton<IAwsSesConfigFactory>(_ => new FakeAwsSesConfigFactory())
                   .AddSingleton<IAwsSesClientFactory>(_ => new FakeAwsSesClientFactory());
    }
}
