namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;

public interface IAwsDynamoDbClientFactory
{
    IAwsCloudWatchClient Create(IAwsCloudWatchConfig config);
}

