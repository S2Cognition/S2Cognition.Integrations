namespace S2Cognition.Integrations.StreamDeck.AzdoPipelineMonitor;

internal class AzdoPipelineMonitorService
{
    private bool _initialized = false;

    internal AzdoPipelineMonitorService()
    {
        _initialized = false;
    }

    public void Initialize(AzdoPipelineMonitorModel settingsModel)
    {
        if(_initialized)
            return;

        _initialized = true;
    }
}