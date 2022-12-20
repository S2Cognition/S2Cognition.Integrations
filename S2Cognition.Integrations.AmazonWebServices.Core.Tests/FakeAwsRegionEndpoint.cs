using Amazon;
using S2Cognition.Integrations.AmazonWebServices.Core.Data;

namespace S2Cognition.Integrations.AmazonWebServices.Core.Tests;

public class FakeAwsRegionEndpoint : IAwsRegionEndpoint
{
    public async Task<RegionEndpoint> GetBySystemName(string awsRegion)
    {
        return await Task.FromResult(RegionEndpoint.USEast1);
    }
}

