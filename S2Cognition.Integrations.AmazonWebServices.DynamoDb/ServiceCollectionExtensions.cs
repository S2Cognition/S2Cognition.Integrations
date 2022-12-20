using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb;

public static class ServiceCollectionExtensions
{
    public static void AddAmazonWebServicesDynamoDbIntegration(this IServiceCollection sc)
    {
        sc.AddScoped<IAmazonWebServicesDynamoDbIntegration, AmazonWebServicesDynamoDbIntegration>();

        sc.AddScoped<IAwsDynamoDbConfigFactory, AwsDynamoDbConfigFactory>();
        sc.AddScoped<IAwsDynamoDbConfig, AwsDynamoDbConfig>();

        sc.AddScoped<IAwsDynamoDbClientFactory, AwsDynamoDbClientFactory>();
        sc.AddScoped<IAwsDynamoDbClient, AwsDynamoDbClient>();

        sc.AddScoped<IAwsDynamoDbContextFactory, AwsDynamoDbContextFactory>();
        sc.AddScoped<IAwsDynamoDbContext, AwsDynamoDbContext>();
    }
}

