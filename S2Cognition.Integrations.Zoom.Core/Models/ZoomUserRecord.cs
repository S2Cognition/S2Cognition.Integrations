using System.Text.Json.Serialization;

namespace S2Cognition.Integrations.Zoom.Core.Models;

internal class ZoomUserRecord
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("type")]
    public int? Type { get; set; }

    [JsonPropertyName("pmi")]
    public long? PersonalMeetingId { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    [JsonPropertyName("verified")]
    public int? Verified { get; set; }

    [JsonPropertyName("dept")]
    public string? Department { get; set; }

    [JsonPropertyName("created_at")]
    public string? CreatedAt { get; set; }

    [JsonPropertyName("last_login_time")]
    public string? LastLoginTime { get; set; }

    [JsonPropertyName("last_client_version")]
    public string? LastClientVersion { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("role_id")]
    public string? RoleId { get; set; }

    [JsonPropertyName("employee_unique_id")]
    public string? EmployeeUniqueId { get; set; }

    [JsonPropertyName("user_created_at")]
    public string? UserCreatedAt { get; set; }
}