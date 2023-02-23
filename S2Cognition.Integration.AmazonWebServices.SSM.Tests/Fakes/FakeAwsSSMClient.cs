using Amazon.SimpleSystemsManagement;
using S2Cognition.Integrations.AmazonWebServices.SSM.Models;

namespace S2Cognition.Integrations.AmazonWebServices.SSM.Tests.Fakes;

internal interface IFakeAwsSsmClient
{
}
internal class FakeAwsSsmClient : IAwsSsmClient, IFakeAwsSsmClient
{
    public AmazonSimpleSystemsManagementClient Native => throw new NotImplementedException();
}
