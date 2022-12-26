using Newtonsoft.Json;

namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

public class CreateColumnResponse
{
    [JsonProperty("create_column")]
    public Column Column { get; set; }

    public CreateColumnResponse(Column column)
    {
        Column = column;
    }
}