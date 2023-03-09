namespace S2Cognition.Integrations.Core.Tests;

[SuppressMessage("Major Code Smell", "S3881:\"IDisposable\" should be implemented correctly", Justification = "Not needed for test teardown")]
public class UnitTestBase : IDisposable
{
    protected readonly IFakeHttpClient _httpClient;
    protected readonly IServiceProvider _ioc;

    public UnitTestBase()
    {
        var sc = new ServiceCollection();
        sc.AddIntegrationUtilities();
        sc.AddFakeClients();
        IocSetup(sc).Wait();

        _ioc = sc.BuildServiceProvider();

        _httpClient = _ioc.GetRequiredService<IFakeHttpClient>();
        TestSetup().Wait();
    }

    [SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize", Justification = "Not needed for test teardown")]
    public void Dispose()
    {
        TestTeardown().Wait();
        _httpClient.EnsureDisposed();
    }

    protected virtual async Task IocSetup(IServiceCollection sc)
    {
        await Task.CompletedTask;
    }

    protected virtual async Task TestSetup()
    {
        await Task.CompletedTask;
    }

    protected virtual async Task TestTeardown()
    {
        await Task.CompletedTask;
    }
}