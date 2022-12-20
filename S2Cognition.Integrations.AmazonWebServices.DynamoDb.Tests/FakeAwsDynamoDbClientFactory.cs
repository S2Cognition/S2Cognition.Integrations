using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Tests;

public class FakeAwsDynamoDbClientFactory : IAwsDynamoDbClientFactory
{
    public IAwsDynamoDbClient Create(IAwsDynamoDbConfig config)
    {
        return new FakeAwsDynamoDbClient();
    }
}
