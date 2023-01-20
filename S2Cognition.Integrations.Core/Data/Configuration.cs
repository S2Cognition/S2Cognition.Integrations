namespace S2Cognition.Integrations.Core.Data;

public interface IConfiguration
{
    IServiceProvider IoC { get; }
}

public class Configuration : IConfiguration
{
    public const int DefaultPageSize = 25;

    public IServiceProvider IoC { get; private set; }

    protected Configuration(IServiceProvider ioc)
    {
        IoC = ioc;
    }
}
