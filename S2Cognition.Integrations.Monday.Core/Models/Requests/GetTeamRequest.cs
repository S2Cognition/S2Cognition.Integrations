using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

public interface IGetTeamRequest : IMondayRequest
{
    ulong TeamId { get; set; }

    ITeamOptions TeamOptions { get; set; }
}

public interface IGetTeamResult : IMondayResult
{
    Team? Data { get; }
}
internal class GetTeamResult : MondayResult, IGetTeamResult
{
    public Team? Data { get; set; }

    public GetTeamResult(Team? data)
    {
        Data = data;
    }
}

public class GetTeamRequest : MondayRequest, IGetTeamRequest
{
    public ulong TeamId { get; set; }

    public ITeamOptions TeamOptions { get; set; }

    public GetTeamRequest(ulong teamId)
    {
        TeamId = teamId;

        TeamOptions = new TeamOptions(RequestMode.Default);
    }

    public GetTeamRequest(ulong teamId, RequestMode mode)
        : this(teamId)
    {
        TeamOptions = new TeamOptions(mode);
    }
}
