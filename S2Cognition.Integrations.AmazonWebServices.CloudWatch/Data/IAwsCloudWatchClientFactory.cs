namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;

public interface IAwsCloudWatchClientFactory
{
    IAwsCloudWatchClient Create(IAwsCloudWatchConfig config);
}

