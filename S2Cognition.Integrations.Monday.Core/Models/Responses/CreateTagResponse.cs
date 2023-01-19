using Newtonsoft.Json;

namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

internal class CreateTagResponse
{
    [JsonProperty("create_or_get_tag")]
    public Tag Tag { get; set; }

    internal CreateTagResponse(Tag tag)
    {
        Tag = tag;
    }
}