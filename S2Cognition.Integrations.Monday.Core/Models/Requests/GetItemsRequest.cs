using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

public interface IGetItemsRequest : IMondayRequest
{
    ulong? BoardId { get; set; }
    int Limit { get; set; }

    string? FilterColumnName { get; set; }
    string? FilterColumnValue { get; set; }
    StateFilter? FilterState { get; set; }

    IItemOptions? ItemOptions { get; set; }
}

public interface IGetItemsResult : IMondayResult
{
    IEnumerable<Item> Data { get; }
}
internal class GetItemsResult : MondayResult, IGetItemsResult
{
    public IEnumerable<Item> Data { get; set; }

    public GetItemsResult(IEnumerable<Item> data)
    {
        Data = data;
    }
}

public enum StateFilter
{
    None,
    Active
}

public class GetItemsRequest : MondayRequest, IGetItemsRequest
{
    public ulong? BoardId { get; set; }

    public int Limit { get; set; } = 100000;
    public string? FilterColumnName { get; set; } = null;
    public string? FilterColumnValue { get; set; } = null;
    public StateFilter? FilterState { get; set; } = null;

    public IItemOptions? ItemOptions { get; set; }

    public GetItemsRequest(ulong boardId)
    {
        BoardId = boardId;

        ItemOptions = new ItemOptions(RequestMode.Default);
        if (ItemOptions.BoardOptions != null)
        {
            ItemOptions.BoardOptions.IncludeBoardStateType = false;
            ItemOptions.BoardOptions.IncludeBoardFolderId = false;
        }
        if (ItemOptions.GroupOptions != null)
        {
            ItemOptions.GroupOptions.IncludeColor = false;
        }
        ItemOptions.IncludeSubscribers = false;
        ItemOptions.SubscriberOptions = null;
        ItemOptions.IncludeColumnValues = false;
        ItemOptions.ColumnValueOptions = null;
    }

    public GetItemsRequest(ulong boardId, RequestMode mode)
        : this(boardId)
    {
        ItemOptions = new ItemOptions(mode);
    }
}
