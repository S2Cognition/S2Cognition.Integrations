using Amazon.SimpleSystemsManagement;

namespace S2Cognition.Integrations.AmazonWebServices.SSM.Models;

internal interface IAwsSsmClient
{
    AmazonSimpleSystemsManagementClient Native { get; }
}
internal class AwsSsmClient : IAwsSsmClient
{
    private readonly AmazonSimpleSystemsManagementClient _client;

    public AmazonSimpleSystemsManagementClient Native => _client;

    internal AwsSsmClient(IAwsSsmConfig config)
    {
        _client = new AmazonSimpleSystemsManagementClient(config.Native);
    }
}
