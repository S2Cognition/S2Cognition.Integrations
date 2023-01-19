using Amazon;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;

namespace S2Cognition.Integrations.AmazonWebServices.Core.Tests.Fakes;

internal class FakeAwsRegionEndpoint : IAwsRegionEndpoint
{
    public RegionEndpoint Native => RegionEndpoint.USEast1;
}

