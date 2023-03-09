namespace S2Cognition.Integrations.Dropbox.Core.Data;

public class GetSharedLinksResponse 
{
    public ICollection<DropboxShare> Entries { get; set; } = Array.Empty<DropboxShare>();
}
