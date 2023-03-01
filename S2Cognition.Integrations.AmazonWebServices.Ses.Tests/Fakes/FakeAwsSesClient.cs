using Amazon.SimpleEmailV2;
using Amazon.SimpleEmailV2.Model;
using S2Cognition.Integrations.AmazonWebServices.Ses.Models;

namespace S2Cognition.Integrations.AmazonWebServices.Ses.Tests.Fakes;

internal interface IFakeAwsSesClient
{

}
internal class FakeAwsSesClient : IAwsSesClient, IFakeAwsSesClient
{
    public AmazonSimpleEmailServiceV2Client Native => throw new NotImplementedException();

    public async Task<SendEmailResponse> Send(SendEmailRequest req)
    {
        return await Task.FromResult(new SendEmailResponse());
    }
}
