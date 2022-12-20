using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Tests;

public class FakeAwsDynamoDbConfigFactory : IAwsDynamoDbConfigFactory
{
    public IAwsDynamoDbConfig Create()
    {
        return new FakeAwsDynamoDbConfig();
    }
}
