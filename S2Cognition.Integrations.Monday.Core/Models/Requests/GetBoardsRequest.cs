using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

public interface IGetBoardsRequest : IMondayRequest
{
    int Limit { get; set; }

    IBoardOptions BoardOptions { get; set; }
}

public interface IGetBoardsResult : IMondayResult
{
    IEnumerable<Board> Data { get; }
}

internal class GetBoardsResult : MondayResult, IGetBoardsResult
{
    public IEnumerable<Board> Data { get; set; }

    public GetBoardsResult(IEnumerable<Board> data)
    {
        Data = data;
    }
}

public class GetBoardsRequest : MondayRequest, IGetBoardsRequest
{
    public int Limit { get; set; } = 100000;

    public IBoardOptions BoardOptions { get; set; }

    public GetBoardsRequest()
    {
        BoardOptions = new BoardOptions(RequestMode.Default);
        BoardOptions.IncludeColumns = false;
        BoardOptions.ColumnOptions = null;
    }

    public GetBoardsRequest(RequestMode mode)
        : this()
    {
        BoardOptions = new BoardOptions(mode);
    }
}
