using S2Cognition.Integrations.AmazonWebServices.Core.Data;

namespace S2Cognition.Integrations.AmazonWebServices.Core.Tests.Fakes;

public class FakeAwsRegionFactory : IAwsRegionFactory
{
    public async Task<IAwsRegionEndpoint> Create(string awsRegion)
    {
        return await Task.FromResult(new FakeAwsRegionEndpoint());
    }
}

