namespace S2Cognition.Integrations.NetSuite.Core.Data;

public class GetCustomRecordRequest
{
    private readonly long _internalId;
    public string InternalId => $"{_internalId}";

    public GetCustomRecordRequest(long internalId)
    {
        _internalId = internalId;
    }
}