using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using S2Cognition.Integrations.AmazonWebServices.SSM.Data;

namespace S2Cognition.Integrations.AmazonWebServices.SSM.Models;

internal interface IAwsSsmClient
{
    AmazonSimpleSystemsManagementClient Native { get; }
    Task<GetSSMParameterResponse> GetParameter(GetSSMParameterRequest req);
    Task<PutSSMParameterResponse> PutParameter(PutSSMParameterRequest req);
}
internal class AwsSsmClient : IAwsSsmClient
{
    private readonly AmazonSimpleSystemsManagementClient _client;

    public AmazonSimpleSystemsManagementClient Native => _client;

    internal AwsSsmClient(IAwsSsmConfig config)
    {
        _client = new AmazonSimpleSystemsManagementClient(config.Native);
    }

    public async Task<GetSSMParameterResponse> GetParameter(GetSSMParameterRequest req)
    {
        if (req.Name == null)
            throw new InvalidDataException("Missing Parameters Exception");

        GetParameterResponse response;

        try
        {
            response = await Native.GetParameterAsync(new GetParameterRequest
            {
                Name = req.Name,
            });
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Aws Get Parameter Server Error. {ex.Message}");
        }

        return new GetSSMParameterResponse
        {
            Value = response.Parameter.Value
        };
    }

    public async Task<PutSSMParameterResponse> PutParameter(PutSSMParameterRequest req)
    {

        if (req.Name == null ||
            req.Value == null ||
            req.Type == null)
            throw new InvalidDataException("Missing Parameters Exception");

        try
        {
            await Native.PutParameterAsync(new PutParameterRequest
            {
                Name = req.Name,
                Value = req.Value,
                Type = req.Type
            });
            return new PutSSMParameterResponse();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Aws Put Parameter Failed. {ex.Message}");
        }
    }
}
