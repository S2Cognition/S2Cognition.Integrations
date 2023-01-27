using Amazon.DynamoDBv2;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

internal interface IAwsDynamoDbConfig
{
    string? ServiceUrl { get; set; }
    IAwsRegionEndpoint? RegionEndpoint { get; set; }
    AmazonDynamoDBConfig Native { get; }
}
