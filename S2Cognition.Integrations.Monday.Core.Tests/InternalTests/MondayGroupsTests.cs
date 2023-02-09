using FakeItEasy;
using GraphQL;
using S2Cognition.Integrations.Monday.Core.Models;
using S2Cognition.Integrations.Monday.Core.Models.Mutations;
using S2Cognition.Integrations.Monday.Core.Models.Responses;
using Shouldly;

namespace S2Cognition.Integrations.Monday.Core.Tests.InternalTests;

public class MondayGroupsTests : MondayTests
{
    private void FakeGetGroupsResponse(ulong boardId)
    {
        A.CallTo(() => _graphQlClient.SendMutationAsync<GetGroupsResponse>(A<GraphQLRequest>._))
            .Returns(new GraphQLResponse<GetGroupsResponse>
            {
                Data = new GetGroupsResponse(new[] {
                    new Board{
                        Id = boardId,
                        Groups = new[] {
                            new Group {
                                Id = _random.NextString()
                            }
                        }
                    }
                })
            });
    }

    private void FakeCreateGroupResponse(string name)
    {
        A.CallTo(() => _graphQlClient.SendMutationAsync<CreateGroupResponse>(A<GraphQLRequest>._))
            .Returns(new GraphQLResponse<CreateGroupResponse>
            {
                Data = new CreateGroupResponse(new Group
                {
                    Id = _random.NextString(),
                    Name = name
                })
            });
    }

    [Fact]
    public async Task GetGroups_Pass()
    {
        var groupId = _random.NextUInt64();

        FakeGetGroupsResponse(groupId);

        var result = await _mondayClient.GetGroups(groupId);
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task UpdateItemGroup_Pass()
    {
        var itemId = _random.NextUInt64();
        var groupId = _random.NextString();

        var result = await _mondayClient.UpdateItemGroup(itemId, groupId);

        result.ShouldBeTrue();
    }

    [Fact]
    public async Task CreateGroup_Pass()
    {
        var boardId = _random.NextUInt64();
        var name = _random.NextString();

        FakeCreateGroupResponse(name);

        var result = await _mondayClient.CreateGroup(new CreateGroup(boardId, name));

        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task ArchiveGroup_Pass()
    {
        var boardId = _random.NextUInt64();
        var groupId = _random.NextString();

        var result = await _mondayClient.ArchiveGroup(boardId, groupId);

        result.ShouldBeTrue();
    }

    [Fact]
    public async Task DeleteGroup_Pass()
    {
        var boardId = _random.NextUInt64();
        var groupId = _random.NextString();

        var result = await _mondayClient.DeleteGroup(boardId, groupId);

        result.ShouldBeTrue();
    }
}

