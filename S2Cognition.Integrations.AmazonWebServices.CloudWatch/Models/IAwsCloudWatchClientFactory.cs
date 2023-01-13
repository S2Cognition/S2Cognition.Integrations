using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models
{
    public interface IAwsCloudWatchClientFactory
    {
        IAwsCloudWatchClient Create(IAwsCloudWatchConfig config);
    }
}
