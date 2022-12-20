using Amazon;
using S2Cognition.Integrations.AmazonWebServices.Core.Data;

namespace S2Cognition.Integrations.AmazonWebServices.Core;

public class AwsRegionFactory : IAwsRegionFactory
{
    public async Task<IAwsRegionEndpoint> Create(string awsRegion)
    {
        return await Task.FromResult(new AwsRegionEndpoint(RegionEndpoint.GetBySystemName(awsRegion)));
    }
}

public class AwsRegionEndpoint : IAwsRegionEndpoint
{
    private readonly RegionEndpoint _region;

    public AwsRegionEndpoint(RegionEndpoint region)
    {
        _region = region;
    }
}