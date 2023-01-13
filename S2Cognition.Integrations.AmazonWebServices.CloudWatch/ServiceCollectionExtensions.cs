using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.Core;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAmazonWebServicesCloudWatchIntegration(this IServiceCollection sc)
        {
            sc.AddIntegrationUtilities()
                .AddScoped<ICloudWatchIntegration, CloudWatchIntegration>();

            return sc;
        }
    }
}
