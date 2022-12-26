using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

public interface IGetGroupsRequest : IMondayRequest
{
    ulong BoardId { get; set; }

    IGroupOptions GroupOptions { get; set; }
}

public interface IGetGroupsResult : IMondayResult
{
    IEnumerable<Group> Data { get; }
}

internal class GetGroupsResult : MondayResult, IGetGroupsResult
{
    public IEnumerable<Group> Data { get; set; }

    public GetGroupsResult(IEnumerable<Group> data)
    {
        Data = data;
    }
}

public class GetGroupsRequest : MondayRequest, IGetGroupsRequest
{
    public ulong BoardId { get; set; }

    public IGroupOptions GroupOptions { get; set; }

    public GetGroupsRequest(ulong boardId)
    {
        BoardId = boardId;

        GroupOptions = new GroupOptions(RequestMode.Default);
    }

    public GetGroupsRequest(ulong boardId, RequestMode mode)
    {
        GroupOptions = new GroupOptions(mode);
    }
}
