using S2Cognition.Integrations.StreamDeck.Core;
using StreamDeckLib;
namespace S2Cognition.Integrations.StreamDeck.AwsAlarmMonitor;

[ActionUuid(Uuid = "com.StreamDeckSharp.streamdeck.AwsAlarmMonitor")]
public partial class AwsAlarmMonitorAction : StreamDeckAction<AwsAlarmMonitorModel>
{
    public AwsAlarmMonitorAction()
        : base(30)
    {
        Initialize().Wait();
    }
}