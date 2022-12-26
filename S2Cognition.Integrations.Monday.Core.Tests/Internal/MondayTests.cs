using FakeItEasy;
using GraphQL.Client.Abstractions;

namespace S2Cognition.Integrations.Monday.Core.Tests.Internal;

public class MondayTests
{
    protected readonly Random _random;
    protected readonly MondayClient _mondayClient;
    protected readonly IGraphQLClient _graphQlClient;

    public MondayTests()
    {
        _random = new Random();

        _graphQlClient = A.Fake<IGraphQLClient>();
        _mondayClient = new MondayClient(_graphQlClient);
    }
}
