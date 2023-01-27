namespace S2Cognition.Integrations.Monday.Core.Models;

/// <summary>
///     Get the complexity data of your queries.
/// </summary>
internal class Complexity
{
    /// <summary>
    ///     The remainder of complexity after the query's execution.
    /// </summary>
    public int Before { get; set; }

    /// <summary>
    ///     The remainder of complexity after the query's execution.
    /// </summary>
    public int After { get; set; }

    /// <summary>
    ///     The specific query's complexity.
    /// </summary>
    public int Query { get; set; }
}