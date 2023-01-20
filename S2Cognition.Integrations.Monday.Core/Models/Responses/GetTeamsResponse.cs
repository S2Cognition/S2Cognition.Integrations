namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

internal class GetTeamsResponse
{
    public IEnumerable<Team> Teams { get; set; }

    internal GetTeamsResponse(IEnumerable<Team> teams)
    {
        Teams = teams;
    }
}