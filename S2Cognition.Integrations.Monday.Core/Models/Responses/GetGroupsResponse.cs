namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

public class GetGroupsResponse
{
    public IEnumerable<Board> Boards { get; set; }

    public GetGroupsResponse(IEnumerable<Board> boards)
    {
        Boards = boards;
    }
}