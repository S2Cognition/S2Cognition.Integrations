namespace S2Cognition.Integrations.Monday.Core.Models.Mutations;

/// <summary>
///     Create a new item mutation.
/// </summary>
internal class CreateItem
{
    /// <summary>
    ///     The new item's name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     The board's unique identifier.
    /// </summary>
    public ulong BoardId { get; set; }

    /// <summary>
    ///     The group's unique identifier.
    /// </summary>
    public string GroupId { get; set; }

    /// <summary>
    ///     The column values of the new item [JSON].
    /// </summary>
    public string? ColumnValues { get; set; }

    internal CreateItem(string name, ulong boardId, string groupId)
    {
        Name = name;
        BoardId = boardId;
        GroupId = groupId;
    }
}