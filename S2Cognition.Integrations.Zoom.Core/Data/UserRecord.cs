namespace S2Cognition.Integrations.Zoom.Core.Data;

public class UserRecord
{
    public string Id { get; set; } = String.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Department { get; set; }

    // These are trivial to expose, but should be typed more appropriately:
#pragma warning disable S125 // Sections of code should not be commented out
    // public int? Type { get; set; }
    // public long? PersonalMeetingId { get; set; }
    // public string? Timezone { get; set; }
    // public int? Verified { get; set; }
    // public string? CreatedAt { get; set; }
    // public string? LastLoginTime { get; set; }
    // public string? LastClientVersion { get; set; }
    // public string? Language { get; set; }
    // public string? Status { get; set; }
    // public string? RoleId { get; set; }
    // public string? EmployeeUniqueId { get; set; }
    // public string? UserCreatedAt { get; set; }
#pragma warning restore S125 // Sections of code should not be commented out
}
