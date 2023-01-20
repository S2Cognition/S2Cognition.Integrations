using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.Core.Models;

namespace S2Cognition.Integrations.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIntegrationUtilities(this IServiceCollection sc)
    {
        return sc.AddSingleton<IHttpClientFactory>(_ => new HttpClientFactory())
            .AddSingleton<IGraphQlHttpClientFactory>(_ => new GraphQlHttpClientFactory())
            .AddSingleton<IStringUtils>(_ => new StringUtils())
            .AddSingleton<IDateTimeUtils>(_ => new DateTimeUtils());
    }
}