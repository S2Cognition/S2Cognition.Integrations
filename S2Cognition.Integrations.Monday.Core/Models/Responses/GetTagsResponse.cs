namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

internal class GetTagsResponse
{
    public IEnumerable<Tag> Tags { get; set; }

    internal GetTagsResponse(IEnumerable<Tag> tags)
    {
        Tags = tags;
    }
}