namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;

public class GetAlarmsStateResponse
{
    public IList<GetAlarmStateDetails>? Alarms { get; set; } = Array.Empty<GetAlarmStateDetails>();
}

public class GetAlarmStateDetails
{
    public string AlarmName { get; set; } = String.Empty;
    public string AlarmArn { get; set; } = string.Empty;
    public string AlarmDescription { get; set; } = String.Empty;
    public AlarmState State { get; set; } = AlarmState.Unknown;
}
