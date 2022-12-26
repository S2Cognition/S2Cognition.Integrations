namespace S2Cognition.Integrations.Monday.Core.Models.Mutations;

/// <summary>
///     Create a new board mutation.
/// </summary>
public class CreateBoard
{
    /// <summary>
    ///     The board's name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     The board's kind. (public / private / share)
    /// </summary>
    public BoardAccessTypes BoardAccessType { get; set; }

    public CreateBoard(string name, BoardAccessTypes boardAccessType)
    {
        Name = name;
        BoardAccessType = boardAccessType;
    }
}