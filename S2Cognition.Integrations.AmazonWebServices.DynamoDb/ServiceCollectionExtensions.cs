using Microsoft.Extensions.DependencyInjection;

namespace S2Cognition.Integrations.AmazonWebServices.Core;

public static class ServiceCollectionExtensions
{
    public static void AddAmazonWebServicesDynamoDbIntegration(this IServiceCollection sc)
    {
        sc.AddScoped<IAmazonWebServicesDynamoDbIntegration, AmazonWebServicesDynamoDbIntegration>();
    }
}
