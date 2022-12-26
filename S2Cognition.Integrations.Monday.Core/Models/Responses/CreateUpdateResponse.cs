using Newtonsoft.Json;

namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

public class CreateUpdateResponse
{
    [JsonProperty("create_update")]
    public Update Update { get; set; }

    public CreateUpdateResponse(Update update)
    {
        Update = update;
    }
}