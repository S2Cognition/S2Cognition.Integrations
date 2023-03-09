namespace S2Cognition.Integrations.Dropbox.Core.Data;

public class DropboxShare
{
    public string? Id { get; set; }
    public bool IsFolder { get; set; } = false;
    public bool IsPublic { get; set; } = false;
    public string? Name { get; set; }
    public string? Path { get; set; }
    public string? Url { get; set; }
}