using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Tests.Fakes;

internal class FakeAwsDynamoDbConfigFactory : IAwsDynamoDbConfigFactory
{
    public IAwsDynamoDbConfig Create()
    {
        return new FakeAwsDynamoDbConfig();
    }
}
