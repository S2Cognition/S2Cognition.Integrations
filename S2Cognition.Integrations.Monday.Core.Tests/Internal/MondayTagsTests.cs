using FakeItEasy;
using GraphQL;
using S2Cognition.Integrations.Monday.Core.Models;
using S2Cognition.Integrations.Monday.Core.Models.Mutations;
using S2Cognition.Integrations.Monday.Core.Models.Responses;
using Shouldly;
using System.Threading.Tasks;

namespace S2Cognition.Integrations.Monday.Core.Tests.Internal;

public class MondayTagsTests : MondayTests
{
    private void FakeGetTagsRequest()
    {
        A.CallTo(() => _graphQlClient.SendMutationAsync<GetBoardTagsResponse>(A<GraphQLRequest>._))
            .Returns(new GraphQLResponse<GetBoardTagsResponse>
            {
                Data = new GetBoardTagsResponse(new[] {
                    new Board{
                        Tags = new[] {
                            new Tag { }
                        }
                    }
                })
            });
    }

    private void FakeGetTagRequest()
    {
        A.CallTo(() => _graphQlClient.SendMutationAsync<GetTagsResponse>(A<GraphQLRequest>._))
            .Returns(new GraphQLResponse<GetTagsResponse>
            {
                Data = new GetTagsResponse(new[] {
                    new Tag { }
                })
            });
    }

    private void FakeCreateTagRequest()
    {
        A.CallTo(() => _graphQlClient.SendMutationAsync<CreateTagResponse>(A<GraphQLRequest>._))
            .Returns(new GraphQLResponse<CreateTagResponse>
            {
                Data = new CreateTagResponse(new Tag { Id = _random.NextUInt64() })
            });
    }

    [Fact]
    public async Task GetTags_Pass()
    {
        var boardId = _random.NextUInt64();

        FakeGetTagsRequest();

        var result = await _mondayClient.GetTags(boardId);
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetTag_Pass()
    {
        var tagId = _random.NextUInt64();

        FakeGetTagRequest();

        var result = await _mondayClient.GetTag(tagId);
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task CreateTag_Pass()
    {
        var name = _random.NextString();
        var boardId = _random.NextUInt64();

        FakeCreateTagRequest();

        var result = await _mondayClient.CreateTag(new CreateTag(name, boardId));

        result.ShouldBeGreaterThan(0ul);
    }
}

