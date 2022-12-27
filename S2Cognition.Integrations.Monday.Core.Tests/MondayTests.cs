using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.Core.Tests;
using S2Cognition.Integrations.Monday.Core.Data;
using Shouldly;

namespace S2Cognition.Integrations.Monday.Core.Tests;

public class MondayTests : UnitTestBase
{
    private MondayConfiguration _configuration = default!;
    private IMondayIntegration _sut = default!;

    protected override async Task IocSetup(IServiceCollection sc)
    {
        sc.AddMondayIntegration();
        await Task.CompletedTask;
    }

    protected override async Task TestSetup()
    {
        _configuration = new MondayConfiguration(_ioc)
        {
        };

        _sut = _ioc.GetRequiredService<IMondayIntegration>();
        await _sut.Initialize(_configuration);
    }

    [Fact]
    public async Task EnsureGetUsersIsCallable()
    {
        var users = await _sut.GetUsers();

        users.ShouldNotBeNull();
    }
}