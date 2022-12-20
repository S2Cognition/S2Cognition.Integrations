namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

public interface IAwsDynamoDbClientFactory
{
    IAwsDynamoDbClient Create(IAwsDynamoDbConfig config);
}
