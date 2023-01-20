using S2Cognition.Integrations.StreamDeck.Core.Models;
using StreamDeckLib;
namespace S2Cognition.Integrations.StreamDeck.AwsAlarmMonitor;

[ActionUuid(Uuid = "com.StreamDeckSharp.streamdeck.AwsAlarmMonitor")]
internal partial class AwsAlarmMonitorAction : StreamDeckAction<AwsAlarmMonitorModel>
{
    internal AwsAlarmMonitorAction()
        : base(30)
    {
        Initialize().Wait();
    }
}