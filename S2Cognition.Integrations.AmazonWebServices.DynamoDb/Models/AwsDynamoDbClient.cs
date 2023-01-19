using Amazon.DynamoDBv2;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

internal class AwsDynamoDbClient : IAwsDynamoDbClient
{
    private readonly AmazonDynamoDBClient _client;

    public AmazonDynamoDBClient Native => _client;

    internal AwsDynamoDbClient(IAwsDynamoDbConfig config)
    {
        _client = new AmazonDynamoDBClient(config.Native);
    }
}

