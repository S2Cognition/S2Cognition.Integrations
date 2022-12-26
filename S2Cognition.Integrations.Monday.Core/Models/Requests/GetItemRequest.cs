using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

public interface IGetItemRequest : IMondayRequest
{
    ulong ItemId { get; set; }

    IItemOptions ItemOptions { get; set; }
}

public interface IGetItemResult : IMondayResult
{
    Item? Data { get; }
}

internal class GetItemResult : MondayResult, IGetItemResult
{
    public Item? Data { get; set; }

    public GetItemResult(Item? data)
    {
        Data = data;
    }
}

public class GetItemRequest : MondayRequest, IGetItemRequest
{
    public ulong ItemId { get; set; }

    public IItemOptions ItemOptions { get; set; }

    public GetItemRequest(ulong itemId)
    {
        ItemId = itemId;

        ItemOptions = new ItemOptions(RequestMode.Default);
    }

    public GetItemRequest(ulong itemId, RequestMode mode)
        : this(itemId)
    {
        ItemOptions = new ItemOptions(mode);
    }
}
