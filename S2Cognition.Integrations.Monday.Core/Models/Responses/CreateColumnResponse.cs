using Newtonsoft.Json;

namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

internal class CreateColumnResponse
{
    [JsonProperty("create_column")]
    public Column Column { get; set; }

    internal CreateColumnResponse(Column column)
    {
        Column = column;
    }
}