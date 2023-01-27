using Amazon;

namespace S2Cognition.Integrations.AmazonWebServices.Core.Models;

internal interface IAwsRegionEndpoint
{
    RegionEndpoint Native { get; }
}

internal class AwsRegionEndpoint : IAwsRegionEndpoint
{
    private readonly RegionEndpoint _region;
    public RegionEndpoint Native => _region;

    internal AwsRegionEndpoint(RegionEndpoint region)
    {
        _region = region;
    }
}