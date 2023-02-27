using Amazon.SimpleSystemsManagement;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;
using S2Cognition.Integrations.AmazonWebServices.SSM.Models;

namespace S2Cognition.Integrations.AmazonWebServices.SSM.Tests.Fakes;

internal class FakeAwsSsmConfig : IAwsSsmConfig
{
    public string? ServiceUrl { get; set; }
    public IAwsRegionEndpoint? RegionEndpoint { get; set; }

    public AmazonSimpleSystemsManagementConfig Native => throw new NotImplementedException();
}
