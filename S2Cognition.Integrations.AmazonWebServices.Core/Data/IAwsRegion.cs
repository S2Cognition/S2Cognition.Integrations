namespace S2Cognition.Integrations.AmazonWebServices.Core.Data;

public interface IAwsRegionFactory
{
    public Task<IAwsRegionEndpoint> Create(string awsRegion);
}
