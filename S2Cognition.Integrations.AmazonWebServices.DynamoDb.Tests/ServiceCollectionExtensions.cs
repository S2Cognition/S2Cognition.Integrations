using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;
using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Tests.Fakes;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Tests;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFakeAmazonWebServicesDynamoDb(this IServiceCollection sc)
    {
        sc.AddSingleton<IAwsDynamoDbConfigFactory, FakeAwsDynamoDbConfigFactory>()
            .AddScoped<IAwsDynamoDbConfig, FakeAwsDynamoDbConfig>()
            .AddSingleton<IAwsDynamoDbClientFactory, FakeAwsDynamoDbClientFactory>()
            .AddScoped<IAwsDynamoDbClient, FakeAwsDynamoDbClient>()
            .AddSingleton<IAwsDynamoDbContextFactory, FakeAwsDynamoDbContextFactory>()
            .AddScoped<IAwsDynamoDbContext, FakeAwsDynamoDbContext>();

        return sc;
    }
}
