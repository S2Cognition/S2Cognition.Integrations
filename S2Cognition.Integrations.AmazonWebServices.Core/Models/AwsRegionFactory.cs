using Amazon;

namespace S2Cognition.Integrations.AmazonWebServices.Core.Models;

internal interface IAwsRegionFactory
{
    Task<IAwsRegionEndpoint> Create(string awsRegion);
}

internal class AwsRegionFactory : IAwsRegionFactory
{
    public async Task<IAwsRegionEndpoint> Create(string awsRegion)
    {
        return await Task.FromResult(new AwsRegionEndpoint(RegionEndpoint.GetBySystemName(awsRegion)));
    }
}
