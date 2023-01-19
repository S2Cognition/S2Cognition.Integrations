using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using S2Cognition.Integrations.Core.Models;

namespace S2Cognition.Integrations.Core.Tests.Fakes;

public interface IFakeHttpClient
{
    void ExpectsPost(string route, string response);
    void ExpectsGet(string route, string response);

    void EnsureDisposed();
}

[SuppressMessage("Major Code Smell", "S3881:\"IDisposable\" should be implemented correctly", Justification = "This is a custom disposal pattern to enable unit test validations of dispose being called properly")]
internal class FakeHttpClient : IFakeHttpClient, IHttpClient
{
    private readonly IDictionary<string, string> _gets = new Dictionary<string, string>();
    private readonly IDictionary<string, string> _posts = new Dictionary<string, string>();
    private bool _isDisposed = true;

    internal FakeHttpClient()
    { 
    }
    
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
        if (_gets.TryGetValue(route, out string? value))
            return await ProcessResponse<T>(value);

        return default;
    }

    public async Task<T?> Post<T>(string route)
    {
        if (_posts.TryGetValue(route, out string? value))
            return await ProcessResponse<T>(value);

        return default;
    }

    private static async Task<T?> ProcessResponse<T>(string response)
    {
        return await Task.FromResult(JsonSerializer.Deserialize<T>(response));
    }

    public void SetAuthorization(string auth, AuthorizationType? authType = AuthorizationType.Basic)
    {
        // Should I ensure this is set for all calls?
    }
}
