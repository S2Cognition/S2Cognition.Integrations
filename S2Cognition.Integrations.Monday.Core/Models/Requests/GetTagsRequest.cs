using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

public interface IGetTagsRequest : IMondayRequest
{
    ulong BoardId { get; set; }

    ITagOptions TagOptions { get; set; }
}

public interface IGetTagsResult : IMondayResult
{
    IEnumerable<Tag> Data { get; }
}
internal class GetTagsResult : MondayResult, IGetTagsResult
{
    public IEnumerable<Tag> Data { get; set; }

    public GetTagsResult(IEnumerable<Tag> data)
    {
        Data = data;
    }
}

public class GetTagsRequest : MondayRequest, IGetTagsRequest
{
    public ulong BoardId { get; set; }

    public ITagOptions TagOptions { get; set; }

    public GetTagsRequest(ulong boardId)
    {
        BoardId = boardId;

        TagOptions = new TagOptions(RequestMode.Default);
    }

    public GetTagsRequest(ulong boardId, RequestMode mode)
        : this(boardId)
    {
        TagOptions = new TagOptions(mode);
    }
}
