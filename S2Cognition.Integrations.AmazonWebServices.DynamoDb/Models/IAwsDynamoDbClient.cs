using Amazon.DynamoDBv2;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

internal interface IAwsDynamoDbClient
{
    AmazonDynamoDBClient Native { get; }
}
