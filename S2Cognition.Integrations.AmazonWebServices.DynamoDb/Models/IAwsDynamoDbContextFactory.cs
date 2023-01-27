namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

internal interface IAwsDynamoDbContextFactory
{
    IAwsDynamoDbContext Create(IAwsDynamoDbClient client);
}
