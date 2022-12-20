using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Tests;

public static class ServiceCollectionExtensions
{
    public static void AddFakeAmazonWebServicesDynamoDb(this IServiceCollection sc)
    {
        sc.AddScoped<IAwsDynamoDbConfigFactory, FakeAwsDynamoDbConfigFactory>();
        sc.AddScoped<IAwsDynamoDbConfig, FakeAwsDynamoDbConfig>();

        sc.AddScoped<IAwsDynamoDbClientFactory, FakeAwsDynamoDbClientFactory>();
        sc.AddScoped<IAwsDynamoDbClient, FakeAwsDynamoDbClient>();

        sc.AddScoped<IAwsDynamoDbContextFactory, FakeAwsDynamoDbContextFactory>();
        sc.AddScoped<IAwsDynamoDbContext, FakeAwsDynamoDbContext>();
    }
}
