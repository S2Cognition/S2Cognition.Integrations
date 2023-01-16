using FakeItEasy;
using GraphQL;
using S2Cognition.Integrations.Monday.Core.Models;
using S2Cognition.Integrations.Monday.Core.Models.Mutations;
using S2Cognition.Integrations.Monday.Core.Models.Responses;
using Shouldly;
using System.Threading.Tasks;

namespace S2Cognition.Integrations.Monday.Core.Tests.Internal;

public class MondayUpdatesTests : MondayTests
{
    private void FakeCreateUpdateRequest()
    {
        A.CallTo(() => _graphQlClient.SendMutationAsync<CreateUpdateResponse>(A<GraphQLRequest>._))
            .Returns(new GraphQLResponse<CreateUpdateResponse>
            {
                Data = new CreateUpdateResponse(new Update { Id = _random.NextUInt64() })
            });
    }

    [Fact]
    public async Task CreateUpdate_Pass()
    {
        var itemId = _random.NextUInt64();
        var body = _random.NextString();

        FakeCreateUpdateRequest();

        var result = await _mondayClient.CreateUpdate(new CreateUpdate(itemId, body));

        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task ClearItemUpdates_Pass()
    {
        var itemId = _random.NextUInt64();

        var result = await _mondayClient.ClearItemUpdates(itemId);

        result.ShouldBeTrue();
    }
}

