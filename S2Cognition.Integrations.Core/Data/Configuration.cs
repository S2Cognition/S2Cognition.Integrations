namespace S2Cognition.Integrations.Core.Data;

public interface IConfiguration
{
    IServiceProvider ServiceProvider { get; }
}

public class Configuration : IConfiguration
{
    public IServiceProvider ServiceProvider { get; private set; }

    public Configuration(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }
}
