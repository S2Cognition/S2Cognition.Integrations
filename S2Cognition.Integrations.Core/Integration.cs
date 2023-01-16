using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.Core.Data;
using S2Cognition.Integrations.Core.Models;

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
    protected readonly IServiceProvider _ioc;

    private T? _configuration = default;
    private IDateTime? _dateTime = default;

    private bool _isInitialized = false;

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

    protected IDateTime DateTime
    {
        get
        {
            _dateTime ??= _ioc.GetRequiredService<IDateTime>();
            return _dateTime;
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
