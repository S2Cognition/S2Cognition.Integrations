using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb;

public class AwsDynamoDbClientFactory : IAwsDynamoDbClientFactory
{
    public IAwsDynamoDbClient Create(IAwsDynamoDbConfig config)
    {
        return new AwsDynamoDbClient(config);
    }
}


