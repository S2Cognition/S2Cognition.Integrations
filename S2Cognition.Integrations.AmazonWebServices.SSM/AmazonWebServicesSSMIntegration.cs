using Amazon.SimpleSystemsManagement.Model;
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
                var factory = _serviceProvider.GetRequiredService<IAwsSsmClientFactory>();

                var regionFactory = _serviceProvider.GetRequiredService<IAwsRegionFactory>();

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
        if (String.IsNullOrWhiteSpace(req.Name))
            throw new ArgumentException(nameof(GetSsmParameterRequest.Name));

        var response = await Client.GetParameter(new GetParameterRequest
        {
            Name = req.Name
        });

        return new GetSsmParameterResponse
        {
            Value = response.Parameter.Value
        };
    }

    public async Task<PutSsmParameterResponse> PutSsmParameter(PutSsmParameterRequest req)
    {
        if (String.IsNullOrWhiteSpace(req.Name))
            throw new ArgumentException(nameof(PutSsmParameterRequest.Name));

        if (String.IsNullOrWhiteSpace(req.Value))
            throw new ArgumentException(nameof(PutSsmParameterRequest.Value));

        if (String.IsNullOrWhiteSpace(req.Type))
            throw new ArgumentException(nameof(PutSsmParameterRequest.Type));

        req.Name = $"{req.Name}-{Configuration.Environment}";

        await Client.PutParameter(new PutParameterRequest
        {
            Name = req.Name,
            Value = req.Value,
            Type = req.Type
        });

        return new PutSsmParameterResponse();
    }
}
