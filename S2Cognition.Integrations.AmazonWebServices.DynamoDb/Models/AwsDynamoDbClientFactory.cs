namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

internal interface IAwsDynamoDbClientFactory
{
    IAwsDynamoDbClient Create(IAwsDynamoDbConfig config);
}

internal class AwsDynamoDbClientFactory : IAwsDynamoDbClientFactory
{
    public IAwsDynamoDbClient Create(IAwsDynamoDbConfig config)
    {
        return new AwsDynamoDbClient(config);
    }
}


