using Amazon.CloudWatch;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models;

internal interface IAwsCloudWatchConfig
{
    string? ServiceUrl { get; set; }
    IAwsRegionEndpoint? RegionEndpoint { get; set; }
    AmazonCloudWatchConfig Native { get; }
}

internal class AwsCloudWatchConfig : IAwsCloudWatchConfig
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

    private readonly AmazonCloudWatchConfig _config;
    public AmazonCloudWatchConfig Native => _config;

    internal AwsCloudWatchConfig()
    {
        _config = new AmazonCloudWatchConfig { ServiceURL = ServiceUrl, RegionEndpoint = RegionEndpoint?.Native };
    }
}


