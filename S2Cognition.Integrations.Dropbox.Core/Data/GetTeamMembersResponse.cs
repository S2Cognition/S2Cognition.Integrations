namespace S2Cognition.Integrations.Dropbox.Core.Data;

public class GetTeamMembersResponse
{
    public ICollection<DropboxTeamMember> Entries { get; set; } = Array.Empty<DropboxTeamMember>();
}
