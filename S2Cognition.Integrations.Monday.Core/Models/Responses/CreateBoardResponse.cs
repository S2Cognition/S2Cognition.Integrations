using Newtonsoft.Json;

namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

public class CreateBoardResponse
{
    [JsonProperty("create_board")]
    public Board Board { get; set; }

    public CreateBoardResponse(Board baord)
    {
        Board = baord;
    }
}
