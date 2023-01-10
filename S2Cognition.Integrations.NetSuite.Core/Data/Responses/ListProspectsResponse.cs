namespace S2Cognition.Integrations.NetSuite.Core.Data.Responses;

public class Prospect
{
}

public class ListProspectsResponse 
{
    public IList<Prospect> Prospects { get; set; } = Array.Empty<Prospect>();
}
