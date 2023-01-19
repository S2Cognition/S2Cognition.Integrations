using Newtonsoft.Json;

namespace S2Cognition.Integrations.Monday.Core.Models;

/// <summary>
///     Monday.com tags are objects that help you group items from different groups or different boards throughout your
///     account by a consistent keyword. Tag entities are created and presented through the “Tags” column.
/// </summary>
internal class Tag
{
    /// <summary>
    ///     The tag's unique identifier.
    /// </summary>
    [JsonProperty("id")]
    public ulong Id { get; set; }

    /// <summary>
    ///     The tag's name.
    /// </summary>
    [JsonProperty("name")]
    public string? Name { get; set; }

    /// <summary>
    ///     The tag's color.
    /// </summary>
    [JsonProperty("color")]
    public string? Color { get; set; }
}
