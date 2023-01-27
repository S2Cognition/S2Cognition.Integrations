using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAmazonWebServicesDynamoDbIntegration(this IServiceCollection sc)
    {
        return sc.AddSingleton<IAmazonWebServicesDynamoDbIntegration>(_ => new AmazonWebServicesDynamoDbIntegration(_))
            .AddSingleton<IAwsDynamoDbConfigFactory>(_ => new AwsDynamoDbConfigFactory())
            .AddSingleton<IAwsDynamoDbClientFactory>(_ => new AwsDynamoDbClientFactory())
            .AddSingleton<IAwsDynamoDbContextFactory>(_ => new AwsDynamoDbContextFactory());
    }
}

