using Amazon.CloudWatch;
using S2Cognition.Integrations.AmazonWebServices.Core.Data;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data
{
    public interface IAwsCloudWatchConfig
    {
        string? ServiceUrl { get; set; }
        IAwsRegionEndpoint? RegionEndpoint { get; set; }
        AmazonCloudWatchConfig Native { get; }
    }
}
