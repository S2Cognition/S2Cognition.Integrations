using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

internal interface IGetGroupsRequest : IMondayRequest
{
    ulong BoardId { get; set; }

    IGroupOptions GroupOptions { get; set; }
}

internal interface IGetGroupsResult : IMondayResult
{
    IEnumerable<Group> Data { get; }
}

internal class GetGroupsResult : MondayResult, IGetGroupsResult
{
    public IEnumerable<Group> Data { get; set; }

    internal GetGroupsResult(IEnumerable<Group> data)
    {
        Data = data;
    }
}

internal class GetGroupsRequest : MondayRequest, IGetGroupsRequest
{
    public ulong BoardId { get; set; }

    public IGroupOptions GroupOptions { get; set; }

    internal GetGroupsRequest(ulong boardId)
    {
        BoardId = boardId;

        GroupOptions = new GroupOptions(RequestMode.Default);
    }

    internal GetGroupsRequest(ulong boardId, RequestMode mode)
    {
        GroupOptions = new GroupOptions(mode);
    }
}
