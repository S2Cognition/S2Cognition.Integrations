using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

internal interface IGetTeamsRequest : IMondayRequest
{
    ITeamOptions TeamOptions { get; set; }
}

internal interface IGetTeamsResult : IMondayResult
{
    IEnumerable<Team> Data { get; }
}
internal class GetTeamsResult : MondayResult, IGetTeamsResult
{
    public IEnumerable<Team> Data { get; set; }

    internal GetTeamsResult(IEnumerable<Team> data)
    {
        Data = data;
    }
}

internal class GetTeamsRequest : MondayRequest, IGetTeamsRequest
{
    public ITeamOptions TeamOptions { get; set; }

    internal GetTeamsRequest()
    {
        TeamOptions = new TeamOptions(RequestMode.Default);
    }

    internal GetTeamsRequest(RequestMode mode)
        : this()
    {
        TeamOptions = new TeamOptions(mode);
    }
}