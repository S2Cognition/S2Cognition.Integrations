

namespace S2Cognition.Integrations.AmazonWebServices.Ssm.Models;

internal interface IAwsSsmClientFactory
{
    IAwsSsmClient Create(IAwsSsmConfig config);
}

internal class AwsSsmClientFactory : IAwsSsmClientFactory
{
    public IAwsSsmClient Create(IAwsSsmConfig config)
    {
        return new AwsSsmClient(config);
    }
}
