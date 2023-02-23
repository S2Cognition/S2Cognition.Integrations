namespace S2Cognition.Integrations.AmazonWebServices.SSM.Models;

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
