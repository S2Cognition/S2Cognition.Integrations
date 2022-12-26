using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAmazonWebServicesDynamoDbIntegration(this IServiceCollection sc)
    {
        sc.AddSingleton<IAmazonWebServicesDynamoDbIntegration, AmazonWebServicesDynamoDbIntegration>()
            .AddSingleton<IAwsDynamoDbConfigFactory, AwsDynamoDbConfigFactory>()
            .AddScoped<IAwsDynamoDbConfig, AwsDynamoDbConfig>()
            .AddSingleton<IAwsDynamoDbClientFactory, AwsDynamoDbClientFactory>()
            .AddScoped<IAwsDynamoDbClient, AwsDynamoDbClient>()
            .AddSingleton<IAwsDynamoDbContextFactory, AwsDynamoDbContextFactory>()
            .AddScoped<IAwsDynamoDbContext, AwsDynamoDbContext>();

        return sc;
    }
}

