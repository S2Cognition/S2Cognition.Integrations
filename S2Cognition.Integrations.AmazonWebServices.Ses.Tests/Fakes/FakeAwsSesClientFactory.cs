using S2Cognition.Integrations.AmazonWebServices.Ses.Models;

namespace S2Cognition.Integrations.AmazonWebServices.Ses.Tests.Fakes;

internal class FakeAwsSesClientFactory : IAwsSesClientFactory
{
    private FakeAwsSesClient? _client = null;

    public IAwsSesClient Create(IAwsSesConfig config)
    {
        if (_client == null)
        {
            _client = new FakeAwsSesClient();
        }
        return _client;
    }
}
