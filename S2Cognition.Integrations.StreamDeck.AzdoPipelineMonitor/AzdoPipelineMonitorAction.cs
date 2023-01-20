using S2Cognition.Integrations.StreamDeck.Core.Models;

namespace S2Cognition.Integrations.StreamDeck.AzdoPipelineMonitor;

internal partial class AzdoPipelineMonitorAction : StreamDeckAction<AzdoPipelineMonitorModel>
{
    private AzdoPipelineMonitorService? _service;

    public override async Task Initialize()
    {
        _service = new AzdoPipelineMonitorService();

        await Task.CompletedTask;
    }
}