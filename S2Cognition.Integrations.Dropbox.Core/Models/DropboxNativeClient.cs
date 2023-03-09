namespace S2Cognition.Integrations.Dropbox.Core.Models;

public interface IDropboxNativeClient
{
    Task<MembersListV2Result> GetTeamMembers(string accessToken, string? cursor);
    Task<ListFolderResult> GetFolder(string accessToken, string teamMemberId, ListFolderArg args, string? cursor);
    Task<SearchV2Result> GetFiles(string accessToken, string teamMemberId, SearchV2Arg args, string? cursor);
    Task<IDownloadResponse<FileMetadata>> GetFile(string accessToken, string teamMemberId, DownloadArg args);
    Task<ListSharedLinksResult> GetShares(string accessToken, string teamMemberId, ListSharedLinksArg args);
}

internal class DropboxNativeClient : IDropboxNativeClient, IDisposable
{
    private DropboxClientConfig? _primaryConfiguration = null;
    private FullAccount? _primaryAccount = null;
    private DropboxClient? _primaryClient = null;
    private DropboxTeamClient? _teamClient = null;
    private bool _isDisposed = false;

    internal DropboxNativeClient()
    {
    }

    public async Task<ListFolderResult> GetFolder(string accessToken, string teamMemberId, ListFolderArg args, string? cursor)
    {
        var client = await GetPrimaryClient(accessToken, teamMemberId);
        if (String.IsNullOrWhiteSpace(cursor))
            return await client.Files.ListFolderAsync(args);

        return await client.Files.ListFolderContinueAsync(cursor);
    }

    public async Task<IDownloadResponse<FileMetadata>> GetFile(string accessToken, string teamMemberId, DownloadArg args)
    {
        var client = await GetPrimaryClient(accessToken, teamMemberId);

        return await client.Files.DownloadAsync(args);
    }

    public async Task<MembersListV2Result> GetTeamMembers(string accessToken, string? cursor)
    {
        var client = await GetTeamClient(accessToken);
        if (String.IsNullOrWhiteSpace(cursor))
            return await client.Team.MembersListV2Async();

        return await client.Team.MembersListContinueV2Async(cursor);
    }

    public async Task<SearchV2Result> GetFiles(string accessToken, string teamMemberId, SearchV2Arg args, string? cursor)
    {
        var client = await GetPrimaryClient(accessToken, teamMemberId);
        if (String.IsNullOrWhiteSpace(cursor))
            return await client.Files.SearchContinueV2Async(cursor);

        return await client.Files.SearchV2Async(args);
    }

    public async Task<ListSharedLinksResult> GetShares(string accessToken, string teamMemberId, ListSharedLinksArg args)
    {
        var client = await GetPrimaryClient(accessToken, teamMemberId);
        return await client.Sharing.ListSharedLinksAsync(args);
    }

    private async Task<DropboxTeamClient> GetTeamClient(string accessToken)
    {
        _teamClient ??= new DropboxTeamClient(accessToken);

        return await Task.FromResult(_teamClient);
    }

    private async Task<DropboxClientConfig> GetPrimaryConfiguration(string teamMemberId)
    {
        if (_primaryConfiguration == null)
        {
            _primaryConfiguration = new DropboxClientConfig
            {
                HttpClient = new HttpClient()
            };

            _primaryConfiguration.HttpClient.DefaultRequestHeaders.Add("Dropbox-API-Select-User", teamMemberId);
        }
        
        return await Task.FromResult(_primaryConfiguration);
    }

    private async Task<FullAccount> GetPrimaryAccount(string accessToken, string teamMemberId)
    {
        if (_primaryAccount == null)
        {
            var clientConfig = await GetPrimaryConfiguration(teamMemberId);

            using var accountClient = new DropboxClient(accessToken, clientConfig);
            _primaryAccount = await accountClient.Users.GetCurrentAccountAsync();
        }

        return _primaryAccount ?? throw new InvalidOperationException();
    }

    private async Task<DropboxClient> GetPrimaryClient(string accessToken, string teamMemberId)
    {
        if (_primaryClient == null)
        {
            var dropboxAccount = await GetPrimaryAccount(accessToken, teamMemberId);
            var clientConfig = await GetPrimaryConfiguration(teamMemberId);
            clientConfig.HttpClient.DefaultRequestHeaders
                .Add("Dropbox-API-Path-Root", $@"{{"".tag"": ""namespace_id"", ""namespace_id"": ""{dropboxAccount.RootInfo.RootNamespaceId}""}}");

            _primaryClient = new DropboxClient(accessToken, clientConfig);
        }

        return _primaryClient;
    }

    protected virtual void Dispose(bool isDisposing)
    {
        if (!_isDisposed)
        {
            if (isDisposing)
            {
                _teamClient?.Dispose();
                _teamClient = null;

                _primaryClient?.Dispose();
                _primaryClient = null;
            }

            _isDisposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(isDisposing: true);
        GC.SuppressFinalize(this);
    }
}
