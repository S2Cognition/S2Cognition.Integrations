using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

public interface IGetTagRequest : IMondayRequest
{
    ulong TagId { get; set; }

    ITagOptions TagOptions { get; set; }
}

public interface IGetTagResult : IMondayResult
{
    Tag? Data { get; }
}
internal class GetTagResult : MondayResult, IGetTagResult
{
    public Tag? Data { get; set; }

    public GetTagResult(Tag? data)
    {
        Data = data;
    }
}

public class GetTagRequest : MondayRequest, IGetTagRequest
{
    public ulong TagId { get; set; }

    public ITagOptions TagOptions { get; set; }

    public GetTagRequest(ulong tagId)
    {
        TagId = tagId;

        TagOptions = new TagOptions(RequestMode.Default);
    }

    public GetTagRequest(ulong tagId, RequestMode mode)
        : this(tagId)
    {
        TagOptions = new TagOptions(mode);
    }
}
