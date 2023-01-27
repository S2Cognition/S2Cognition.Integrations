using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

internal interface IGetBoardRequest : IMondayRequest
{
    ulong BoardId { get; set; }

    IBoardOptions BoardOptions { get; set; }
}

internal interface IGetBoardResult : IMondayResult
{
    Board? Data { get; }
}

internal class GetBoardResult : MondayResult, IGetBoardResult
{
    public Board? Data { get; set; }

    internal GetBoardResult(Board? data)
    {
        Data = data;
    }
}

internal class GetBoardRequest : MondayRequest, IGetBoardRequest
{
    public ulong BoardId { get; set; }

    public IBoardOptions BoardOptions { get; set; }

    internal GetBoardRequest(ulong boardId)
    {
        BoardId = boardId;

        BoardOptions = new BoardOptions(RequestMode.Default);
    }

    internal GetBoardRequest(ulong boardId, RequestMode mode)
        : this(boardId)
    {
        BoardOptions = new BoardOptions(mode);
    }
}
