using Newtonsoft.Json;

namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

internal class CreateGroupResponse
{
    [JsonProperty("create_group")]
    public Group Group { get; set; }

    internal CreateGroupResponse(Group group)
    {
        Group = group;
    }
}