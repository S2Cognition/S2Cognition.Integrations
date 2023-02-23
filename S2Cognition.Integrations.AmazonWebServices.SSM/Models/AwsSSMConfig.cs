using Amazon.SimpleSystemsManagement;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;

namespace S2Cognition.Integrations.AmazonWebServices.SSM.Models;

internal interface IAwsSsmConfig
{
    string? ServiceUrl { get; set; }
    IAwsRegionEndpoint? RegionEndpoint { get; set; }
    AmazonSimpleSystemsManagementConfig Native { get; }
}
internal class AwsSsmConfig : IAwsSsmConfig
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

    private readonly AmazonSimpleSystemsManagementConfig _config;
    public AmazonSimpleSystemsManagementConfig Native => _config;

    internal AwsSsmConfig()
    {
        _config = new AmazonSimpleSystemsManagementConfig { ServiceURL = ServiceUrl, RegionEndpoint = RegionEndpoint?.Native };
    }
}
