using FakeItEasy;
using GraphQL;
using S2Cognition.Integrations.Monday.Core.Models;
using S2Cognition.Integrations.Monday.Core.Models.Responses;
using Shouldly;

namespace S2Cognition.Integrations.Monday.Core.Tests.InternalTests;

public class MondayTeamTests : MondayTests
{
    private void FakeGetTeamsRequest()
    {
        A.CallTo(() => _graphQlClient.SendMutationAsync<GetTeamsResponse>(A<GraphQLRequest>._))
            .Returns(new GraphQLResponse<GetTeamsResponse>
            {
                Data = new GetTeamsResponse(new[] { new Team { Id = _random.NextUInt64() } })
            });
    }

    private void FakeGetTeamRequest(ulong id)
    {
        A.CallTo(() => _graphQlClient.SendMutationAsync<GetTeamsResponse>(A<GraphQLRequest>._))
            .Returns(new GraphQLResponse<GetTeamsResponse>
            {
                Data = new GetTeamsResponse(new[] { new Team { Id = id } })
            });
    }

    [Fact]
    public async Task GetTeams_Pass()
    {
        FakeGetTeamsRequest();

        var result = await _mondayClient.GetTeams();
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetTeam_Pass()
    {
        var teamId = _random.NextUInt64();

        FakeGetTeamRequest(teamId);

        var result = await _mondayClient.GetTeam(teamId);
        result.ShouldNotBeNull();
    }
}

