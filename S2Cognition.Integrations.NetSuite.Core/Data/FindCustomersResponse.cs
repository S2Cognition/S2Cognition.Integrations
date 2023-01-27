namespace S2Cognition.Integrations.NetSuite.Core.Data;
        
public class FindCustomersResponse 
{
    public int? TotalRecords { get; set; } = null;
    public ICollection<CustomerRecord> Customers { get; set; } = Array.Empty<CustomerRecord>();
}
