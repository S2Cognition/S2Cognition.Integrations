namespace S2Cognition.Integrations.NetSuite.Core.Data;

public class FindLeadsResponse
{
    public ICollection<LeadRecord> Leads { get; set; } = Array.Empty<LeadRecord>();
}
