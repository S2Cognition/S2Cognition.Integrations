using S2Cognition.Integrations.AmazonWebServices.Core.Models;

namespace S2Cognition.Integrations.AmazonWebServices.Ses.Models;

internal interface IAwsSesConfig
{
    IAwsRegionEndpoint? RegionEndpoint { get; set; }
}

internal class AwsSesConfig : IAwsSesConfig
{
    public IAwsRegionEndpoint? RegionEndpoint { get; set; }

    internal AwsSesConfig()
    {
    }
}
