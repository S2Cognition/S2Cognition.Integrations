using Amazon.DynamoDBv2;
using S2Cognition.Integrations.AmazonWebServices.Core.Data;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

internal interface IAwsDynamoDbConfig
{
    string? ServiceUrl { get; set; }
    IAwsRegionEndpoint? RegionEndpoint { get; set; }
    AmazonDynamoDBConfig Native { get; }
}

internal class AwsDynamoDbConfig : IAwsDynamoDbConfig
{
    private string? _serviceUrl;
    public string? ServiceUrl
    {
        get => _serviceUrl;

        set
        {
            _serviceUrl = value;
            _config.ServiceURL = value;
        }
    }

    private IAwsRegionEndpoint? _regionEndpoint;
    public IAwsRegionEndpoint? RegionEndpoint
    {
        get => _regionEndpoint;

        set
        {
            _regionEndpoint = value;
            _config.RegionEndpoint = value?.Native;
        }
    }

    private readonly AmazonDynamoDBConfig _config;
    public AmazonDynamoDBConfig Native => _config;

    internal AwsDynamoDbConfig()
    {
        _config = new AmazonDynamoDBConfig { ServiceURL = ServiceUrl, RegionEndpoint = RegionEndpoint?.Native };
    }
}

