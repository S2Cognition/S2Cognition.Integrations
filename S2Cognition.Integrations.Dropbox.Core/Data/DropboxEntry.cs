namespace S2Cognition.Integrations.Dropbox.Core.Data;

public class DropboxEntry
{
    public string Name { get; set; } = String.Empty;
    public string? Path { get; set; } = null;
    public bool IsFolder { get; set; } = false;
    public bool HasSubFolders { get; set; } = false;
}
