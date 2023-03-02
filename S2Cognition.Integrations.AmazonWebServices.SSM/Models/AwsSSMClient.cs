using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;

namespace S2Cognition.Integrations.AmazonWebServices.Ssm.Models;

internal interface IAwsSsmClient
{
    AmazonSimpleSystemsManagementClient Native { get; }
    Task<GetParameterResponse> GetParameter(GetParameterRequest req);
    Task<PutParameterResponse> PutParameter(PutParameterRequest req);
}

internal class AwsSsmClient : IAwsSsmClient
{
    private readonly AmazonSimpleSystemsManagementClient _client;

    public AmazonSimpleSystemsManagementClient Native => _client;

    internal AwsSsmClient(IAwsSsmConfig config)
    {
        _client = new AmazonSimpleSystemsManagementClient(config.Native);
    }

    public async Task<GetParameterResponse> GetParameter(GetParameterRequest req)
    {
        return await Native.GetParameterAsync(req);
    }

    public async Task<PutParameterResponse> PutParameter(PutParameterRequest req)
    {
        return await Native.PutParameterAsync(req);
    }
}
