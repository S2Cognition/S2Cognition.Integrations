using S2Cognition.Integrations.Core.Data;

namespace S2Cognition.Integrations.Monday.Core.Data;

public class MondayConfiguration : Configuration
{
    public string ApiKey { get; set; } = String.Empty;

    public MondayConfiguration(IServiceProvider serviceProvider)
        : base(serviceProvider)
    { 
    }
}
