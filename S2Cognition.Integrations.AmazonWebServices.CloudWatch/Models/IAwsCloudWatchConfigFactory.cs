using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models;

public class AwsCloudWatchConfigFactory : IAwsCloudWatchConfigFactory
{
    public IAwsCloudWatchConfig Create()
    {
        return new AwsCloudWatchConfig();
    }
}

