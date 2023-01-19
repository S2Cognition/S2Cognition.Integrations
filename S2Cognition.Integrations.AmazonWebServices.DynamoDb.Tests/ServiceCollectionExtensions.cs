using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;
using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Tests.Fakes;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Tests;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFakeAmazonWebServicesDynamoDb(this IServiceCollection sc)
    {
        return sc.AddSingleton<IAwsDynamoDbConfigFactory, FakeAwsDynamoDbConfigFactory>()
            .AddSingleton<IAwsDynamoDbClientFactory, FakeAwsDynamoDbClientFactory>()
            .AddSingleton<IAwsDynamoDbContextFactory, FakeAwsDynamoDbContextFactory>();
    }
}
