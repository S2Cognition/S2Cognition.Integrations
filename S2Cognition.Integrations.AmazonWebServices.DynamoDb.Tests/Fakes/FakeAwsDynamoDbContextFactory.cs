using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Tests.Fakes;

internal class FakeAwsDynamoDbContextFactory : IAwsDynamoDbContextFactory
{
    public IAwsDynamoDbContext Create(IAwsDynamoDbClient client)
    {
        return new FakeAwsDynamoDbContext();
    }
}
