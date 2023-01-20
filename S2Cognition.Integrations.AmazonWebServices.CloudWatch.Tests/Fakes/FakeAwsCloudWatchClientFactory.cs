using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Tests.Fakes;

internal class FakeAwsCloudWatchClientFactory : IAwsCloudWatchClientFactory
{
    public IAwsCloudWatchClient Create(IAwsCloudWatchConfig config)
    {
        return new FakeAwsCloudWatchClient();
    }
}

