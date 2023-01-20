namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;

public class GetAlarmStateResponse
{
    public string Arn { get; set; } = String.Empty;
    public AlarmState State { get; set; } = AlarmState.Unknown;
}
