using Amazon.SimpleSystemsManagement;
using S2Cognition.Integrations.AmazonWebServices.SSM.Data;
using S2Cognition.Integrations.AmazonWebServices.SSM.Models;

namespace S2Cognition.Integrations.AmazonWebServices.SSM.Tests.Fakes;

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

    public async Task<GetSSMParameterResponse> GetParameter(GetSSMParameterRequest req)
    {
        if (req.Name == null)
            throw new InvalidDataException("Missing Parameters Exception");

        return await Task.FromResult(new GetSSMParameterResponse
        {
            Value = _parameterValue
        });


    }

    public async Task<PutSSMParameterResponse> PutParameter(PutSSMParameterRequest req)
    {
        if (req.Name == null ||
            req.Value == null ||
            req.Type == null)
            throw new InvalidDataException("Missing Parameters Exception");

        return await Task.FromResult(new PutSSMParameterResponse());

    }
}
