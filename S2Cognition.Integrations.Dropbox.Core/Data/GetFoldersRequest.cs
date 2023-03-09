namespace S2Cognition.Integrations.Dropbox.Core.Data;

public class GetFolderRequest 
{
    public bool FoldersOnly { get; set; } = false;
    public string Path { get; set; } = String.Empty;
}
