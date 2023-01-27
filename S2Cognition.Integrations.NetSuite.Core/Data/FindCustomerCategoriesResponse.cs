namespace S2Cognition.Integrations.NetSuite.Core.Data;

public class FindCustomerCategoriesResponse
{
    public ICollection<PickListRecord> Results { get; set; } = Array.Empty<PickListRecord>();
}

public class PickListRecord
{
    public string Name { get; set; } = String.Empty;
    public string? InternalId { get; set; } = null;
    public string? ExternalId { get; set; } = null;
}

