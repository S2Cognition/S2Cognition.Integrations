namespace S2Cognition.Integrations.Dropbox.Core.Tests;

public class GetFolderTests : IntegrationsTests
{
    [Fact]
    public async Task EnsureGetFolderChecksThatAccessTokenIsNotNull()
    {
        _configuration.AccessToken = null;
        await _sut.Initialize(_configuration);

        var ex = await Should.ThrowAsync<InvalidOperationException>(async () => await _sut.GetFolder(new GetFolderRequest()));

        ex.ShouldNotBeNull();
        ex.Message.ShouldBe(nameof(DropboxConfiguration.AccessToken));
    }

    [Fact]
    public async Task EnsureGetFolderChecksThatLoginEmailAddressIsNotNull()
    {
        _configuration.LoginEmailAddress = null;
        await _sut.Initialize(_configuration);

        var ex = await Should.ThrowAsync<InvalidOperationException>(async () => await _sut.GetFolder(new GetFolderRequest()));

        ex.ShouldNotBeNull();
        ex.Message.ShouldBe(nameof(DropboxConfiguration.LoginEmailAddress));
    }

    private ListFolderResult BuildListFolderResult(int[] ids, bool Files = false, bool HasMore = false)
    {
        var entries = new List<Metadata>();
        foreach (var id in ids)
        {
            if (Files)
                entries.Add(new FileMetadata($"{id}", $"{id}", DateTime.UtcNow, DateTime.UtcNow, "123456789", 1));
            else
                entries.Add(new FolderMetadata($"{id}", $"{id}"));
        }

        return new ListFolderResult(entries, Guid.NewGuid().ToString(), HasMore);
    }

    [Fact]
    public async Task EnsureGetFolderReturnsAllExpectedResults()
    {
        var initialResult = BuildListFolderResult(new[] { 123, 234, 345 }, HasMore: true);
        var secondaryResult = BuildListFolderResult(new[] { 555, 666, 777 }, Files: true, HasMore: true);
        var tertiaryResult = BuildListFolderResult(new[] { 987, 876, 765 });

        A.CallTo(() => _client.GetFolder(A<string>._, A<string>._, A<ListFolderArg>._, A<string?>._))
            .ReturnsNextFromSequence(initialResult, secondaryResult, tertiaryResult);

        var resp = await _sut.GetFolder(new GetFolderRequest());

        resp.ShouldNotBeNull();
        A.CallTo(() => _client.GetFolder(A<string>._, A<string>._, A<ListFolderArg>._, A<string?>._)).MustHaveHappened(3, Times.Exactly);
        resp.Entries.Count.ShouldBe(9);
        resp.Entries.Any(_ => _.Name == $"123").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"234").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"345").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"555").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"666").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"777").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"765").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"876").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"987").ShouldBeTrue();
    }

    [Fact]
    public async Task EnsureGetFolderReturnsOnlyFoldersWhenRequested()
    {
        var initialResult = BuildListFolderResult(new[] { 123, 234, 345 }, HasMore: true);
        var secondaryResult = BuildListFolderResult(new[] { 555, 666, 777 }, Files: true, HasMore: true);
        var tertiaryResult = BuildListFolderResult(new[] { 987, 876, 765 });

        A.CallTo(() => _client.GetFolder(A<string>._, A<string>._, A<ListFolderArg>._, A<string?>._))
            .ReturnsNextFromSequence(initialResult, secondaryResult, tertiaryResult);

        var resp = await _sut.GetFolder(new GetFolderRequest { FoldersOnly = true });

        resp.ShouldNotBeNull();
        A.CallTo(() => _client.GetFolder(A<string>._, A<string>._, A<ListFolderArg>._, A<string?>._)).MustHaveHappened(3, Times.Exactly);
        resp.Entries.Count.ShouldBe(6);
        resp.Entries.Any(_ => _.Name == $"123").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"234").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"345").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"765").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"876").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"987").ShouldBeTrue();
    }
}
