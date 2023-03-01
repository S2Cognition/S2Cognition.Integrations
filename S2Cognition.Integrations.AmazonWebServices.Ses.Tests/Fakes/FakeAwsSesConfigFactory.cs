using S2Cognition.Integrations.AmazonWebServices.Ses.Models;

namespace S2Cognition.Integrations.AmazonWebServices.Ses.Tests.Fakes;

internal class FakeAwsSesConfigFactory : IAwsSesConfigFactory
{
    internal FakeAwsSesConfigFactory()
    {
    }

    public IAwsSesConfig Create()
    {
        return new FakeAwsSesConfig();
    }
}
