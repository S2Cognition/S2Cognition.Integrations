using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Tests.Fakes;

public class FakeAwsCloudWatchClientFactory : IAwsCloudWatchClientFactory
{
    public IAwsCloudWatchClient Create(IAwsCloudWatchConfig config)
    {
        return new FakeAwsCloudWatchClient();
    }
}

