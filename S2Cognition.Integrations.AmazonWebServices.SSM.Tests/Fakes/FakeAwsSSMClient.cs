using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
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

    public async Task<GetParameterResponse> GetParameter(GetParameterRequest req)
    {
        return await Task.FromResult(new GetParameterResponse
        {
            Parameter = new Parameter
            {
                Value = _parameterValue
            }
        });
    }

    public async Task<PutParameterResponse> PutParameter(PutParameterRequest req)
    {
        return await Task.FromResult(new PutParameterResponse());
    }
}
