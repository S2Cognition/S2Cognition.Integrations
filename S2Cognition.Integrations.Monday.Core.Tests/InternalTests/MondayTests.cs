using FakeItEasy;
using S2Cognition.Integrations.Core.Models;
using S2Cognition.Integrations.Monday.Core.Models;

namespace S2Cognition.Integrations.Monday.Core.Tests.InternalTests;

public class MondayTests
{
    protected readonly Random _random;
    internal readonly MondayClient _mondayClient;
    protected readonly IGraphQlHttpClient _graphQlClient;

    public MondayTests()
    {
        _random = new Random();

        _graphQlClient = A.Fake<IGraphQlHttpClient>();
        _mondayClient = new MondayClient(_graphQlClient);
    }
}
