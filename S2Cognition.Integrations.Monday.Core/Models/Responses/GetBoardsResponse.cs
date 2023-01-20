namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

internal class GetBoardsResponse
{
    public List<Board> Boards { get; set; }

    internal GetBoardsResponse(List<Board> boards)
    {
        Boards = boards;
    }
}