using Newtonsoft.Json;

namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

public class CreateTagResponse
{
    [JsonProperty("create_or_get_tag")]
    public Tag Tag { get; set; }

    public CreateTagResponse(Tag tag)
    {
        Tag = tag;
    }
}