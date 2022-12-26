namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

public class GetBoardTagsResponse
{
    public IEnumerable<Board> Boards { get; set; }

    public GetBoardTagsResponse(IEnumerable<Board> boards)
    {
        Boards = boards;
    }
}