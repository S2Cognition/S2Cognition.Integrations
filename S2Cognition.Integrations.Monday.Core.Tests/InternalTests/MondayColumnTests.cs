using FakeItEasy;
using GraphQL;
using S2Cognition.Integrations.Monday.Core.Models;
using S2Cognition.Integrations.Monday.Core.Models.Mutations;
using S2Cognition.Integrations.Monday.Core.Models.Responses;
using Shouldly;

namespace S2Cognition.Integrations.Monday.Core.Tests.InternalTests;

public class MondayColumnTests : MondayTests
{
    private void FakeCreateColumnResponse(string name)
    {
        A.CallTo(() => _graphQlClient.SendMutationAsync<CreateColumnResponse>(A<GraphQLRequest>._))
            .Returns(new GraphQLResponse<CreateColumnResponse>
            {
                Data = new CreateColumnResponse(new Column
                {
                    Id = _random.NextString(),
                    Name = name
                })
            });
    }

    [Fact]
    public async Task CreateColumn_Pass()
    {
        var boardId = _random.NextUInt64();
        var name = _random.NextString();

        FakeCreateColumnResponse(name);

        var result = await _mondayClient.CreateColumn(new CreateColumn(boardId, name, ColumnTypes.Status));

        result.ShouldNotBeNull();
        result.ShouldNotBe(string.Empty);
    }

    [Fact]
    public async Task UpdateColumn_Pass()
    {
        var boardId = _random.NextUInt64();
        var itemId = _random.NextUInt64();
        var columnId = _random.NextString();

        var result = await _mondayClient.UpdateColumn(new UpdateColumn(boardId, itemId, columnId, "{\"index\": 1}"));

        result.ShouldBeTrue();
    }
}

