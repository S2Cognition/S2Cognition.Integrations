using Amazon.CloudWatch;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Tests.Fakes
{
    public class FakeAwsCloudWatchClient : IAwsCloudWatchClient
    {
        public AmazonCloudWatchClient Native => throw new NotImplementedException();
    }
}
