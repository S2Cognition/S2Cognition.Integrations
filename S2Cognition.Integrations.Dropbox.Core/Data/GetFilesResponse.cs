namespace S2Cognition.Integrations.Dropbox.Core.Data;

public class GetFilesResponse 
{
    public ICollection<DropboxEntry> Entries { get; set; } = Array.Empty<DropboxEntry>();
}
