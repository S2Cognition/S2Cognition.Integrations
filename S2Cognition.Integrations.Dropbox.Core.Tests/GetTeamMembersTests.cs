namespace S2Cognition.Integrations.Dropbox.Core.Tests;

public class GetTeamMembersTests : IntegrationsTests
{
    protected override async Task TestSetup()
    {
        await base.TestSetup();

        // The base test class init methods override get team members for all tests by default.
        // This resets that and allows testing of that method.
        A.CallTo(_sut).CallsBaseMethod();
    }

    [Fact]
    public async Task EnsureGetTeamMembersChecksThatAccessTokenIsNotNull()
    {
        _configuration.AccessToken = null;
        await _sut.Initialize(_configuration);

        var ex = await Should.ThrowAsync<InvalidOperationException>(async () => await _sut.GetTeamMembers(new GetTeamMembersRequest()));

        ex.ShouldNotBeNull();
        ex.Message.ShouldBe(nameof(DropboxConfiguration.AccessToken));
    }

    private MembersListV2Result BuildMembersListV2Result(int[] ids, bool HasMore = false)
    {
        var members = new List<TeamMemberInfoV2>();
        foreach (var id in ids)
        {
            members.Add(new TeamMemberInfoV2(
                new TeamMemberProfile($"{id}", $"test{id}@example.com", true,
                    TeamMemberStatus.Active.Instance,
                    new Name($"Given Name {id}", $"Surname {id}", $"Familiar Name {id}", $"Test #{id}",
                        $"Abbreviated Name {id}"),
                    TeamMembershipType.Full.Instance, Array.Empty<string>(), $"A{id}Z"
                )
            ));
        }

        return new MembersListV2Result(members, Guid.NewGuid().ToString(), HasMore);
    }

    [Fact]
    public async Task EnsureGetTeamMembersReturnsAllExpectedResults()
    {
        var initialResult = BuildMembersListV2Result(new[] { 123, 234, 345 }, HasMore: true);
        var secondaryResult = BuildMembersListV2Result(new[] { 987 });

        A.CallTo(() => _client.GetTeamMembers(A<string>._, A<string?>._))
            .ReturnsNextFromSequence(initialResult, secondaryResult);

        var resp = await _sut.GetTeamMembers(new GetTeamMembersRequest());

        resp.ShouldNotBeNull();
        A.CallTo(() => _client.GetTeamMembers(A<string>._, A<string?>._)).MustHaveHappenedTwiceExactly();
        resp.Entries.Count.ShouldBe(4);
        resp.Entries.Any(_ => _.Id == $"123").ShouldBeTrue();
        resp.Entries.Any(_ => _.Id == $"234").ShouldBeTrue();
        resp.Entries.Any(_ => _.Id == $"345").ShouldBeTrue();
        resp.Entries.Any(_ => _.Id == $"987").ShouldBeTrue();
    }
}
