using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

public interface IGetTeamsRequest : IMondayRequest
{
    ITeamOptions TeamOptions { get; set; }
}

public interface IGetTeamsResult : IMondayResult
{
    IEnumerable<Team> Data { get; }
}
internal class GetTeamsResult : MondayResult, IGetTeamsResult
{
    public IEnumerable<Team> Data { get; set; }

    public GetTeamsResult(IEnumerable<Team> data)
    {
        Data = data;
    }
}

public class GetTeamsRequest : MondayRequest, IGetTeamsRequest
{
    public ITeamOptions TeamOptions { get; set; }

    public GetTeamsRequest()
    {
        TeamOptions = new TeamOptions(RequestMode.Default);
    }

    public GetTeamsRequest(RequestMode mode)
        : this()
    {
        TeamOptions = new TeamOptions(mode);
    }
}