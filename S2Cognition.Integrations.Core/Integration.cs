using S2Cognition.Integrations.Core.Data;

namespace S2Cognition.Integrations.Core;

public interface IIntegration<in T>
    where T : IConfiguration
{
    Task Initialize(T configuration);
}

public class Integration<T> : IIntegration<T>
    where T : IConfiguration
{
    private T? _configuration = default;
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
        await Task.CompletedTask;
    }
}
