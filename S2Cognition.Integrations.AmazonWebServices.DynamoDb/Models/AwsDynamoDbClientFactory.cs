namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

internal class AwsDynamoDbClientFactory : IAwsDynamoDbClientFactory
{
    public IAwsDynamoDbClient Create(IAwsDynamoDbConfig config)
    {
        return new AwsDynamoDbClient(config);
    }
}


