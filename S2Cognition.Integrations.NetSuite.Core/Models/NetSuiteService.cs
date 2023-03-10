using Oracle.NetSuite;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Text;
using System.Xml;
using S2Cognition.Integrations.NetSuite.Core.Data;

using VoidTask = System.Threading.Tasks.Task;

namespace S2Cognition.Integrations.NetSuite.Core.Models;

internal interface INetSuiteService
{
    Task<getResponse?> Get(BaseRef record);
    Task<getListResponse?> List(params BaseRef[] records);
    Task<searchResponse?> Search(SearchRecord record);
    Task<addResponse?> Add(Record record);
    Task<updateResponse?> Update(Record record);
}

internal class NetSuiteService : NetSuitePortTypeClient, INetSuiteService
{
    private const string UrlLookupHostname = "https://webservices.netsuite.com";
    private const string ServicePath = "services/NetSuitePort_2022_2";

    private readonly NetSuiteConfiguration _configuration;

    internal static async Task<INetSuiteService> Create(NetSuiteConfiguration configuration)
    {
        var binding = new BasicHttpBinding
        {
            MaxBufferSize = int.MaxValue,
            ReaderQuotas = XmlDictionaryReaderQuotas.Max,
            MaxReceivedMessageSize = int.MaxValue,
            AllowCookies = true,
        };

        binding.Security.Mode = BasicHttpSecurityMode.Transport;

        var dynamicUrlLookup = new NetSuiteService(configuration, binding, new Uri($"{UrlLookupHostname}/{ServicePath}"));
        var dynamicHostname = await dynamicUrlLookup.LookupDynamicHostname();
        var soapUri = new Uri($"{dynamicHostname}/{ServicePath}");

        return new NetSuiteService(configuration, binding, soapUri);
    }

    public async Task<searchResponse?> Search(SearchRecord record)
    {
        var request = new searchRequest(await GetTokenPassport(), null, null, null, record);
        var response = await searchAsync(request);
        return response;
    }

    public async Task<addResponse?> Add(Record record)
    {
        var request = new addRequest(await GetTokenPassport(), null, null, null, record);
        var response = await addAsync(request);
        return response;
    }

    public async Task<updateResponse?> Update(Record record)
    {
        var request = new updateRequest(await GetTokenPassport(), null, null, null, record);
        var response = await updateAsync(request);
        return response;
    }

    public async Task<getResponse?> Get(BaseRef record)
    {
        var request = new getRequest(await GetTokenPassport(), null, null, null, record);
        var response = await getAsync(request);
        return response;
    }

    public async Task<getListResponse?> List(params BaseRef[] records)
    {
        var request = new getListRequest(await GetTokenPassport(), null, null, null, records);
        var response = await getListAsync(request);
        return response;
    }

    private NetSuiteService(NetSuiteConfiguration configuration, Binding binding, Uri uri)
        : base(binding, new EndpointAddress(uri.ToString()))
    {
        _configuration = configuration;
    }

    private async Task<TokenPassport> GetTokenPassport()
    {
        if ((_configuration.AccountId == null)
            || String.IsNullOrWhiteSpace(_configuration.AccountId)
            || (_configuration.ConsumerKey == null)
            || String.IsNullOrWhiteSpace(_configuration.ConsumerKey)
            || (_configuration.ConsumerSecret == null)
            || String.IsNullOrWhiteSpace(_configuration.ConsumerSecret)
            || (_configuration.TokenId == null)
            || String.IsNullOrWhiteSpace(_configuration.TokenId)
            || (_configuration.TokenSecret == null)
            || String.IsNullOrWhiteSpace(_configuration.TokenSecret))
        {
            throw new InvalidOperationException("NetSuiteCrm configuration is invalid");
        }

        string nonce = ComputeNonce();
        long timestamp = ComputeTimestamp();
        TokenPassportSignature signature = ComputeSignature(_configuration.AccountId, _configuration.ConsumerKey,
            _configuration.ConsumerSecret, _configuration.TokenId, _configuration.TokenSecret, nonce, timestamp);

        return await VoidTask.FromResult(new TokenPassport
        {
            account = _configuration.AccountId,
            consumerKey = _configuration.ConsumerKey,
            token = _configuration.TokenId,
            nonce = nonce,
            timestamp = timestamp,
            signature = signature
        });
    }

    private static string ComputeNonce()
    {
        var bytes = new byte[4];
        RandomNumberGenerator.Create().GetBytes(bytes);
        var n = (bytes[0]&0x7F) | (bytes[1] << 8) | (bytes[2] << 16) | (bytes[3] << 24);

        var maxExclusive = 10000000 - 123400;
        var data = 123400 + (n % maxExclusive);
        
        return data.ToString();
    }

    private static long ComputeTimestamp()
    {
        // from https://source.dot.net/
        var daysTo1970 = 719162L;
        var ticksPerDay = 864000000000L;
        var unixEpoch = new DateTime(daysTo1970 * ticksPerDay, DateTimeKind.Utc);
        return (long)DateTime.UtcNow.Subtract(unixEpoch).TotalSeconds;
    }

    private static TokenPassportSignature ComputeSignature(string compId, string consumerKey, string consumerSecret, string tokenId, string tokenSecret, string nonce, long timestamp)
    {
        var baseString = $"{compId}&{consumerKey}&{tokenId}&{nonce}&{timestamp}";
        var key = $"{consumerSecret}&{tokenSecret}";
        var encoding = new ASCIIEncoding();
        var keyBytes = encoding.GetBytes(key);
        var baseStringBytes = encoding.GetBytes(baseString);
        using var hmacSha256 = new HMACSHA256(keyBytes);
        var hashBaseString = hmacSha256.ComputeHash(baseStringBytes);
        var signature = Convert.ToBase64String(hashBaseString);
        return new TokenPassportSignature
        {
            algorithm = "HMAC-SHA256",
            Value = signature
        };
    }

    private async Task<string> LookupDynamicHostname()
    {
        var request = new getDataCenterUrlsRequest(await GetTokenPassport(), null, null, null, _configuration.AccountId);
        var response = await getDataCenterUrlsAsync(request);
        var urls = response.getDataCenterUrlsResult.dataCenterUrls;

        return urls.webservicesDomain;
    }
}