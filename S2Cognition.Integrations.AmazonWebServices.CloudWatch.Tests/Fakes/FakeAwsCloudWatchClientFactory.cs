using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Tests.Fakes;

internal class FakeAwsCloudWatchClientFactory : IAwsCloudWatchClientFactory
{
    private FakeAwsCloudWatchClient? _client = null;

    public IAwsCloudWatchClient Create(IAwsCloudWatchConfig config)
    {
        if (_client == null)
        {
            _client = new FakeAwsCloudWatchClient();
        }
        return _client;
    }
}

