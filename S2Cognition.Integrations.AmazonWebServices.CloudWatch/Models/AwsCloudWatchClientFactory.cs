using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models;

internal interface IAwsCloudWatchClientFactory
{
    IAwsCloudWatchClient Create(IAwsCloudWatchConfig config);
}


internal class AwsCloudWatchClientFactory : IAwsCloudWatchClientFactory
{
    public IAwsCloudWatchClient Create(IAwsCloudWatchConfig config)
    {
        return new AwsCloudWatchClient(config);
    }
}
