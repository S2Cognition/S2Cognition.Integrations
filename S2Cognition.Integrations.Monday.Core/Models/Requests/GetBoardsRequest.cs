using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

internal interface IGetBoardsRequest : IMondayRequest
{
    int Limit { get; set; }

    IBoardOptions BoardOptions { get; set; }
}

internal interface IGetBoardsResult : IMondayResult
{
    IEnumerable<Board> Data { get; }
}

internal class GetBoardsResult : MondayResult, IGetBoardsResult
{
    public IEnumerable<Board> Data { get; set; }

    internal GetBoardsResult(IEnumerable<Board> data)
    {
        Data = data;
    }
}

internal class GetBoardsRequest : MondayRequest, IGetBoardsRequest
{
    public int Limit { get; set; } = 100000;

    public IBoardOptions BoardOptions { get; set; }

    internal GetBoardsRequest()
    {
        BoardOptions = new BoardOptions(RequestMode.Default);
        BoardOptions.IncludeColumns = false;
        BoardOptions.ColumnOptions = null;
    }

    internal GetBoardsRequest(RequestMode mode)
        : this()
    {
        BoardOptions = new BoardOptions(mode);
    }
}
