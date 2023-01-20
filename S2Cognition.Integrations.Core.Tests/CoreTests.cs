using S2Cognition.Integrations.Core.Data;

namespace S2Cognition.Integrations.Core.Tests;

public class CoreTests : UnitTestBase
{
    private DummyConfiguration _configuration = default!;
    private IDummyIntegration _sut = default!;

    protected override async Task IocSetup(IServiceCollection sc)
    {
        sc.AddScoped<IDummyIntegration>(_ => new DummyIntegration(_));

        await Task.CompletedTask;
    }

    protected override async Task TestSetup()
    {
        _configuration = new DummyConfiguration(_ioc);
        _sut = _ioc.GetRequiredService<IDummyIntegration>();
        await Task.CompletedTask;
    }

    [Fact]
    public async Task EnsureCheckingForInitializationReturnsFalseWhenNotInitialize()
    {
        var isInitialized = await _sut.IsInitialized();

        isInitialized.ShouldBeFalse();
    }

    [Fact]
    public async Task EnsureCheckingForInitializationReturnsTrueWhenInitialize()
    {
        await _sut.Initialize(_configuration);

        var isInitialized = await _sut.IsInitialized();

        isInitialized.ShouldBeTrue();
    }
}

public class DummyConfiguration : Configuration
{
    public DummyConfiguration(IServiceProvider ioc) 
        : base(ioc)
    {
    }
}

public interface IDummyIntegration : IIntegration<DummyConfiguration>
{
}

internal class DummyIntegration : Integration<DummyConfiguration>, IDummyIntegration
{
    internal DummyIntegration(IServiceProvider ioc) 
        : base(ioc)
    {
    }
}
