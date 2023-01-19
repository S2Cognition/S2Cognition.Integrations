namespace S2Cognition.Integrations.Monday.Core.Models.Mutations;

/// <summary>
///     Create a new column in board mutation.
/// </summary>
internal class CreateColumn
{
    /// <summary>
    ///     The board's unique identifier.
    /// </summary>
    public ulong BoardId { get; set; }

    /// <summary>
    ///     The new column's title.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     The type of column to create.
    /// </summary>
    public ColumnTypes ColumnType { get; set; }

    /// <summary>
    ///     The new column's defaults. [JSON]
    /// </summary>
    public string? Defaults { get; set; }

    internal CreateColumn(ulong boardId, string name, ColumnTypes columnType)
    {
        BoardId = boardId;
        Name = name;
        ColumnType = columnType;
    }
}
