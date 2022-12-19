using System.Text.Json.Serialization;

namespace S2Cognition.Integrations.Zoom.Core.Models;

internal class ZoomGetUsersPagedResponse
{
    [JsonPropertyName("page_count")]
    public int? PageCount { get; set; }

    [JsonPropertyName("page_size")]
    public int? PageSize { get; set; }

    [JsonPropertyName("page_number")]
    public int? PageNumber { get; set; }

    [JsonPropertyName("total_records")]
    public int? TotalRecords { get; set; }

    [JsonPropertyName("next_page_token")]
    public string? NextPageToken { get; set; }

    [JsonPropertyName("users")]
    public IList<ZoomUserRecord>? Users { get; set; }
}