namespace S2Cognition.Integrations.Zoom.Core.Data;

public class PagedResponse
{
    public int PageSize { get; set; } = 0;
    public int TotalRecords { get; set; } = 0;
    public string? NextPageToken { get; set; } = null;

    protected PagedResponse()
    { 
    }
}
