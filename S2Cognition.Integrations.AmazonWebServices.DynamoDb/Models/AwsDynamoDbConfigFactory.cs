namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

internal interface IAwsDynamoDbConfigFactory
{
    IAwsDynamoDbConfig Create();
}

internal class AwsDynamoDbConfigFactory : IAwsDynamoDbConfigFactory
{
    public IAwsDynamoDbConfig Create()
    {
        return new AwsDynamoDbConfig();
    }
}


