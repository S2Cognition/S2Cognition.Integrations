namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;

public class GetAlarmsStateRequest
{
    public List<string>? AlarmNames { get; set; } = null;
    public string? AlarmNamePrefix { get; set; } = null;
    public string? StateValue { get; set; } = null;
    public int MaxRecords { get; set; } = 99;
}