using Amazon.SimpleSystemsManagement;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;
using S2Cognition.Integrations.AmazonWebServices.Ssm.Data;
using S2Cognition.Integrations.AmazonWebServices.Ssm.Models;

namespace S2Cognition.Integrations.AmazonWebServices.Ssm.Tests.Fakes;

internal class FakeAwsSsmConfig : IAwsSsmConfig
{
    public string? ServiceUrl { get; set; }
    public IAwsRegionEndpoint? RegionEndpoint { get; set; }
    public EnvironmentType? Environment { get; set; }

    public AmazonSimpleSystemsManagementConfig Native => throw new NotImplementedException();
}
