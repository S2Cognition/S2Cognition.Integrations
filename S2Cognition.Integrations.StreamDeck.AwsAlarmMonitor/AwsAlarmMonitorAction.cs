using S2Cognition.Integrations.StreamDeck.Core;
using StreamDeckLib.Messages;

namespace S2Cognition.Integrations.StreamDeck.AwsAlarmMonitor;

public partial class AwsAlarmMonitorAction : StreamDeckAction<AwsAlarmMonitorModel>
{
    private AwsAlarmMonitorService? _service;

    public override async Task Initialize()
    {
        _service = new AwsAlarmMonitorService();

        await Task.CompletedTask;
    }

    public override async Task OnKeyDown(StreamDeckEventPayload args)
    {
        await base.OnKeyDown(args);

        var url = $"https://{SettingsModel.AwsRegion}.console.aws.amazon.com/cloudwatch/home?region={SettingsModel.AwsRegion}#alarmsV2:alarm/{SettingsModel.AwsAlarmName}?";
        await Manager.OpenUrlAsync(args.context, url);
    }

    public override async Task OnTick(StreamDeckEventPayload args)
    {
        await base.OnTick(args);
        await Update(args);
    }

    private async Task Update(StreamDeckEventPayload args)
    {
        if(_service == null)
            throw new InvalidOperationException();

        _service.Initialize(SettingsModel);
        var status = await _service.QuerySystemStatus(SettingsModel);
        var state = status switch
        {
            ActionStates.Up => (int)ActionStates.Up,
            ActionStates.Down => (int)ActionStates.Down,
            _ => (int)ActionStates.Unknown
        };
        await Manager.SetStateAsync(args.context, state);
    }
}
