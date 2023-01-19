namespace S2Cognition.Integrations.Monday.Core.Models.Mutations;

/// <summary>
///     Create a new tag or get it if it already exists mutation.
/// </summary>
internal class CreateTag
{
    /// <summary>
    ///     The new tag's name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     The private board id to create the tag at (not needed for public boards)
    /// </summary>
    public ulong BoardId { get; set; }

    internal CreateTag(string name, ulong boardId)
    {
        Name = name;
        BoardId = boardId;
    }
}
