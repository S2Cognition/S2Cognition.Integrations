using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;
using S2Cognition.Integrations.AmazonWebServices.Ssm.Data;
using S2Cognition.Integrations.AmazonWebServices.Ssm.Models;
using S2Cognition.Integrations.Core;

namespace S2Cognition.Integrations.AmazonWebServices.Ssm;

public interface IAmazonWebServicesSsmIntegration : IIntegration<AmazonWebServicesSsmConfiguration>
{
    Task<GetSsmParameterResponse> GetSsmParameter(GetSsmParameterRequest req);
    Task<PutSsmParameterResponse> PutSsmParameter(PutSsmParameterRequest req);
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

    public async Task<GetSsmParameterResponse> GetSsmParameter(GetSsmParameterRequest req)
    {
        return await Client.GetParameter(req);
    }

    public async Task<PutSsmParameterResponse> PutSsmParameter(PutSsmParameterRequest req)
    {
        return await Client.PutParameter(req);
    }
}
