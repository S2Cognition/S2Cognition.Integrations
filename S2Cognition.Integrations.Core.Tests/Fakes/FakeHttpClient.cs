using System.Text.Json;
using S2Cognition.Integrations.Core.Models;

namespace S2Cognition.Integrations.Core.Tests.Fakes;

public interface IFakeHttpClient
{
    void ExpectsPost(string route, string response);
    void ExpectsGet(string route, string response);

    void EnsureDisposed();
}

internal class FakeHttpClient : IFakeHttpClient, IHttpClient
{
    private readonly IDictionary<string, string> _gets = new Dictionary<string, string>();
    private readonly IDictionary<string, string> _posts = new Dictionary<string, string>();
    private bool _isDisposed = true;

    public void Dispose()
    {
        _isDisposed = true;
    }

    public void EnsureDisposed()
    {
        if (!_isDisposed)
            throw new InvalidOperationException("Test case indicates the HttpClient is not being properly disposed");

        _isDisposed = false;
    }

    public void ExpectsPost(string route, string response)
    {
        _posts.Add(route, response);
    }

    public void ExpectsGet(string route, string response)
    {
        _gets.Add(route, response);
    }

    public async Task<T?> Get<T>(string route)
    {
        if (_gets.ContainsKey(route))
            return await ProcessResponse<T>(_gets[route]);

        return default;
    }

    public async Task<T?> Post<T>(string route)
    {
        if (_posts.ContainsKey(route))
            return await ProcessResponse<T>(_posts[route]);

        return default;
    }

    private async Task<T?> ProcessResponse<T>(string response)
    {
        return await Task.FromResult(JsonSerializer.Deserialize<T>(response));
    }

    public void SetAuthorization(string auth, AuthorizationType? authType = AuthorizationType.Basic)
    {
        // Should I ensure this is set for all calls?
    }
}