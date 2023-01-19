namespace S2Cognition.Integrations.Monday.Core.Models.Mutations;

/// <summary>
///     Create a new update mutation.
/// </summary>
internal class CreateUpdate
{
    /// <summary>
    ///     The item's unique identifier.
    /// </summary>
    public ulong ItemId { get; set; }

    /// <summary>
    ///     The update text.
    /// </summary>
    public string Body { get; set; }

    internal CreateUpdate(ulong itemId, string body)
    {
        ItemId = itemId;
        Body = body;
    }
}
