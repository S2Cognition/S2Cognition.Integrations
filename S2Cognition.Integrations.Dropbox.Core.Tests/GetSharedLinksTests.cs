using static Dropbox.Api.Files.ListRevisionsMode;
using static Dropbox.Api.Sharing.SharingFileAccessError;
using System;

namespace S2Cognition.Integrations.Dropbox.Core.Tests;

public class GetSharedLinksTests : IntegrationsTests
{
    [Fact]
    public async Task EnsureGetSharedLinksChecksThatAccessCodeIsNotNull()
    {
        _configuration.AccessToken = null;
        await _sut.Initialize(_configuration);

        var ex = await Should.ThrowAsync<InvalidOperationException>(async () => await _sut.GetSharedLinks(new GetSharedLinksRequest()));

        ex.ShouldNotBeNull();
        ex.Message.ShouldBe(nameof(DropboxConfiguration.AccessToken));
    }

    [Fact]
    public async Task EnsureGetSharedLinksChecksThatLoginEmailAddressIsNotNull()
    {
        _configuration.LoginEmailAddress = null;
        await _sut.Initialize(_configuration);

        var ex = await Should.ThrowAsync<InvalidOperationException>(async () => await _sut.GetSharedLinks(new GetSharedLinksRequest()));

        ex.ShouldNotBeNull();
        ex.Message.ShouldBe(nameof(DropboxConfiguration.LoginEmailAddress));
    }

    private ListSharedLinksResult BuildListSharedLinksResult(int[] ids, bool HasMore = false)
    {
        var links = new List<SharedLinkMetadata>();
        foreach (var id in ids)
        {
            var permissions = new LinkPermissions(true, Array.Empty<VisibilityPolicy>(), true, true, true, true, true, true, true, requestedVisibility: RequestedVisibility.Public.Instance);
            links.Add(new FileLinkMetadata($"{id}", $"{id}", permissions, DateTime.UtcNow, DateTime.UtcNow, "123456789", 1, pathLower: Guid.NewGuid().ToString().ToLower()));
        }

        return new ListSharedLinksResult(links, HasMore, cursor: Guid.NewGuid().ToString());
    }

    [Fact]
    public async Task EnsureGetSharedLinksReturnsAllExpectedResults()
    {
        var initialResult = BuildListSharedLinksResult(new[] { 123, 234, 345 }, HasMore: true);
        var secondaryResult = BuildListSharedLinksResult(new[] { 987, 876, 765 });

        A.CallTo(() => _client.GetShares(A<string>._, A<string>._, A<ListSharedLinksArg>._))
            .ReturnsNextFromSequence(initialResult, secondaryResult);

        var resp = await _sut.GetSharedLinks(new GetSharedLinksRequest());

        resp.ShouldNotBeNull();
        A.CallTo(() => _client.GetShares(A<string>._, A<string>._, A<ListSharedLinksArg>._)).MustHaveHappenedTwiceExactly();
        resp.Entries.Count.ShouldBe(6);
        resp.Entries.Any(_ => _.Name == $"123").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"234").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"345").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"765").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"876").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"987").ShouldBeTrue();
    }
}
