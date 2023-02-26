namespace S2Cognition.Integrations.AmazonWebServices.Ses.Models;

internal interface IAwsSesClientFactory
{
    IAwsSesClient Create(IAwsSesConfig config);
}
internal class AwsSesClientFactory : IAwsSesClientFactory
{
    public IAwsSesClient Create(IAwsSesConfig config)
    {
        return new AwsSesClient(config);
    }
}
