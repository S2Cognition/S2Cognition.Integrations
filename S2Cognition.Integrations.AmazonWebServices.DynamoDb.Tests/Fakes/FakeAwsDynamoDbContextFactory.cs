using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Tests.Fakes;

public class FakeAwsDynamoDbContextFactory : IAwsDynamoDbContextFactory
{
    public IAwsDynamoDbContext Create(IAwsDynamoDbClient client)
    {
        return new FakeAwsDynamoDbContext();
    }
}
