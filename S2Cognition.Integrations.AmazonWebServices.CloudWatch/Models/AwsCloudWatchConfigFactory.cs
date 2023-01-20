namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models;

internal interface IAwsCloudWatchConfigFactory
{
    IAwsCloudWatchConfig Create();
}

internal class AwsCloudWatchConfigFactory : IAwsCloudWatchConfigFactory
{
    public IAwsCloudWatchConfig Create()
    {
        return new AwsCloudWatchConfig();
    }
}

