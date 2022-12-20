using Amazon.DynamoDBv2;
using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Tests;

public class FakeAwsDynamoDbClient : IAwsDynamoDbClient
{
    public AmazonDynamoDBClient Native => throw new NotImplementedException();
}
