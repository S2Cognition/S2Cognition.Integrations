using S2Cognition.Integrations.Core.Tests;
using S2Cognition.Integrations.Zoom.Core.Data;

namespace S2Cognition.Integrations.Zoom.Core.Tests;

public class GetUsersTests : UnitTestBase
{
    private ZoomConfiguration _configuration = default!;
    private IZoomIntegration _sut = default!;

    protected override async Task IocSetup(IServiceCollection sc)
    {
        sc.AddFakeZoomIntegration();
        await Task.CompletedTask;
    }

    protected override async Task TestSetup()
    {
        _configuration = new ZoomConfiguration(_ioc)
        {
            AccountId = "fake_account_id",
            ClientId = "fake_client_id",
            ClientSecret = "fake_client_secret"
        };

        _sut = _ioc.GetRequiredService<IZoomIntegration>();
        await _sut.Initialize(_configuration);
    }

    [Fact]
    public async Task EnsureGetUsersIsCallable()
    {
        var users = await _sut.GetUsers(new GetUsersRequest());

        users.ShouldNotBeNull();
    }
}