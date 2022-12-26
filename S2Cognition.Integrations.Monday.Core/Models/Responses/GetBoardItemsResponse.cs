namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

public class GetBoardItemsResponse
{
    public IEnumerable<Board> Boards { get; set; }

    public GetBoardItemsResponse(IEnumerable<Board> boards)
    {
        Boards = boards;
    }
}