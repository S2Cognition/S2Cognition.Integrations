using Amazon.DynamoDBv2;
using S2Cognition.Integrations.AmazonWebServices.Core.Data;
using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Tests.Fakes;

public class FakeAwsDynamoDbConfig : IAwsDynamoDbConfig
{
    public string? ServiceUrl { get; set; }
    public IAwsRegionEndpoint? RegionEndpoint { get; set; }

    public AmazonDynamoDBConfig Native => throw new NotImplementedException();
}

