using FakeItEasy;
using GraphQL;
using S2Cognition.Integrations.Monday.Core.Models;
using S2Cognition.Integrations.Monday.Core.Models.Responses;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace S2Cognition.Integrations.Monday.Core.Tests.Internal;

public class MondayUserTests : MondayTests
{
    private void FakeGetUsersResponse(ulong? userId = null)
    {
        var userList = new List<User>();
        if (userId != null)
        {
            userList.Add(new User
            {
                Id = userId
            });
        }

        A.CallTo(() => _graphQlClient.SendQueryAsync<GetUsersResponse>(A<GraphQLRequest>._))
            .Returns(new GraphQLResponse<GetUsersResponse>
            {
                Data = new GetUsersResponse
                {
                    Users = userList
                }
            });
    }

    [Fact]
    public async Task GetUsers_Pass()
    {
        FakeGetUsersResponse();

        var result = await _mondayClient.GetUsers();

        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetUsers_UserAccessTypes_Pass()
    {
        var userId = _random.NextUInt64();

        FakeGetUsersResponse(userId);

        var result = await _mondayClient.GetUsers(UserAccessTypes.All);
        result.ShouldNotBeNull();

        result = await _mondayClient.GetUsers(UserAccessTypes.Guests);
        result.ShouldNotBeNull();

        result = await _mondayClient.GetUsers(UserAccessTypes.NonGuests);
        result.ShouldNotBeNull();

        result = await _mondayClient.GetUsers(UserAccessTypes.NonPending);
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetUser_Pass()
    {
        var userId = _random.NextUInt64();

        FakeGetUsersResponse(userId);

        var result = await _mondayClient.GetUser(userId);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(userId);
    }
}

