using GraphQL;
using GraphQL.Client.Abstractions.Websocket;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System.Net.Http.Headers;

namespace S2Cognition.Integrations.Core.Models;

public interface IGraphQlHttpClientFactory
{
    IGraphQlHttpClient Create();
}

internal class GraphQlHttpClientFactory : IGraphQlHttpClientFactory
{
    internal GraphQlHttpClientFactory()
    { 
    }
    
    public IGraphQlHttpClient Create()
    {
        return new GraphQlHttpClient();
    }
}

public interface IGraphQlHttpClient : IDisposable
{
    string? BaseUrl { get; set; }
    IGraphQLWebsocketJsonSerializer Serializer { get; set; }
    string? Authorization { get; set; }

    Task<GraphQLResponse<T>> SendQueryAsync<T>(GraphQLRequest request);
    Task<GraphQLResponse<T>> SendMutationAsync<T>(GraphQLRequest request);
}

internal class GraphQlHttpClient : IGraphQlHttpClient
{
    public string? BaseUrl
    {
        get
        {
            return _baseUrl;
        }

        set
        {
            _baseUrl = value;
            _client?.Dispose();
            _client = null;
        }
    }

    public IGraphQLWebsocketJsonSerializer Serializer
    {
        get
        {
            return _serializer;
        }

        set
        {
            _serializer = value;
            _client?.Dispose();
            _client = null;
        }
    }
    
    public string? Authorization
    {
        get
        {
            return _authorization;
        }

        set
        {
            _authorization = value;
            _client?.Dispose();
            _client = null;
        }
    }

    private bool _isDisposed = false;
    private string? _baseUrl = null;
    private IGraphQLWebsocketJsonSerializer _serializer = new NewtonsoftJsonSerializer();
    private string? _authorization = null;
    private GraphQLHttpClient? _client;

    private GraphQLHttpClient Client
    {
        get
        {
            if (_client == null)
            {
                if ((_baseUrl == null) || String.IsNullOrWhiteSpace(_baseUrl))
                    throw new InvalidOperationException();

                if (_serializer == null)
                    throw new InvalidOperationException();

                _client = new GraphQLHttpClient(_baseUrl, _serializer);

                if(!String.IsNullOrWhiteSpace(_authorization))
                    _client.HttpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(_authorization);
            }

            return _client;
        }
    }

    internal GraphQlHttpClient()
    {
    }

    ~GraphQlHttpClient()
    {
        Dispose(false);
    }

    public async Task<GraphQLResponse<T>> SendQueryAsync<T>(GraphQLRequest request)
    {
        return await Client.SendQueryAsync<T>(request);
    }

    public async Task<GraphQLResponse<T>> SendMutationAsync<T>(GraphQLRequest request)
    {
        return await Client.SendMutationAsync<T>(request);
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
