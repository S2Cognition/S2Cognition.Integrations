using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

internal interface IGetUserRequest : IMondayRequest
{
    ulong UserId { get; set; }

    IUserOptions UserOptions { get; set; }
}

internal interface IGetUserResult : IMondayResult
{
    User? Data { get; }
}

internal class GetUserResult : MondayResult, IGetUserResult
{
    public User? Data { get; set; }

    internal GetUserResult(User? data)
    {
        Data = data;
    }
}

internal class GetUserRequest : MondayRequest, IGetUserRequest
{
    public ulong UserId { get; set; }

    public IUserOptions UserOptions { get; set; }

    internal GetUserRequest(ulong userId)
    {
        UserId = userId;

        UserOptions = new UserOptions(RequestMode.Default);
    }

    internal GetUserRequest(ulong userId, RequestMode mode)
        : this(userId)
    {
        UserOptions = new UserOptions(mode);
    }
}
