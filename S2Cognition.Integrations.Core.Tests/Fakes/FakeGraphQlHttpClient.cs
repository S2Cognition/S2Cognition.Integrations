using GraphQL;
using GraphQL.Client.Abstractions.Websocket;
using S2Cognition.Integrations.Core.Models;

namespace S2Cognition.Integrations.Core.Tests.Fakes;

public interface IFakeGraphQlHttpClient
{
    void ExpectQuery<T>(string[][] queryData, T response);
    void ExpectMutation<T>(string[][] mutationData, T response);

    void EnsureDisposed();
}

public class FakeGraphQlHttpClient : IFakeGraphQlHttpClient, IGraphQlHttpClient
{
    public string? BaseUrl { get; set; }
    public IGraphQLWebsocketJsonSerializer Serializer { get; set; } = default!;
    public string? Authorization { get; set; }

    private readonly List<(string[][] Expectations, object? Response)> _expectedQueries = new();
    private readonly List<(string[][] Expectations, object? Response)> _expectedMutations = new();
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

    public void ExpectQuery<T>(string[][] queryData, T response)
    {
        _expectedQueries.Add((Expectations: queryData, Response: response));
    }

    public void ExpectMutation<T>(string[][] mutationData, T response)
    {
        _expectedMutations.Add((Expectations: mutationData, Response: response));
    }

    public async Task<GraphQLResponse<T>> SendMutationAsync<T>(GraphQLRequest request)
    {
        foreach (var expectation in _expectedMutations)
        {
            if (expectation.Expectations.All(_ => StringsRoughlyEqual(request[_.First()].ToString(), _.Skip(1).First())))
            {
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                return await Task.FromResult(new GraphQLResponse<T>
                {
                    Data = (T)expectation.Response
                });
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8601 // Possible null reference assignment.
            }
        }

        var err = "Mutation Not Found:";
        foreach (var kvp in request)
        {
            err += $"{Environment.NewLine}* {kvp.Key} ==> {Simplify(kvp.Value.ToString())}";
        }

        throw new InvalidOperationException(err);
    }

    public async Task<GraphQLResponse<T>> SendQueryAsync<T>(GraphQLRequest request)
    {
        foreach (var expectation in _expectedQueries)
        {
            if (expectation.Expectations.All(_ => StringsRoughlyEqual(request[_.First()].ToString(), _.Skip(1).First())))
            {
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                return await Task.FromResult(new GraphQLResponse<T>
                {
                    Data = (T)expectation.Response
                });
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8601 // Possible null reference assignment.
            }
        }

        var err = "Query Not Found:";
        foreach (var kvp in request)
        {
            err += $"{Environment.NewLine}* {kvp.Key} ==> {Simplify(kvp.Value.ToString())}";
        }

        throw new InvalidOperationException(err);
    }

    private bool StringsRoughlyEqual(string? a, string b)
    {
        return String.Equals(Simplify(a), Simplify(b), StringComparison.InvariantCultureIgnoreCase);
    }

    private string Simplify(string? str)
    {
        str = (str ?? String.Empty)
            .Replace("{", " { ")
            .Replace("}", " } ")
            .Replace("\n", " ")
            .Replace("\r", " ")
            .Replace("\t", " ");

        while(str.Contains("  "))
            str = str.Replace("  ", " ");

        return str.Trim();
    }
}
