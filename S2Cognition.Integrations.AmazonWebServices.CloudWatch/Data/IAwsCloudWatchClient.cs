using Amazon.CloudWatch;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data
{
    public interface IAwsCloudWatchClient
    {
        AmazonCloudWatchClient Native { get; }
    }
}

