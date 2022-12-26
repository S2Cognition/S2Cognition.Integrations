namespace S2Cognition.Integrations.Monday.Core.Models.Mutations;

/// <summary>
///     Create a new update mutation.
/// </summary>
public class CreateUpdate
{
    /// <summary>
    ///     The item's unique identifier.
    /// </summary>
    public ulong ItemId { get; set; }

    /// <summary>
    ///     The update text.
    /// </summary>
    public string Body { get; set; }

    public CreateUpdate(ulong itemId, string body)
    {
        ItemId = itemId;
        Body = body;
    }
}
