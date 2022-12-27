using S2Cognition.Integrations.Monday.Core.Data;

namespace S2Cognition.Integrations.Monday.Core;

public interface IMondayIntegration
{
    Task<object?> GetUsers();
    Task Initialize(MondayConfiguration configuration);
}

public class MondayIntegration : IMondayIntegration
{
    private MondayConfiguration? _configuration;

    public async Task Initialize(MondayConfiguration configuration)
    {
        _configuration = configuration;
        await Task.CompletedTask;
    }

    public async Task<object?> GetUsers()
    {
        return await Task.FromResult(123);
    }
}
