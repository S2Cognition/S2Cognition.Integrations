namespace S2Cognition.Integrations.StreamDeck.Core.Models;

internal class StreamDeckConfig
{
    internal StreamDeckConfig()
    {
    }

    public string? PluginAuthor { get; set; }
    public string? PluginDescription { get; set; }
    public string? PluginName { get; set; }
    public string? PluginIcon { get; set; }
    public string? PluginUrl { get; set; }
    public string? PluginVersion { get; set; }
    public string? CategoryName { get; set; }
    public string? CategoryIcon { get; set; }
    public string? ActionId { get; set; }
    public string? ActionIcon { get; set; }
    public string? ActionName { get; set; }
    public string? ActionDescription { get; set; }

    public IDictionary<string, string>? StateIcons { get; set; }
    public IDictionary<string, string>? Configurations { get; set; }

    public int? UpdateFrequencySeconds { get; set; }
    public string? DistributionTool { get; set; }
}
