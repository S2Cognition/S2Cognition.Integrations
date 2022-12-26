using S2Cognition.Integrations.StreamDeck.Core;
using StreamDeckLib;

namespace S2Cognition.Integrations.StreamDeck.AzdoPipelineMonitor;

[ActionUuid(Uuid = "S2Cognition.Integrations.StreamDeck.AzdoPipelineMonitor")]
public partial class AzdoPipelineMonitorAction : StreamDeckAction<AzdoPipelineMonitorModel>
{
    public AzdoPipelineMonitorAction()
        : base(30)
    {
        Initialize().Wait();
    }
}