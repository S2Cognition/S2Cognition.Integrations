using Newtonsoft.Json;

namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

internal class CreateItemResponse
{
    [JsonProperty("create_item")]
    public Item Item { get; set; }

    internal CreateItemResponse(Item item)
    {
        Item = item;
    }
}