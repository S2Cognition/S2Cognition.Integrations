using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;

namespace S2Cognition.Integrations.Core.Tests;

public static class ServiceCollectionExtensions
{
    public static void AddFakeHttpClient(this IServiceCollection sc)
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
    }
}
