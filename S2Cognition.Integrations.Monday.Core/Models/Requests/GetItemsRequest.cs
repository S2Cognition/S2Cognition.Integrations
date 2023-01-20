using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

internal interface IGetItemsRequest : IMondayRequest
{
    ulong? BoardId { get; set; }
    int Limit { get; set; }

    string? FilterColumnName { get; set; }
    string? FilterColumnValue { get; set; }
    StateFilter? FilterState { get; set; }

    IItemOptions? ItemOptions { get; set; }
}

internal interface IGetItemsResult : IMondayResult
{
    IEnumerable<Item> Data { get; }
}
internal class GetItemsResult : MondayResult, IGetItemsResult
{
    public IEnumerable<Item> Data { get; set; }

    internal GetItemsResult(IEnumerable<Item> data)
    {
        Data = data;
    }
}

internal enum StateFilter
{
    None,
    Active
}

internal class GetItemsRequest : MondayRequest, IGetItemsRequest
{
    public ulong? BoardId { get; set; }

    public int Limit { get; set; } = 100000;
    public string? FilterColumnName { get; set; } = null;
    public string? FilterColumnValue { get; set; } = null;
    public StateFilter? FilterState { get; set; } = null;

    public IItemOptions? ItemOptions { get; set; }

    internal GetItemsRequest(ulong boardId)
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

    internal GetItemsRequest(ulong boardId, RequestMode mode)
        : this(boardId)
    {
        ItemOptions = new ItemOptions(mode);
    }
}
