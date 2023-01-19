using Amazon.DynamoDBv2;
using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Tests.Fakes;

internal class FakeAwsDynamoDbClient : IAwsDynamoDbClient
{
    public AmazonDynamoDBClient Native => throw new NotImplementedException();
}
