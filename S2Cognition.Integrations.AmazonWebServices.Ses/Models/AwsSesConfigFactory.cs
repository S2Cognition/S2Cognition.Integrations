namespace S2Cognition.Integrations.AmazonWebServices.Ses.Models;

internal interface IAwsSesConfigFactory
{
    IAwsSesConfig Create();
}

internal class AwsSesConfigFactory : IAwsSesConfigFactory
{
    public IAwsSesConfig Create()
    {
        return new AwsSesConfig();
    }
}
