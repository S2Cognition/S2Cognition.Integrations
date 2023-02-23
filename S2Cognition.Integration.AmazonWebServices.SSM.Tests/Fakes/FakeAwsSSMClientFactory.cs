using S2Cognition.Integrations.AmazonWebServices.SSM.Models;

namespace S2Cognition.Integrations.AmazonWebServices.SSM.Tests.Fakes;

internal class FakeAwsSsmClientFactory : IAwsSsmClientFactory
{
    private FakeAwsSsmClient? _client = null;
    public IAwsSsmClient Create(IAwsSsmConfig config)
    {
        if (_client == null)
        {
            _client = new FakeAwsSsmClient();
        }
        return _client;
    }
}
