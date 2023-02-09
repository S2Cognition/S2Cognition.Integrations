using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazanWebServices.S3.Data;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;
using S2Cognition.Integrations.AmazonWebServices.S3.Models;
using S2Cognition.Integrations.Core;

namespace S2Cognition.Integrations.AmazonWebServices.S3;

public interface IAmazonWebServicesS3Integration : IIntegration<AmazonWebServicesS3Configuration>
{
}
internal class AmazonWebServicesS3Integration : Integration<AmazonWebServicesS3Configuration>, IAmazonWebServicesS3Integration
{
    private IAwsS3Client? _client;

    private IAwsS3Client Client
    {
        get
        {
            if (_client == null)
            {
                var factory = _ioc.GetRequiredService<IAwsS3ClientFactory>();

                var regionFactory = _ioc.GetRequiredService<IAwsRegionFactory>();

                _client = factory.Create(new AwsS3Config
                {
                    ServiceUrl = Configuration.ServiceUrl,
                    RegionEndpoint = regionFactory.Create(Configuration.AwsRegion).Result
                });
            }

            return _client;
        }
    }
    internal AmazonWebServicesS3Integration(IServiceProvider serviceProvider)
: base(serviceProvider)
    {
    }
}
