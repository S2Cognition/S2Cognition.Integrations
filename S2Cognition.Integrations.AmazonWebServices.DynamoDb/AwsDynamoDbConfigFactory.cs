using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb;

public class AwsDynamoDbConfigFactory : IAwsDynamoDbConfigFactory
{
    public IAwsDynamoDbConfig Create()
    {
        return new AwsDynamoDbConfig();
    }
}


