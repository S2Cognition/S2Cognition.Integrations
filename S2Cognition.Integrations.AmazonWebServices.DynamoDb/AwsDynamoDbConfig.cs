using Amazon.DynamoDBv2;
using S2Cognition.Integrations.AmazonWebServices.Core.Data;
using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb;

public class AwsDynamoDbConfig : IAwsDynamoDbConfig
{
    public string? ServiceURL { get; set; }
    public IAwsRegionEndpoint? RegionEndpoint { get; set; }

    private readonly AmazonDynamoDBConfig _config;
    public AmazonDynamoDBConfig Native => _config;

    public AwsDynamoDbConfig()
    {
        _config = new AmazonDynamoDBConfig();
    }
}

