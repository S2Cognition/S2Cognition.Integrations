namespace S2Cognition.Integrations.Dropbox.Core.Tests;

public class GetFilesTests : IntegrationsTests
{
    [Fact]
    public async Task EnsureGetFilesChecksThatAccessCodeIsNotNull()
    {
        _configuration.AccessToken = null;
        await _sut.Initialize(_configuration);

        var ex = await Should.ThrowAsync<InvalidOperationException>(async () => await _sut.GetFiles(new GetFilesRequest { Path = Guid.NewGuid().ToString() }));

        ex.ShouldNotBeNull();
        ex.Message.ShouldBe(nameof(DropboxConfiguration.AccessToken));
    }

    [Fact]
    public async Task EnsureGetFilesChecksThatLoginEmailAddressIsNotNull()
    {
        _configuration.LoginEmailAddress = null;
        await _sut.Initialize(_configuration);

        var ex = await Should.ThrowAsync<InvalidOperationException>(async () => await _sut.GetFiles(new GetFilesRequest { Path = Guid.NewGuid().ToString() }));

        ex.ShouldNotBeNull();
        ex.Message.ShouldBe(nameof(DropboxConfiguration.LoginEmailAddress));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData("\r")]
    [InlineData(" \t \n \r ")]
    public async Task EnsureGetFilesChecksThatPathIsNotNullOrWhitespace(string? path)
    {
        var ex = await Should.ThrowAsync<ArgumentException>(async () => await _sut.GetFiles(new GetFilesRequest { Path = path }));

        ex.ShouldNotBeNull();
        ex.Message.ShouldBe(nameof(GetFilesRequest.Path));
    }

    private SearchV2Result BuildSearchV2Result(int[] ids, bool HasMore = false)
    {
        var matches = new List<SearchMatchV2>();
        foreach (var id in ids)
        {
            var file = new FileMetadata($"{id}", $"{id}", DateTime.UtcNow, DateTime.UtcNow, "123456789", 1);
            matches.Add(new SearchMatchV2(new MetadataV2.Metadata(file)));
        }
        
        return new SearchV2Result(matches, HasMore, Guid.NewGuid().ToString());
    }

    [Fact]
    public async Task EnsureGetFilesReturnsAllExpectedResults()
    {
        var initialResult = BuildSearchV2Result(new[] { 123, 234, 345 }, HasMore: true);
        var secondaryResult = BuildSearchV2Result(new[] { 987, 876, 765});

        A.CallTo(() => _client.GetFiles(A<string>._, A<string>._, A<SearchV2Arg>._, A<string?>._))
            .ReturnsNextFromSequence(initialResult, secondaryResult);

        var resp = await _sut.GetFiles(new GetFilesRequest { Path = Guid.NewGuid().ToString() });

        resp.ShouldNotBeNull();
        A.CallTo(() => _client.GetFiles(A<string>._, A<string>._, A<SearchV2Arg>._, A<string?>._)).MustHaveHappenedTwiceExactly();
        resp.Entries.Count.ShouldBe(6);
        resp.Entries.Any(_ => _.Name == $"123").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"234").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"345").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"765").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"876").ShouldBeTrue();
        resp.Entries.Any(_ => _.Name == $"987").ShouldBeTrue();
    }
}
