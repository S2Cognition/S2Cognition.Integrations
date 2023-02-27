using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using S2Cognition.Integrations.AmazonWebServices.Ssm.Data;

namespace S2Cognition.Integrations.AmazonWebServices.Ssm.Models;

internal interface IAwsSsmClient
{
    AmazonSimpleSystemsManagementClient Native { get; }
    Task<GetSsmParameterResponse> GetParameter(GetSsmParameterRequest req);
    Task<PutSsmParameterResponse> PutParameter(PutSsmParameterRequest req);
}
internal class AwsSsmClient : IAwsSsmClient
{
    private readonly AmazonSimpleSystemsManagementClient _client;

    public AmazonSimpleSystemsManagementClient Native => _client;

    internal AwsSsmClient(IAwsSsmConfig config)
    {
        _client = new AmazonSimpleSystemsManagementClient(config.Native);
    }

    public async Task<GetSsmParameterResponse> GetParameter(GetSsmParameterRequest req)
    {
        if (String.IsNullOrWhiteSpace(req.Name))
            throw new ArgumentException(nameof(GetSsmParameterRequest.Name));

        GetParameterResponse response;

        response = await Native.GetParameterAsync(new GetParameterRequest
        {
            Name = req.Name,
        });

        return new GetSsmParameterResponse
        {
            Value = response.Parameter.Value
        };
    }

    public async Task<PutSsmParameterResponse> PutParameter(PutSsmParameterRequest req)
    {
        if (String.IsNullOrWhiteSpace(req.Name))
            throw new ArgumentException(nameof(PutSsmParameterRequest.Name));

        if (String.IsNullOrWhiteSpace(req.Value))
            throw new ArgumentException(nameof(PutSsmParameterRequest.Value));

        if (String.IsNullOrWhiteSpace(req.Type))
            throw new ArgumentException(nameof(PutSsmParameterRequest.Type));

        await Native.PutParameterAsync(new PutParameterRequest
        {
            Name = req.Name,
            Value = req.Value,
            Type = req.Type
        });
        return new PutSsmParameterResponse();

    }
}
