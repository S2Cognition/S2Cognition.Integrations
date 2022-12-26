using Amazon;
using S2Cognition.Integrations.AmazonWebServices.Core.Data;

namespace S2Cognition.Integrations.AmazonWebServices.Core.Tests;

public class FakeAwsRegionEndpoint : IAwsRegionEndpoint
{
    public RegionEndpoint Native => RegionEndpoint.USEast1;
}

