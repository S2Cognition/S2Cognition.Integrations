namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

internal class GetComplexityResponse
{
    public Complexity Complexity { get; set; }

    internal GetComplexityResponse(Complexity complexity)
    {
        Complexity = complexity;
    }
}