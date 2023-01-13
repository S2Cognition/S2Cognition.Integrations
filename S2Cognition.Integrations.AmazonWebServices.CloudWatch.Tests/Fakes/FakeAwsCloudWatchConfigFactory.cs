using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Tests.Fakes
{
    public class FakeAwsCloudWatchConfigFactory : IAwsCloudWatchConfigFactory
    {
        public IAwsCloudWatchConfig Create()
        {
            return new FakeAwsCloudWatchConfig();
        }
    }
}
