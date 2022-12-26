using Newtonsoft.Json;

namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

public class CreateGroupResponse
{
    [JsonProperty("create_group")]
    public Group Group { get; set; }

    public CreateGroupResponse(Group group)
    {
        Group = group;
    }
}