using S2Cognition.Integrations.Core.Data;

namespace S2Cognition.Integrations.NetSuite.Core.Data;

/// <summary>
/// When refreshing sandbox, the consumer and token are rendered invalid.
/// To refresh (in staging, NOT IN PROD) follow these steps IN ORDER:
/// 
///    1. Setup => Integration => Manage Integrations
///       a. Change state of the appropriate integration to Blocked (save)
///       b. Create new integration with only TOKEN-BASED AUTHENTICATION and ISSUETOKEN ENDPOINT checked
///       c. Record newly created Consumer Key/Secret in a key vault (e.g. Keeper).  It is not possible to see these
///          again inside NetSuite, be sure to keep them in a safe accessible location for future reference.
///    2. Setup => Users/Roles => Access Tokens
///       a. Revoke existing access token
///       b. Create new access token
///       c. Record newly created Token Id/Secret in a key vaule (e.g. Keeper).  It is not possible to see these
///          again inside NetSuite, be sure to keep them in a safe accessible location for future reference.
///       
/// </summary>
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
