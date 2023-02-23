using S2Cognition.Integrations.AmazonWebServices.SSM.Models;

namespace S2Cognition.Integrations.AmazonWebServices.SSM.Tests.Fakes;

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


