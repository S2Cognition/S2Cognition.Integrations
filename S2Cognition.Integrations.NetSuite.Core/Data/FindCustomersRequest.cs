using S2Cognition.Integrations.Core.Data;

namespace S2Cognition.Integrations.NetSuite.Core.Data;

public class FindCustomersRequest
{
    public PickListRecord? Category { get; set; } = null;
    public PickListRecord? Status { get; set; } = null;
    public SearchTerm? CustomFields { get; set; } = null;
}

