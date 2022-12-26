using StreamDeckLib;
using StreamDeckLib.Messages;

namespace S2Cognition.Integrations.StreamDeck.Core;

public abstract class StreamDeckAction<T> : BaseStreamDeckActionWithSettingsModel<T>
    where T : StreamDeckModel
{
    protected readonly int _updateFrequencySeconds = 0;
    
    protected CancellationTokenSource? _cancellationTokenSource = null;

    protected StreamDeckAction(int updateFrequencySeconds = 0)
    {
        _updateFrequencySeconds = updateFrequencySeconds;
    }

    public abstract Task Initialize();

    public override async Task OnDidReceiveSettings(StreamDeckEventPayload args)
    {
        StopBackgroundTask();
        if(_updateFrequencySeconds > 0)
        {
            StartBackgroundTask(args);
        }

        await base.OnDidReceiveSettings(args);
    }

    public override async Task OnWillAppear(StreamDeckEventPayload args)
    {
        await base.OnWillAppear(args);

        await OnTick(args);

        StartBackgroundTask(args);
    }

    public override async Task OnKeyDown(StreamDeckEventPayload args)
    {
        await base.OnKeyDown(args);
    }

    public override async Task OnKeyUp(StreamDeckEventPayload args)
    {
        await base.OnKeyUp(args);
    }

    public override async Task OnWillDisappear(StreamDeckEventPayload args)
    {
        await base.OnWillDisappear(args);

        StopBackgroundTask();
    }

    public override async Task OnTitleParametersDidChange(StreamDeckEventPayload args)
    {
        await base.OnTitleParametersDidChange(args);
    }

    public override async Task OnDeviceDidConnect(StreamDeckEventPayload args)
    {
        await base.OnDeviceDidConnect(args);
    }

    public override async Task OnDeviceDidDisconnect(StreamDeckEventPayload args)
    {
        await base.OnDeviceDidDisconnect(args);
    }

    public override async Task OnApplicationDidLaunchAsync(StreamDeckEventPayload args)
    {
        await base.OnApplicationDidLaunchAsync(args);
    }

    public override async Task OnApplicationDidTerminateAsync(StreamDeckEventPayload args)
    {
        await base.OnApplicationDidTerminateAsync(args);
    }

    public override async Task OnDidReceiveGlobalSettings(StreamDeckEventPayload args)
    {
        await base.OnDidReceiveGlobalSettings(args);
    }

    public override async Task OnPropertyInspectorDidDisappear(StreamDeckEventPayload args)
    {
        await base.OnPropertyInspectorDidDisappear(args);
    }

    public override async Task OnPropertyInspectorDidAppear(StreamDeckEventPayload args)
    {
        await base.OnPropertyInspectorDidAppear(args);
    }

    public override async Task OnSendToPlugin(StreamDeckEventPayload args)
    {
        await base.OnSendToPlugin(args);
    }

    public virtual async Task OnTick(StreamDeckEventPayload args)
    {
        await Task.CompletedTask;
    }

    private void StartBackgroundTask(StreamDeckEventPayload args)
    {
        StopBackgroundTask();

        _cancellationTokenSource = new CancellationTokenSource();
        Task.Run(() => BackgroundTask(args, _cancellationTokenSource.Token));
    }

    private void StopBackgroundTask()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = null;
    }

    private async Task BackgroundTask(StreamDeckEventPayload args, CancellationToken ct)
    {
        if(_updateFrequencySeconds == 0)
        {
            StopBackgroundTask();
            return;
        }

        while(!ct.IsCancellationRequested)
        {
            // Cancellation exception is expected.
            await Task.Delay(TimeSpan.FromSeconds(_updateFrequencySeconds), ct);

            await OnTick(args);
        }
    }
}
