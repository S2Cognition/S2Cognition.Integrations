using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Tests.Fakes;

internal class FakeAwsDynamoDbClientFactory : IAwsDynamoDbClientFactory
{
    public IAwsDynamoDbClient Create(IAwsDynamoDbConfig config)
    {
        return new FakeAwsDynamoDbClient();
    }
}
