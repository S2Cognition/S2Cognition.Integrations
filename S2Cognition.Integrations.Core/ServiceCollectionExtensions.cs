using Microsoft.Extensions.DependencyInjection;

namespace S2Cognition.Integrations.Core;

public static class ServiceCollectionExtensions
{
    public static void AddIntegrationUtilities(this IServiceCollection sc)
    {
        sc.AddSingleton<IHttpClientFactory, HttpClientFactory>()
            .AddSingleton<IStringUtils, StringUtils>()
        ;
    }
}