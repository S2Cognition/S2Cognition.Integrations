using FakeItEasy;
using GraphQL.Client.Abstractions;
using S2Cognition.Integrations.Core.Models;
using S2Cognition.Integrations.Monday.Core.Models;

namespace S2Cognition.Integrations.Monday.Core.Tests.Internal;

public class MondayTests
{
    protected readonly Random _random;
    protected readonly MondayClient _mondayClient;
    protected readonly IGraphQlHttpClient _graphQlClient;

    public MondayTests()
    {
        _random = new Random();

        _graphQlClient = A.Fake<IGraphQlHttpClient>();
        _mondayClient = new MondayClient(_graphQlClient);
    }
}
