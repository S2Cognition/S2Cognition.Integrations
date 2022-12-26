using Newtonsoft.Json;

namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

public class CreateItemResponse
{
    [JsonProperty("create_item")]
    public Item Item { get; set; }

    public CreateItemResponse(Item item)
    {
        Item = item;
    }
}