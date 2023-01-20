using FakeItEasy;
using GraphQL;
using S2Cognition.Integrations.Monday.Core.Models;
using S2Cognition.Integrations.Monday.Core.Models.Responses;
using Shouldly;

namespace S2Cognition.Integrations.Monday.Core.Tests.InternalTests;

public class MondaySystemTests : MondayTests
{
    private void FakeRateLimit()
    {
        A.CallTo(() => _graphQlClient.SendMutationAsync<GetComplexityResponse>(A<GraphQLRequest>._))
            .Returns(new GraphQLResponse<GetComplexityResponse>
            {
                Data = new GetComplexityResponse(new Complexity { After = 1234 })
            });
    }

    private void FakeComplexity()
    {
        A.CallTo(() => _graphQlClient.SendMutationAsync<GetComplexityResponse>(A<GraphQLRequest>._))
            .Returns(new GraphQLResponse<GetComplexityResponse>
            {
                Data = new GetComplexityResponse(new Complexity { })
            });
    }

    [Fact]
    public async Task GetRateLimit_Pass()
    {
        FakeRateLimit();

        var result = await _mondayClient.GetRateLimit();

        result.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task GetComplexity_Pass()
    {
        FakeComplexity();

        var result = await _mondayClient.GetComplexity("users(kind: non_guests) { id name }");

        result.ShouldNotBeNull();
    }
}

