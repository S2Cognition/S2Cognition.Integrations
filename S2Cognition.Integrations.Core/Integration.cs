using S2Cognition.Integrations.Core.Data;

namespace S2Cognition.Integrations.Core;

public interface IIntegration<in T>
    where T : IConfiguration
{
    Task Initialize(T configuration);
    Task<bool> IsInitialized();
}

public class Integration<T> : IIntegration<T>
    where T : IConfiguration
{
    private T? _configuration = default;
    private bool _isInitialized = false;
    protected readonly IServiceProvider _ioc;

    public Integration(IServiceProvider serviceProvider)
    {
        _ioc = serviceProvider;
    }

    protected T Configuration
    {
        get
        {
            if (_configuration == null)
                throw new InvalidOperationException("Integration has not been initialized");

            return _configuration;
        }
    }

    public virtual async Task Initialize(T configuration)
    {
        _configuration = configuration;
        _isInitialized = true;
        await Task.CompletedTask;
    }

    public async Task<bool> IsInitialized()
    {
        return await Task.FromResult(_isInitialized);
    }
}
