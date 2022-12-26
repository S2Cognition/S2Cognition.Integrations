using S2Cognition.Integrations.StreamDeck.Core;

namespace S2Cognition.Integrations.StreamDeck.AwsAlarmMonitor;

public class Program
{
    protected Program()
    {
    }

    public static async Task Main(string[] args)
    {
        var plugin = new StreamDeckPlugin<Program>(args);
        await plugin.Run(new StreamDeckConfig
        {
            PluginAuthor = "Eric J. Peters",
            PluginDescription = "Aws Alarm Monitor",
            PluginName = "Aws Alarm Monitor",
            PluginIcon = "images/Plugin",
            PluginUrl = "http://www.google.com",
            PluginVersion = "1.0.0",
            CategoryName = "StreamDeckSharp Tools",
            CategoryIcon = "images/Category",
            ActionId = "com.StreamDeckSharp.streamdeck.AwsAlarmMonitor",
            ActionIcon = "images/Action",
            ActionName = "Aws Alarm Monitor",
            ActionDescription = "Monitors Aws CloudWatch Alarms",
            UpdateFrequencySeconds = 30,
            StateIcons = new Dictionary<string, string> {
                { "Initializing" , "images/StateInit" },
                { "Down" , "images/StateDown"},
                { "Up" , "images/StateUp"},
                { "Unknown" , "images/StateUnknown"}
            },
            Configurations = new Dictionary<string, string> {
                { "Access Key Id", "AwsAccessKeyId" },
                { "Access Key Secret", "AwsAccessKeySecret"},
                { "Region", "AwsRegion"},
                { "Alarm Name", "AwsAlarmName"}
            },
            DistributionTool = "..\\DistributionTool.exe"
        });
    }
}
