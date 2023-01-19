using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

internal interface IGetTagsRequest : IMondayRequest
{
    ulong BoardId { get; set; }

    ITagOptions TagOptions { get; set; }
}

internal interface IGetTagsResult : IMondayResult
{
    IEnumerable<Tag> Data { get; }
}
internal class GetTagsResult : MondayResult, IGetTagsResult
{
    public IEnumerable<Tag> Data { get; set; }

    internal GetTagsResult(IEnumerable<Tag> data)
    {
        Data = data;
    }
}

internal class GetTagsRequest : MondayRequest, IGetTagsRequest
{
    public ulong BoardId { get; set; }

    public ITagOptions TagOptions { get; set; }

    internal GetTagsRequest(ulong boardId)
    {
        BoardId = boardId;

        TagOptions = new TagOptions(RequestMode.Default);
    }

    internal GetTagsRequest(ulong boardId, RequestMode mode)
        : this(boardId)
    {
        TagOptions = new TagOptions(mode);
    }
}
