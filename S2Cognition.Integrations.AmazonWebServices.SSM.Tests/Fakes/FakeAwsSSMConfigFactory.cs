using S2Cognition.Integrations.AmazonWebServices.Ssm.Models;

namespace S2Cognition.Integrations.AmazonWebServices.Ssm.Tests.Fakes;

internal class FakeAwsSsmConfigFactory : IAwsSsmConfigFactory
{
    internal FakeAwsSsmConfigFactory()
    {
    }

    public IAwsSsmConfig Create()
    {
        return new FakeAwsSsmConfig();
    }
}


