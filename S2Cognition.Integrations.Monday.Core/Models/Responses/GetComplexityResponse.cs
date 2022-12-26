namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

public class GetComplexityResponse
{
    public Complexity Complexity { get; set; }

    public GetComplexityResponse(Complexity complexity)
    {
        Complexity = complexity;
    }
}