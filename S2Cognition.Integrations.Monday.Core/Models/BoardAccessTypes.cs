namespace S2Cognition.Integrations.Monday.Core.Models;

/// <summary>
///     The board's kind. (public / private / share)
/// </summary>
internal enum BoardAccessTypes
{
    /// <summary>
    ///     [Default State]
    /// </summary>
    Default,

    /// <summary>
    ///     Board is public.
    /// </summary>
    Public,

    /// <summary>
    ///     Board is private.
    /// </summary>
    Private,

    /// <summary>
    ///     Board is shared.
    /// </summary>
    Share
}