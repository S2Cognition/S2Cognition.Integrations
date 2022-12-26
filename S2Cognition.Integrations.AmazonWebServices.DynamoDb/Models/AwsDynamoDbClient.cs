using Amazon.DynamoDBv2;
using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

public class AwsDynamoDbClient : IAwsDynamoDbClient
{
    private readonly AmazonDynamoDBClient _client;

    public AmazonDynamoDBClient Native => _client;

    public AwsDynamoDbClient(IAwsDynamoDbConfig config)
    {
        _client = new AmazonDynamoDBClient(config.Native);
    }
}

