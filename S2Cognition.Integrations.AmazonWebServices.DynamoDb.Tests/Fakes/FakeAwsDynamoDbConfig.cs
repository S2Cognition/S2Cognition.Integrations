using Amazon.DynamoDBv2;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;
using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Tests.Fakes;

internal class FakeAwsDynamoDbConfig : IAwsDynamoDbConfig
{
    public string? ServiceUrl { get; set; }
    public IAwsRegionEndpoint? RegionEndpoint { get; set; }

    public AmazonDynamoDBConfig Native => throw new NotImplementedException();
}

