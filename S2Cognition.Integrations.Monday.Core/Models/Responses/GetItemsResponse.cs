namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

internal class GetItemsResponse
{
    public IEnumerable<Item> Items { get; set; }

    internal GetItemsResponse(IEnumerable<Item> items)
    {
        Items = items;
    }
}