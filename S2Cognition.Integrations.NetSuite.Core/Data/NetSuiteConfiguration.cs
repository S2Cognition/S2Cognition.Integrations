using S2Cognition.Integrations.Core.Data;

namespace S2Cognition.Integrations.NetSuite.Core.Data;

public class NetSuiteConfiguration : Configuration
{
    public string? AccountId { get; set; }
    public string? ConsumerKey { get; set; }
    public string? ConsumerSecret { get; set; }
    public string? TokenId { get; set; }
    public string? TokenSecret { get; set; }

    public NetSuiteConfiguration(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }
}
