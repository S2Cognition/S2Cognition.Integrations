using Newtonsoft.Json;

namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

internal class CreateBoardResponse
{
    [JsonProperty("create_board")]
    public Board Board { get; set; }

    internal CreateBoardResponse(Board baord)
    {
        Board = baord;
    }
}
