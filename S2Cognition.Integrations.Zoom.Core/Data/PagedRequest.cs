using S2Cognition.Integrations.Core.Data;

namespace S2Cognition.Integrations.Zoom.Core.Data;

public class PagedRequest
{
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
    public string? NextPageToken { get; set; } = null;

    protected PagedRequest()
    { 
    }
}
