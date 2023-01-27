namespace S2Cognition.Integrations.NetSuite.Core.Data;

public class FindCustomerStatusesResponse
{
    public ICollection<PickListRecord> Results { get; set; } = Array.Empty<PickListRecord>();
}

