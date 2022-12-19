namespace S2Cognition.Integrations.Zoom.Core.Models;

public class GetUsersResponse
{
    public int PageCount { get; set; } = 0;
    public int PageSize { get; set; } = 0;
    public int PageNumber { get; set; } = 0;
    public int TotalRecords { get; set; } = 0;
    public string? NextPageToken { get; set; } = null;
    public IList<UserRecord> Users { get; set; } = Array.Empty<UserRecord>();
}

public class UserRecord
{
    public string Id { get; set; } = String.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public int? Type { get; set; }
    public long? PersonalMeetingId { get; set; }
    public string? Timezone { get; set; }
    public int? Verified { get; set; }
    public string? Department { get; set; }
    public string? CreatedAt { get; set; }
    public string? LastLoginTime { get; set; }
    public string? LastClientVersion { get; set; }
    public string? Language { get; set; }
    public string? Status { get; set; }
    public string? RoleId { get; set; }
    public string? EmployeeUniqueId { get; set; }
    public string? UserCreatedAt { get; set; }
}
