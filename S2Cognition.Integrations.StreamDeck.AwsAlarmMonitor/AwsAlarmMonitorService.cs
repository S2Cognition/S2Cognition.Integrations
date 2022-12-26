namespace S2Cognition.Integrations.StreamDeck.AwsAlarmMonitor;

public class AwsAlarmMonitorService
{
    private string? _awsAccessKeyId;
    private string? _awsAccessKeySecret;
    // private RegionEndpoint? _awsRegion;
    private string? _alarmName;
    private bool _initialized;

    public AwsAlarmMonitorService()
    {
        _initialized = false;
    }

    public async Task<ActionStates> QuerySystemStatus(AwsAlarmMonitorModel cfg)
    {
        if(!_initialized || (_alarmName == null))
            return ActionStates.Unknown;

        try
        {
            await Task.CompletedTask;
            throw new InvalidOperationException("TODO: implement.");

            // var cloudwatch = new Amazon.CloudWatch.AmazonCloudWatchClient(_awsAccessKeyId, _awsAccessKeySecret, _awsRegion);
            // var result = await cloudwatch.DescribeAlarmsAsync(new DescribeAlarmsRequest
            // {
            //     AlarmNames = new List<string> { _alarmName }
            // });

            // if((result == null)
            //     || (result.MetricAlarms == null)
            //     || (result.MetricAlarms.Count < 1))
            // {
            //     return ActionStates.Unknown;
            // }
            // 
            // return result.MetricAlarms[0].StateValue.Value switch
            // {
            //     "OK" => ActionStates.Up,
            //     "ALARM" => ActionStates.Down,
            //     "INSUFFICIENT_DATA" => ActionStates.Unknown,
            //     _ => ActionStates.Unknown
            // };
        }
        catch(Exception)
        {
            // TODO: log
            return ActionStates.Unknown;
        }
    }

    public void Initialize(AwsAlarmMonitorModel settingsModel)
    {
        _awsAccessKeyId = settingsModel.AwsAccessKeyId;
        _awsAccessKeySecret = settingsModel.AwsAccessKeySecret;
        _alarmName = settingsModel.AwsAlarmName;

        try
        {
            // TODO: _awsRegion = RegionEndpoint.GetBySystemName(settingsModel.AwsRegion);
        }
        catch(Exception)
        {
            // TODO: log
            // _awsRegion = null;
        }

        _initialized = !string.IsNullOrWhiteSpace(_awsAccessKeyId)
            && !string.IsNullOrWhiteSpace(_awsAccessKeySecret)
            && !string.IsNullOrWhiteSpace(_alarmName)
            // && (_awsRegion != null);
            && false;
    }
}
