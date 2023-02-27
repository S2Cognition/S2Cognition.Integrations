namespace S2Cognition.Integrations.AmazonWebServices.Ssm
    .Models;

internal interface IAwsSsmConfigFactory
{
    IAwsSsmConfig Create();
}

internal class AwsSsmConfigFactory : IAwsSsmConfigFactory
{
    public IAwsSsmConfig Create()
    {
        return new AwsSsmConfig();
    }
}
