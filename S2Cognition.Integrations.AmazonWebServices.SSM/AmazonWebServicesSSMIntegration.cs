using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;
using S2Cognition.Integrations.AmazonWebServices.SSM.Data;
using S2Cognition.Integrations.AmazonWebServices.SSM.Models;
using S2Cognition.Integrations.Core;

namespace S2Cognition.Integrations.AmazonWebServices.SSM;

public interface IAmazonWebServicesSsmIntegration : IIntegration<AmazonWebServicesSsmConfiguration>
{
}

internal class AmazonWebServicesSsmIntegration : Integration<AmazonWebServicesSsmConfiguration>, IAmazonWebServicesSsmIntegration
{
    private IAwsSsmClient? _client;
    private IAwsSsmClient Client
    {
        get
        {
            if (_client == null)
            {
                var factory = _ioc.GetRequiredService<IAwsSsmClientFactory>();

                var regionFactory = _ioc.GetRequiredService<IAwsRegionFactory>();

                _client = factory.Create(new AwsSsmConfig
                {
                    ServiceUrl = Configuration.ServiceUrl,
                    RegionEndpoint = regionFactory.Create(Configuration.AwsRegion).Result
                });
            }

            return _client;
        }
    }

    internal AmazonWebServicesSsmIntegration(IServiceProvider serviceProvider)
    : base(serviceProvider)
    {
    }
}
