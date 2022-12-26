using Newtonsoft.Json;
using S2Cognition.Integrations.Monday.Core.Models.Extensions;

namespace S2Cognition.Integrations.Monday.Core.Models;

/// <summary>
///     Columns and items are two core elements of a board. A board is formatted as a table where there are columns and
///     rows called items. In monday.com each column has a specific functionality it enables (ex. a numbers column, a text
///     column, a time tracking column, etc.)
/// </summary>
public class Column
{
    /// <summary>
    ///     The column's unique identifier.
    /// </summary>
    [JsonProperty("id")]
    public string? Id { get; set; }

    /// <summary>
    ///     The column's title.
    /// </summary>
    [JsonProperty("title")]
    public string? Name { get; set; }

    /// <summary>
    ///     The column's type.
    /// </summary>
    [JsonProperty("type")]
    public string? RawColumnType { get; set; }

    /// <summary>
    /// The column's type.
    /// </summary>
    [JsonIgnore]
    public ColumnTypes? ColumnType => ((RawColumnType != null) && Enum.TryParse(RawColumnType.FirstCharacterToUpper(), out ColumnTypes type)) ? type : null;

    /// <summary>
    ///     Is the column archived.
    /// </summary>
    [JsonProperty("archived")]
    public bool? IsArchived { get; set; }

    /// <summary>
    ///     The column's settings in a string form.
    /// </summary>
    [JsonProperty("settings_str")]
    public string? Settings { get; set; }
}
