namespace S2Cognition.Integrations.Dropbox.Core.Tests;

public class IntegrationsTests : UnitTestBase
{
    public IDropboxIntegration _sut = default!;
    public DropboxConfiguration _configuration = default!;
    public IDropboxNativeClient _client = default!;

    protected override async Task IocSetup(IServiceCollection sc)
    {
        sc.AddDropboxIntegration();

        sc.AddSingleton<IDropboxIntegration>(_ => {
            var sut = A.Fake<DropboxIntegration>(__ => __.WithArgumentsForConstructor(new object?[] { _ }));
            A.CallTo(sut).CallsBaseMethod();
            return sut;
        });
        _client = A.Fake<IDropboxNativeClient>();
        sc.AddSingleton(_client);

        await Task.CompletedTask;
    }

    protected override async Task TestSetup()
    {
        var email = Guid.NewGuid().ToString();

        _configuration = new DropboxConfiguration(_ioc)
        {
            AccessToken = Guid.NewGuid().ToString(),
            LoginEmailAddress = email
        };

        _sut = _ioc.GetRequiredService<IDropboxIntegration>();
        await _sut.Initialize(_configuration);

        var response = new GetTeamMembersResponse
        {
            Entries = new List<DropboxTeamMember>
            {
                new DropboxTeamMember
                { 
                    Id = Guid.NewGuid().ToString(),
                    Email = email,
                    Name = Guid.NewGuid().ToString()
                }
            }
        };
        A.CallTo(() => _sut.GetTeamMembers(A<GetTeamMembersRequest>._)).Returns(response);
    }

    [Fact]
    public async Task EnsureConstructionReturnsValidInstance()
    {
        _sut.ShouldNotBeNull();
        (await _sut.IsInitialized()).ShouldBeTrue();
    }
}
