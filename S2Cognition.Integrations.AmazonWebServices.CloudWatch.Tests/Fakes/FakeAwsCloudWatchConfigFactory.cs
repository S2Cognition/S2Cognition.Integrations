using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Tests.Fakes;

internal class FakeAwsCloudWatchConfigFactory : IAwsCloudWatchConfigFactory
{
    public IAwsCloudWatchConfig Create()
    {
        return new FakeAwsCloudWatchConfig();
    }
}
