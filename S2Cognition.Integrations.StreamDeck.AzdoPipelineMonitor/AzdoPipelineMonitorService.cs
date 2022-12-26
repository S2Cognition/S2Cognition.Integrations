namespace S2Cognition.Integrations.StreamDeck.AzdoPipelineMonitor;

public class AzdoPipelineMonitorService
{
    private bool _initialized = false;

    public AzdoPipelineMonitorService()
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