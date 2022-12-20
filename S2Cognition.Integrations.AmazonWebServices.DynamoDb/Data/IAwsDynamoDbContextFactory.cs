namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

public interface IAwsDynamoDbContextFactory
{
    IAwsDynamoDbContext Create(IAwsDynamoDbClient client);
}
