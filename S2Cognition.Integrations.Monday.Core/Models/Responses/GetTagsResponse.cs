namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

public class GetTagsResponse
{
    public IEnumerable<Tag> Tags { get; set; }

    public GetTagsResponse(IEnumerable<Tag> tags)
    {
        Tags = tags;
    }
}