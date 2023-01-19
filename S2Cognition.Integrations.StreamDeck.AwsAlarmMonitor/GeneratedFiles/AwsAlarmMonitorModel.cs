using S2Cognition.Integrations.StreamDeck.Core.Models;

namespace S2Cognition.Integrations.StreamDeck.AwsAlarmMonitor;

internal class AwsAlarmMonitorModel : StreamDeckModel
{
    public string? AwsAccessKeyId { get; set; }
    public string? AwsAccessKeySecret { get; set; }
    public string? AwsRegion { get; set; }
    public string? AwsAlarmName { get; set; }
}