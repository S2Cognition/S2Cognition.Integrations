using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
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

    //private const string _expectedParameterValue = "ADEWI2123kisdr908734";
    private string? _parameterValue = null;


    public void ExpectedParameterValue(string getParameterValue)
    {
        _parameterValue = getParameterValue;
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
                Name = _parameterValue,
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

        return await Task.FromResult(new PutSSMParameterResponse());

    }
}
