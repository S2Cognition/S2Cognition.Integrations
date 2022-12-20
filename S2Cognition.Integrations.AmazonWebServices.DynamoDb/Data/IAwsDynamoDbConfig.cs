using Amazon.DynamoDBv2;
using S2Cognition.Integrations.AmazonWebServices.Core.Data;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

public interface IAwsDynamoDbConfig
{
    string? ServiceURL { get; set; }
    IAwsRegionEndpoint? RegionEndpoint { get; set; }
    AmazonDynamoDBConfig Native { get; }
}
