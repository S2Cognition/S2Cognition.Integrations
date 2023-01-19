namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

internal interface IAwsDynamoDbClientFactory
{
    IAwsDynamoDbClient Create(IAwsDynamoDbConfig config);
}
