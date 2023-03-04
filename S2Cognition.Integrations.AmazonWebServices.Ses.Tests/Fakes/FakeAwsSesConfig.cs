using Amazon.SimpleEmailV2;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;
using S2Cognition.Integrations.AmazonWebServices.Ses.Models;

namespace S2Cognition.Integrations.AmazonWebServices.Ses.Tests.Fakes;

internal class FakeAwsSesConfig : IAwsSesConfig
{
    public string? ServiceUrl { get; set; }
    public IAwsRegionEndpoint? RegionEndpoint { get; set; }

    public AmazonSimpleEmailServiceV2Config Native => throw new NotImplementedException();
}
