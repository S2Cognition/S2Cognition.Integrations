using Amazon.DynamoDBv2;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

public interface IAwsDynamoDbClient
{
    AmazonDynamoDBClient Native { get; }
}
