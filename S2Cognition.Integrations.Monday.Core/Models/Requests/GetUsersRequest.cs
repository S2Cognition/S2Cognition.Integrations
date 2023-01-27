using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

internal interface IGetUsersRequest : IMondayRequest
{
    UserAccessTypes? UserAccessType { get; set; }

    IUserOptions UserOptions { get; set; }
}

internal interface IGetUsersResult : IMondayResult
{
    IEnumerable<User> Data { get; }
}

internal class GetUsersResult : MondayResult, IGetUsersResult
{
    public IEnumerable<User> Data { get; set; }

    internal GetUsersResult(IEnumerable<User> data)
    {
        Data = data;
    }
}

internal class GetUsersRequest : MondayRequest, IGetUsersRequest
{
    public UserAccessTypes? UserAccessType { get; set; } = null;

    public IUserOptions UserOptions { get; set; }

    internal GetUsersRequest()
    {
        UserOptions = new UserOptions(RequestMode.Default);
    }

    internal GetUsersRequest(RequestMode mode)
    {
        UserOptions = new UserOptions(mode);
    }
}
