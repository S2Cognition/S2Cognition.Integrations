using Newtonsoft.Json;

namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

internal class CreateUpdateResponse
{
    [JsonProperty("create_update")]
    public Update Update { get; set; }

    internal CreateUpdateResponse(Update update)
    {
        Update = update;
    }
}