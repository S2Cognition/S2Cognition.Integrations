namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

public class GetItemsResponse
{
    public IEnumerable<Item> Items { get; set; }

    public GetItemsResponse(IEnumerable<Item> items)
    {
        Items = items;
    }
}