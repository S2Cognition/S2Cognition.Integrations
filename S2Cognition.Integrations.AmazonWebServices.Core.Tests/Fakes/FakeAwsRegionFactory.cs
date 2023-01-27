using S2Cognition.Integrations.AmazonWebServices.Core.Models;

namespace S2Cognition.Integrations.AmazonWebServices.Core.Tests.Fakes;

internal class FakeAwsRegionFactory : IAwsRegionFactory
{
    public async Task<IAwsRegionEndpoint> Create(string awsRegion)
    {
        return await Task.FromResult(new FakeAwsRegionEndpoint());
    }
}

