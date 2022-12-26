using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.Core.Models;
using S2Cognition.Integrations.Core.Tests.Fakes;

namespace S2Cognition.Integrations.Core.Tests;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFakeHttpClient(this IServiceCollection sc)
    {
        var client = new FakeHttpClient();
        sc.AddSingleton<IFakeHttpClient>(client);

        var factory = A.Fake<IHttpClientFactory>();
        sc.AddSingleton<IHttpClientFactory>(factory);

        A.CallTo(() => factory.CreateClient()).ReturnsLazily(_ =>
        {
            client.EnsureDisposed();
            return client;
        });

        return sc;
    }
}
