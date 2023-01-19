using S2Cognition.Integrations.StreamDeck.Core.Models;
using StreamDeckLib;

namespace S2Cognition.Integrations.StreamDeck.AzdoPipelineMonitor;

[ActionUuid(Uuid = "S2Cognition.Integrations.StreamDeck.AzdoPipelineMonitor")]
internal partial class AzdoPipelineMonitorAction : StreamDeckAction<AzdoPipelineMonitorModel>
{
    internal AzdoPipelineMonitorAction()
        : base(30)
    {
        Initialize().Wait();
    }
}