using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.Core;
using S2Cognition.Integrations.Core.Models;
using S2Cognition.Integrations.Zoom.Core.Data;
using S2Cognition.Integrations.Zoom.Core.Models;
using System.Text.Json;

namespace S2Cognition.Integrations.Zoom.Core;

public interface IZoomIntegration : IIntegration<ZoomConfiguration>
{
    Task<GetUsersResponse> GetUsers(GetUsersRequest request);
}

internal class ZoomIntegration : Integration<ZoomConfiguration>, IZoomIntegration
{
    private ZoomAuthenticationResponse? _authenticationToken = null;

    internal ZoomIntegration(IServiceProvider ioc)
        : base(ioc)
    {
    }

    public override async Task Initialize(ZoomConfiguration configuration)
    {
        await base.Initialize(configuration);

        _authenticationToken = null;
    }

    private async Task<string> Authenticate()
    {
        if ((_authenticationToken != null)
            && (_authenticationToken.AccessToken != null)
            && !String.IsNullOrWhiteSpace(_authenticationToken.AccessToken))
        {
            return _authenticationToken.AccessToken;
        }

        var ioc = Configuration.IoC;

        var clientFactory = ioc.GetRequiredService<IHttpClientFactory>();
        var stringUtils = ioc.GetRequiredService<IStringUtils>();

        using var client = clientFactory.Create();
        client.SetAuthorization(stringUtils.ToBase64($"{Configuration.ClientId}:{Configuration.ClientSecret}"));

        var route = $"https://zoom.us/oauth/token?grant_type=account_credentials&account_id={Configuration.AccountId}";
        _authenticationToken = await client.Post<ZoomAuthenticationResponse>(route);

        if ((_authenticationToken == null)
            || (_authenticationToken.AccessToken == null)
            || String.IsNullOrWhiteSpace(_authenticationToken.AccessToken))
        {
            throw new InvalidOperationException();
        }

        return _authenticationToken.AccessToken;
    }

    public async Task<GetUsersResponse> GetUsers(GetUsersRequest request)
    {
        var accessToken = await Authenticate();

        var ioc = Configuration.IoC;

        var clientFactory = ioc.GetRequiredService<IHttpClientFactory>();

        using var client = clientFactory.Create();
        client.SetAuthorization(accessToken, AuthorizationType.Bearer);

        var route = $"https://api.zoom.us/v2/users";
        var zoomData = await client.Get<ZoomGetUsersPagedResponse>(route);

        return JsonSerializer.Deserialize<GetUsersResponse>(JsonSerializer.Serialize(zoomData))
            ?? throw new InvalidOperationException($"Cannot deserialize {nameof(GetUsersResponse)}");
    }
}