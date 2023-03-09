namespace S2Cognition.Integrations.Dropbox.Core.Data;

public class GetFolderResponse
{
    public ICollection<DropboxEntry> Entries { get; set; } = Array.Empty<DropboxEntry>();
}
