namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

internal class AwsDynamoDbContextFactory : IAwsDynamoDbContextFactory
{
    public IAwsDynamoDbContext Create(IAwsDynamoDbClient client)
    {
        return new AwsDynamoDbContext(client);
    }
}


