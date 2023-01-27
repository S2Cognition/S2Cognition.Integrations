namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

internal class GetBoardItemsResponse
{
    public IEnumerable<Board> Boards { get; set; }

    internal GetBoardItemsResponse(IEnumerable<Board> boards)
    {
        Boards = boards;
    }
}