using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;
using S2Cognition.Integrations.AmazonWebServices.S3.Models;

namespace S2Cognition.Integrations.AmazonWebServices.S3;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAmazonWebServicesS3Integration(this IServiceCollection sc)
    {
        return sc.AddSingleton<IAmazonWebServicesS3Integration>(_ => new AmazonWebServicesS3Integration(_))
            .AddSingleton<IAwsS3ConfigFactory>(_ => new AwsS3ConfigFactory())
            .AddSingleton<IAwsS3ClientFactory>(_ => new AwsS3ClientFactory())
            .AddSingleton<IAwsRegionFactory>(_ => new AwsRegionFactory());
    }
}
