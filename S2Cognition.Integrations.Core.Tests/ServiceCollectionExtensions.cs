using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.Core.Models;
using S2Cognition.Integrations.Core.Tests.Fakes;

namespace S2Cognition.Integrations.Core.Tests;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFakeClients(this IServiceCollection sc)
    {
        return sc.AddFakeHttpClient()
            .AddFakeGraphQlHttpClient();
    }

    public static IServiceCollection AddFakeHttpClient(this IServiceCollection sc)
    {
        var httpClient = new FakeHttpClient();
        sc.AddSingleton<IFakeHttpClient>(httpClient);

        var httpClientFactory = A.Fake<IHttpClientFactory>();
        sc.AddSingleton(httpClientFactory);

        A.CallTo(() => httpClientFactory.Create()).ReturnsLazily(_ =>
        {
            httpClient.EnsureDisposed();
            return httpClient;
        });

        return sc;
    }

    public static IServiceCollection AddFakeGraphQlHttpClient(this IServiceCollection sc)
    {
        var graphQlHttpClient = new FakeGraphQlHttpClient();
        sc.AddSingleton<IFakeGraphQlHttpClient>(graphQlHttpClient);

        var graphQlHttpClientFactory = A.Fake<IGraphQlHttpClientFactory>();
        sc.AddSingleton(graphQlHttpClientFactory);

        A.CallTo(() => graphQlHttpClientFactory.Create()).ReturnsLazily(_ =>
        {
            graphQlHttpClient.EnsureDisposed();
            return graphQlHttpClient;
        });

        return sc;
    }
}
