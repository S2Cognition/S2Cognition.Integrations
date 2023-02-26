using Amazon.S3;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;

namespace S2Cognition.Integrations.AmazonWebServices.S3.Models;

internal interface IAwsS3Config
{
    string? ServiceUrl { get; set; }
    IAwsRegionEndpoint? RegionEndpoint { get; set; }
    AmazonS3Config Native { get; }
}
internal class AwsS3Config : IAwsS3Config
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

    private readonly AmazonS3Config _config;
    public AmazonS3Config Native => _config;

    internal AwsS3Config()
    {
        _config = new AmazonS3Config { ServiceURL = ServiceUrl, RegionEndpoint = RegionEndpoint?.Native };
    }
}
