using S2Cognition.Integrations.Core.Data;

namespace S2Cognition.Integrations.Zoom.Core.Data;

public class ZoomConfiguration : Configuration
{
    public string AccountId { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;

    public ZoomConfiguration(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }
}