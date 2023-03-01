using Amazon.SimpleSystemsManagement;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;

namespace S2Cognition.Integrations.AmazonWebServices.Ssm.Models;

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
            Native.ServiceURL = value;
        }
    }


    private IAwsRegionEndpoint? _regionEndpoint;
    public IAwsRegionEndpoint? RegionEndpoint
    {
        get => _regionEndpoint;

        set
        {
            _regionEndpoint = value;
            Native.RegionEndpoint = value?.Native;
        }
    }

    public AmazonSimpleSystemsManagementConfig Native { get; }

    internal AwsSsmConfig()
    {
        Native = new AmazonSimpleSystemsManagementConfig { ServiceURL = ServiceUrl, RegionEndpoint = RegionEndpoint?.Native };
    }
}
