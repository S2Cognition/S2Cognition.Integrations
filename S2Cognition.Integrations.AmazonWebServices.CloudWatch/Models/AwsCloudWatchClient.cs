using Amazon.CloudWatch;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models
{
    public class AwsCloudWatchClient : IAwsCloudWatchClient
    {
        private readonly AmazonCloudWatchClient _client;

        public AmazonCloudWatchClient Native => _client;

        public AwsCloudWatchClient(IAwsCloudWatchConfig config)
        {
            _client = new AmazonCloudWatchClient(config.Native);
        }
    }
}
