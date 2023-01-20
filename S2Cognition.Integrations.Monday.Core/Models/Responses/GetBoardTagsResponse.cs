namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

internal class GetBoardTagsResponse
{
    public IEnumerable<Board> Boards { get; set; }

    internal GetBoardTagsResponse(IEnumerable<Board> boards)
    {
        Boards = boards;
    }
}