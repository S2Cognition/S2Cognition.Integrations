using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.S3.Models;
using S2Cognition.Integrations.AmazonWebServices.S3.Tests.Fakes;

namespace S2Cognition.Integrations.AmazonWebServices.S3.Tests;


public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFakeAmazonWebServicesS3(this IServiceCollection sc)
    {
        return sc.AddSingleton<IAwsS3ConfigFactory>(_ => new FakeAwsS3ConfigFactory())
                   .AddSingleton<IAwsS3ClientFactory>(_ => new FakeAwsS3ClientFactory());
    }
}

