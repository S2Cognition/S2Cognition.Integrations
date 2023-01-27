namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

internal class AwsDynamoDbConfigFactory : IAwsDynamoDbConfigFactory
{
    public IAwsDynamoDbConfig Create()
    {
        return new AwsDynamoDbConfig();
    }
}


