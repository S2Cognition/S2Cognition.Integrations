namespace S2Cognition.Integrations.NetSuite.Core.Data;

public class FindProspectsResponse
{
    public ICollection<ProspectRecord> Prospects { get; set; } = Array.Empty<ProspectRecord>();
}
