namespace S2Cognition.Integrations.Dropbox.Core.Data;

public class DropboxConfiguration : Configuration
{
    public string? AccessToken { get; set; }
    public string? LoginEmailAddress { get; set; }

    public DropboxConfiguration(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }
}
