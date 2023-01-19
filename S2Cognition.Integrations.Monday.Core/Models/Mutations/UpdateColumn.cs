namespace S2Cognition.Integrations.Monday.Core.Models.Mutations;

/// <summary>
///     Change an item's column value mutation.
/// </summary>
internal class UpdateColumn
{
    /// <summary>
    ///     The board's unique identifier.
    /// </summary>
    public ulong BoardId { get; set; }

    /// <summary>
    ///     The item's unique identifier.
    /// </summary>
    public ulong ItemId { get; set; }

    /// <summary>
    ///     The column's unique identifier.
    /// </summary>
    public string ColumnId { get; set; }

    /// <summary>
    ///     The new value of the column. [JSON] [https://monday.com/developers/v2#column-values-section]
    /// </summary>
    public string Value { get; set; }

    internal UpdateColumn(ulong boardId, ulong itemId, string columnId, string value)
    {
        BoardId = boardId;
        ItemId = itemId;
        ColumnId = columnId;
        Value = value;
    }
}
