using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

public interface IGetUserRequest : IMondayRequest
{
    ulong UserId { get; set; }

    IUserOptions UserOptions { get; set; }
}

public interface IGetUserResult : IMondayResult
{
    User? Data { get; }
}

internal class GetUserResult : MondayResult, IGetUserResult
{
    public User? Data { get; set; }

    public GetUserResult(User? data)
    {
        Data = data;
    }
}

public class GetUserRequest : MondayRequest, IGetUserRequest
{
    public ulong UserId { get; set; }

    public IUserOptions UserOptions { get; set; }

    public GetUserRequest(ulong userId)
    {
        UserId = userId;

        UserOptions = new UserOptions(RequestMode.Default);
    }

    public GetUserRequest(ulong userId, RequestMode mode)
        : this(userId)
    {
        UserOptions = new UserOptions(mode);
    }
}
