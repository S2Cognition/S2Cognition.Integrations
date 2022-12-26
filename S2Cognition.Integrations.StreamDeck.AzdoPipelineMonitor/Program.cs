using S2Cognition.Integrations.StreamDeck.Core;

namespace S2Cognition.Integrations.StreamDeck.AzdoPipelineMonitor;

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
            PluginDescription = "Azure DevOps Pipeline Monitor",
            PluginName = "Azdo Pipeline Monitor",
            PluginIcon = "images/Plugin",
            PluginUrl = "http://www.google.com",
            PluginVersion = "1.0.0",
            CategoryName = "StreamDeckSharp Tools",
            CategoryIcon = "images/Category",
            ActionId = "com.StreamDeckSharp.streamdeck.AzdoPipelineMonitor",
            ActionIcon = "images/Action",
            ActionName = "Azdo Pipeline Monitor",
            ActionDescription = "Monitors Azure DevOps Pipelines",
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
