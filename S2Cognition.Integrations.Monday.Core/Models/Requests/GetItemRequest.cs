using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

internal interface IGetItemRequest : IMondayRequest
{
    ulong ItemId { get; set; }

    IItemOptions ItemOptions { get; set; }
}

internal interface IGetItemResult : IMondayResult
{
    Item? Data { get; }
}

internal class GetItemResult : MondayResult, IGetItemResult
{
    public Item? Data { get; set; }

    internal GetItemResult(Item? data)
    {
        Data = data;
    }
}

internal class GetItemRequest : MondayRequest, IGetItemRequest
{
    public ulong ItemId { get; set; }

    public IItemOptions ItemOptions { get; set; }

    internal GetItemRequest(ulong itemId)
    {
        ItemId = itemId;

        ItemOptions = new ItemOptions(RequestMode.Default);
    }

    internal GetItemRequest(ulong itemId, RequestMode mode)
        : this(itemId)
    {
        ItemOptions = new ItemOptions(mode);
    }
}
