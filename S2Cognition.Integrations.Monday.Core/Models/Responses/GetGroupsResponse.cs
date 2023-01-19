namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

internal class GetGroupsResponse
{
    public IEnumerable<Board> Boards { get; set; }

    internal GetGroupsResponse(IEnumerable<Board> boards)
    {
        Boards = boards;
    }
}