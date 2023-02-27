using Amazon;
using Amazon.SimpleEmailV2;
using Amazon.SimpleEmailV2.Model;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;

namespace S2Cognition.Integrations.AmazonWebServices.Ses.Models;

internal interface IAwsSesClient
{
    Task<SendEmailResponse> Send(SendEmailRequest req);
}

internal class AwsSesClient : IAwsSesClient
{
    private readonly IAwsSesConfig _config;

    internal AwsSesClient(IAwsSesConfig config)
    {
        _config = config;
    }

    public async Task<SendEmailResponse> Send(SendEmailRequest req)
    {
        var region = _config.RegionEndpoint ?? new AwsRegionEndpoint(RegionEndpoint.USEast1);
        var ses = new AmazonSimpleEmailServiceV2Client(region.Native);
        return await ses.SendEmailAsync(req);
    }
}

