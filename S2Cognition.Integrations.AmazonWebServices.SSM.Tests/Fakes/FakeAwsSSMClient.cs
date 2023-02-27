using Amazon.SimpleSystemsManagement;
using S2Cognition.Integrations.AmazonWebServices.Ssm.Data;
using S2Cognition.Integrations.AmazonWebServices.Ssm.Models;

namespace S2Cognition.Integrations.AmazonWebServices.Ssm.Tests.Fakes;

internal interface IFakeAwsSsmClient
{
    void ExpectedParameterValue(string getParameterValue);
}
internal class FakeAwsSsmClient : IAwsSsmClient, IFakeAwsSsmClient
{
    public AmazonSimpleSystemsManagementClient Native => throw new NotImplementedException();

    private string? _parameterValue = null;

    public void ExpectedParameterValue(string getParameterValue)
    {
        _parameterValue = getParameterValue;
    }

    public async Task<GetSsmParameterResponse> GetParameter(GetSsmParameterRequest req)
    {
        if (String.IsNullOrWhiteSpace(req.Name))
            throw new ArgumentException(nameof(GetSsmParameterRequest.Name));

        return await Task.FromResult(new GetSsmParameterResponse
        {
            Value = _parameterValue
        });
    }

    public async Task<PutSsmParameterResponse> PutParameter(PutSsmParameterRequest req)
    {
        if ((req.Name == null) || (req.Name.Length < 1))
            throw new ArgumentException(nameof(PutSsmParameterRequest.Name));

        if (String.IsNullOrWhiteSpace(req.Value))
            throw new ArgumentException(nameof(PutSsmParameterRequest.Value));

        if (String.IsNullOrWhiteSpace(req.Type))
            throw new ArgumentException(nameof(PutSsmParameterRequest.Type));

        return await Task.FromResult(new PutSsmParameterResponse());

    }
}
