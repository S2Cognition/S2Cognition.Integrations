using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb;

public class AwsDynamoDbContextFactory : IAwsDynamoDbContextFactory
{
    public IAwsDynamoDbContext Create(IAwsDynamoDbClient client)
    {
        return new AwsDynamoDbContext(client);
    }
}


