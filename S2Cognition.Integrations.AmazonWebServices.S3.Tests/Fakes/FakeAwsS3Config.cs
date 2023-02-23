using Amazon.S3;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;
using S2Cognition.Integrations.AmazonWebServices.S3.Models;

namespace S2Cognition.Integrations.AmazonWebServices.S3.Tests.Fakes;

internal class FakeAwsS3Config : IAwsS3Config
{
    public string? ServiceUrl { get; set; }
    public IAwsRegionEndpoint? RegionEndpoint { get; set; }

    public AmazonS3Config Native => throw new NotImplementedException();
}
