using Newtonsoft.Json;

namespace S2Cognition.Integrations.Monday.Core.Models;

/// <summary>
///     Items are grouped together in units called groups. Each board contains one or multiple groups, and each group can
///     hold one or many items.
/// </summary>
public class Group
{
    /// <summary>
    ///     The group's unique identifier.
    /// </summary>
    [JsonProperty("id")]
    public string? Id { get; set; }

    /// <summary>
    ///     The group's title.
    /// </summary>
    [JsonProperty("title")]
    public string? Name { get; set; }

    /// <summary>
    ///     The group's color.
    /// </summary>
    [JsonProperty("color")]
    public string? Color { get; set; }

    /// <summary>
    ///     Is the group archived.
    /// </summary>
    [JsonProperty("archived")]
    public bool? IsArchived { get; set; }

    /// <summary>
    ///     Is the group deleted.
    /// </summary>
    [JsonProperty("deleted")]
    public bool? IsDeleted { get; set; }
}