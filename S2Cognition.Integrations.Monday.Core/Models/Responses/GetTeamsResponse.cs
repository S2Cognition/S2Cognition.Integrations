using System.Collections.Generic;
namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

public class GetTeamsResponse
{
    public IEnumerable<Team> Teams { get; set; }

    public GetTeamsResponse(IEnumerable<Team> teams)
    {
        Teams = teams;
    }
}