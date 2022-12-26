using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

public interface IGetBoardRequest : IMondayRequest
{
    ulong BoardId { get; set; }

    IBoardOptions BoardOptions { get; set; }
}

public interface IGetBoardResult : IMondayResult
{
    Board? Data { get; }
}

internal class GetBoardResult : MondayResult, IGetBoardResult
{
    public Board? Data { get; set; }

    public GetBoardResult(Board? data)
    {
        Data = data;
    }
}

public class GetBoardRequest : MondayRequest, IGetBoardRequest
{
    public ulong BoardId { get; set; }

    public IBoardOptions BoardOptions { get; set; }

    public GetBoardRequest(ulong boardId)
    {
        BoardId = boardId;

        BoardOptions = new BoardOptions(RequestMode.Default);
    }

    public GetBoardRequest(ulong boardId, RequestMode mode)
        : this(boardId)
    {
        BoardOptions = new BoardOptions(mode);
    }
}
