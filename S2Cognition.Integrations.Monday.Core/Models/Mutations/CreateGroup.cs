namespace S2Cognition.Integrations.Monday.Core.Models.Mutations;

/// <summary>
///     Creates a new group in a specific board mutation.
/// </summary>
internal class CreateGroup
{
    /// <summary>
    ///     The board's unique identifier.
    /// </summary>
    public ulong BoardId { get; set; }

    /// <summary>
    ///     The name of the new group.
    /// </summary>
    public string Name { get; set; }

    internal CreateGroup(ulong boardId, string name)
    {
        BoardId = boardId;
        Name = name;
    }
}
