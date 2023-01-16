using System.Net.Http.Headers;
using System.Text.Json;
using SystemHttpClient = System.Net.Http.HttpClient;

namespace S2Cognition.Integrations.Core.Models;

public interface IHttpClientFactory
{
    IHttpClient Create();
}

internal class HttpClientFactory : IHttpClientFactory
{
    public IHttpClient Create()
    {
        return new HttpClient();
    }
}

public enum AuthorizationType
{
    Basic,
    Bearer
}

public interface IHttpClient : IDisposable
{
    void SetAuthorization(string auth, AuthorizationType? authType = AuthorizationType.Basic);
    Task<T?> Get<T>(string route);
    Task<T?> Post<T>(string route);
}

public class HttpClient : IHttpClient
{
    private SystemHttpClient? _client = new();
    private bool _isDisposed = false;

    public HttpClient()
    {
    }

    ~HttpClient()
    {
        Dispose(false);
    }

    public void SetAuthorization(string auth, AuthorizationType? authType = AuthorizationType.Basic)
    {
        if (_client == null)
            throw new ObjectDisposedException(nameof(HttpClient));

        var type = authType switch
        {
            AuthorizationType.Bearer => "Bearer",
            _ => "Basic"
        };

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(type, auth);
    }

    public async Task<T?> Get<T>(string route)
    {
        if (_client == null)
            throw new ObjectDisposedException(nameof(HttpClient));

        var response = await _client.GetAsync(route);
        return await ProcessResponse<T>(response);
    }

    public async Task<T?> Post<T>(string route)
    {
        if (_client == null)
            throw new ObjectDisposedException(nameof(HttpClient));

        var response = await _client.PostAsync(route, null);
        return await ProcessResponse<T>(response);
    }

    private static async Task<T?> ProcessResponse<T>(HttpResponseMessage response)
    {
        var jsonString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(jsonString);
    }

    protected virtual void Dispose(bool isDisposing)
    {
        if (!_isDisposed)
        {
            if (isDisposing)
            {
                _client?.Dispose();
                _client = null;
            }

            _isDisposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}