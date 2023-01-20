using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

internal interface IGetTeamRequest : IMondayRequest
{
    ulong TeamId { get; set; }

    ITeamOptions TeamOptions { get; set; }
}

internal interface IGetTeamResult : IMondayResult
{
    Team? Data { get; }
}

internal class GetTeamResult : MondayResult, IGetTeamResult
{
    public Team? Data { get; set; }

    internal GetTeamResult(Team? data)
    {
        Data = data;
    }
}

internal class GetTeamRequest : MondayRequest, IGetTeamRequest
{
    public ulong TeamId { get; set; }

    public ITeamOptions TeamOptions { get; set; }

    internal GetTeamRequest(ulong teamId)
    {
        TeamId = teamId;

        TeamOptions = new TeamOptions(RequestMode.Default);
    }

    internal GetTeamRequest(ulong teamId, RequestMode mode)
        : this(teamId)
    {
        TeamOptions = new TeamOptions(mode);
    }
}
