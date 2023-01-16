using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.Core.Models;

namespace S2Cognition.Integrations.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIntegrationUtilities(this IServiceCollection sc)
    {
        sc.AddSingleton<IHttpClientFactory, HttpClientFactory>()
            .AddSingleton<IGraphQlHttpClientFactory, GraphQlHttpClientFactory>()
            .AddSingleton<IStringUtils, StringUtils>()
            .AddSingleton<IDateTime, DateTimeUtils>();

        return sc;
    }
}