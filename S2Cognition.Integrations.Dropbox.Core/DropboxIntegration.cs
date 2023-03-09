namespace S2Cognition.Integrations.Dropbox.Core;

public class DropboxIntegration : Integration<DropboxConfiguration>, IDropboxIntegration
{
    private string? _teamMemberId = null;

    internal DropboxIntegration(IServiceProvider serviceProvider)
        : base(serviceProvider)
	{
    }

    private async Task<string> GetConfiguredTeamMemberId()
    {
        if ((Configuration.LoginEmailAddress == null)
            || String.IsNullOrWhiteSpace(Configuration.LoginEmailAddress))
        {
            throw new InvalidOperationException(nameof(DropboxConfiguration.LoginEmailAddress));
        }

        var members = await GetTeamMembers(new GetTeamMembersRequest());
        var teamMember = members.Entries.SingleOrDefault(_ => _.Email?.Trim()?.ToUpper() == Configuration.LoginEmailAddress.Trim().ToUpper())
            ?? throw new InvalidOperationException();

        return teamMember.Id ?? throw new InvalidOperationException();
    }

    public virtual async Task<GetTeamMembersResponse> GetTeamMembers(GetTeamMembersRequest req)
	{
        if ((Configuration.AccessToken == null)
            || String.IsNullOrWhiteSpace(Configuration.AccessToken))
        {
            throw new InvalidOperationException(nameof(DropboxConfiguration.AccessToken));
        }

        var client = _serviceProvider.GetRequiredService<IDropboxNativeClient>();

        var entries = new List<DropboxTeamMember>();
        MembersListV2Result teamMembers;
        string? cursor = null;
        do
        {
            teamMembers = await client.GetTeamMembers(Configuration.AccessToken, cursor);
            entries.AddRange(teamMembers.Members.Select(member => new DropboxTeamMember
            {
                Id = member.Profile.TeamMemberId,
                Email = member.Profile.Email,
                Name = member.Profile.Name.DisplayName
            }));
            cursor = teamMembers.Cursor;
        }
        while (teamMembers.HasMore && !String.IsNullOrWhiteSpace(cursor));

        return new GetTeamMembersResponse
        {
            Entries = entries
        };
	}

    public async Task<GetFolderResponse> GetFolder(GetFolderRequest req)
    {
        if((Configuration.AccessToken == null)
            || String.IsNullOrWhiteSpace(Configuration.AccessToken))
        {
            throw new InvalidOperationException(nameof(DropboxConfiguration.AccessToken));
        }

        _teamMemberId ??= await GetConfiguredTeamMemberId();

        var client = _serviceProvider.GetRequiredService<IDropboxNativeClient>();
        var args = new ListFolderArg(req.Path);
        var entries = new List<DropboxEntry>();
        ListFolderResult folders;
        string? cursor = null;
        do
        {
            folders = await client.GetFolder(Configuration.AccessToken, _teamMemberId, args, cursor);
            foreach (var folder in folders.Entries)
            {
                if (folder.IsFolder)
                {
                    entries.Add(new DropboxEntry
                    {
                        IsFolder = true,
                        Name = folder.Name,
                        Path = folder.PathLower,
                        HasSubFolders = !String.IsNullOrWhiteSpace(folder.PathLower)
                    });
                }
                else if (folder.IsFile && !req.FoldersOnly)
                {
                    entries.Add(new DropboxEntry
                    {
                        IsFolder = false,
                        Name = folder.Name,
                        Path = folder.PathLower,
                        HasSubFolders = false
                    });
                }
            }

            cursor = folders.Cursor;
        }
        while (folders.HasMore && !String.IsNullOrWhiteSpace(cursor));

        return new GetFolderResponse { 
            Entries = entries
        };
    }

    public async Task<GetFilesResponse> GetFiles(GetFilesRequest req)
    {
        if ((Configuration.AccessToken == null)
            || String.IsNullOrWhiteSpace(Configuration.AccessToken))
        {
            throw new InvalidOperationException(nameof(DropboxConfiguration.AccessToken));
        }

        if (String.IsNullOrWhiteSpace(req.Path))
            throw new ArgumentException(nameof(GetFilesRequest.Path));

        _teamMemberId ??= await GetConfiguredTeamMemberId();

        var entries = new List<DropboxEntry>();
        var client = _serviceProvider.GetRequiredService<IDropboxNativeClient>();
        var args = new SearchV2Arg(req.Path);
        SearchV2Result files;
        string? cursor = null;
        do
        {
            files = await client.GetFiles(Configuration.AccessToken, _teamMemberId, args, cursor);

            entries.AddRange(files.Matches
                .Where(match => match.Metadata.AsMetadata.Value.IsFile)
                .Select(match => new DropboxEntry
                {
                    IsFolder = false,
                    Name = match.Metadata.AsMetadata.Value.Name,
                    Path = match.Metadata.AsMetadata.Value.PathLower,
                    HasSubFolders = false
                }));

            cursor = files.Cursor;
        }
        while (files.HasMore && !String.IsNullOrWhiteSpace(cursor));

        return new GetFilesResponse
        {
            Entries = entries
        };
    }

    public async Task<GetFileResponse> GetFile(GetFileRequest req)
    {
        if ((Configuration.AccessToken == null)
            || String.IsNullOrWhiteSpace(Configuration.AccessToken))
        {
            throw new InvalidOperationException(nameof(DropboxConfiguration.AccessToken));
        }

        _teamMemberId ??= await GetConfiguredTeamMemberId();

        var client = _serviceProvider.GetRequiredService<IDropboxNativeClient>();
        var args = new DownloadArg();
        var download = await client.GetFile(Configuration.AccessToken, _teamMemberId, args);

        return new GetFileResponse
        {
            Data = await download.GetContentAsByteArrayAsync()
        };
    }

    public async Task<GetSharedLinksResponse> GetSharedLinks(GetSharedLinksRequest req)
	{
        if ((Configuration.AccessToken == null)
            || String.IsNullOrWhiteSpace(Configuration.AccessToken))
        {
            throw new InvalidOperationException(nameof(DropboxConfiguration.AccessToken));
        }

        _teamMemberId ??= await GetConfiguredTeamMemberId();

        var entries = new List<DropboxShare>();
        var client = _serviceProvider.GetRequiredService<IDropboxNativeClient>();
        ListSharedLinksResult shares;
        string? cursor = null;
        do
        {
            var args = new ListSharedLinksArg(req.Path, cursor);
            shares = await client.GetShares(Configuration.AccessToken, _teamMemberId, args);

            entries.AddRange(shares.Links
                .Select(share => new DropboxShare
                {
                    IsFolder = share.IsFolder,
                    IsPublic = share.LinkPermissions.RequestedVisibility.IsPublic,
                    Url = share.Url,
                    Name = share.Name,
                    Path = share.PathLower,
                    Id = share.Id
                }));

            cursor = shares.Cursor;
        }
        while (shares.HasMore && !String.IsNullOrWhiteSpace(cursor));

        return new GetSharedLinksResponse
        {
            Entries = entries
        };
    }
}
