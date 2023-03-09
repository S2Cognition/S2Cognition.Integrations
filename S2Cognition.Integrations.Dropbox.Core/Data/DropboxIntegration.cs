namespace S2Cognition.Integrations.Dropbox.Core.Data;

public interface IDropboxIntegration : IIntegration<DropboxConfiguration>
{
    Task<GetTeamMembersResponse> GetTeamMembers(GetTeamMembersRequest req);
    Task<GetFolderResponse> GetFolder(GetFolderRequest req);
    Task<GetFilesResponse> GetFiles(GetFilesRequest req);
    Task<GetFileResponse> GetFile(GetFileRequest req);
    Task<GetSharedLinksResponse> GetSharedLinks(GetSharedLinksRequest req);
}

