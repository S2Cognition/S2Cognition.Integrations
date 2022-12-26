using System.Collections.Generic;
namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

public class GetBoardsResponse
{
    public List<Board> Boards { get; set; }

    public GetBoardsResponse(List<Board> boards)
    {
        Boards = boards;
    }
}