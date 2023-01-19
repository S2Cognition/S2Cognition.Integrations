namespace S2Cognition.Integrations.Zoom.Core.Data;

public class GetUsersResponse : PagedResponse
{
    public ICollection<UserRecord> Users { get; set; } = Array.Empty<UserRecord>();
}
