using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.Core.Tests;
using S2Cognition.Integrations.AmazonWebServices.Ses.Data;
using S2Cognition.Integrations.AmazonWebServices.Ses.Models;
using S2Cognition.Integrations.AmazonWebServices.Ses.Tests.Fakes;
using S2Cognition.Integrations.Core.Tests;
using Shouldly;

namespace S2Cognition.Integrations.AmazonWebServices.Ses.Tests;

public class SesTests : UnitTestBase
{
    private IAmazonWebServicesSesIntegration _sut = default!;
    private IFakeAwsSesClient _client = default!;

    protected override async Task IocSetup(IServiceCollection sc)
    {
        sc.AddAmazonWebServicesSesIntegration();
        sc.AddFakeAmazonWebServices();
        sc.AddFakeAmazonWebServicesSes();

        await Task.CompletedTask;
    }

    protected override async Task TestSetup()
    {
        var configuration = new AmazonWebServicesSesConfiguration(_ioc)
        {
            AccessKey = "fake AccessKey",
            SecretKey = "fake SecretKey",
            AwsRegion = "fake AwsRegion"
        };

        _sut = _ioc.GetRequiredService<IAmazonWebServicesSesIntegration>();
        await _sut.Initialize(configuration);

        var configFactory = _ioc.GetRequiredService<IAwsSesConfigFactory>();
        var config = configFactory.Create();

        var clientFactory = _ioc.GetRequiredService<IAwsSesClientFactory>();
        _client = clientFactory.Create(config) as IFakeAwsSesClient ?? throw new InvalidOperationException();
    }

    [Theory]
    [InlineData(null, "recipient@test.com", "Subject", "Body", nameof(SendRequest.Sender))]
    [InlineData("sender@test.com", null, "Subject", "Body", nameof(SendRequest.Recipient))]
    [InlineData("sender@test.com", "recipient@test.com", null, "Body", nameof(SendRequest.Subject))]
    [InlineData("sender@test.com", "recipient@test.com", "Subject", null, nameof(SendRequest.Body))]
    [InlineData("sender@test", "recipient@test.com", "Subject", "Body", nameof(SendRequest.Sender))]
    [InlineData("sender@test.com", "recipient@", "Subject", "Body", nameof(SendRequest.Recipient))]

    public async Task EnsureSendReturnsExceptionWIthNullParams(string sender, string recipient, string subject, string body, string expectedError)
    {
        var ex = await Should.ThrowAsync<ArgumentException>(async () => await _sut.Send(new SendRequest
        {
            Sender = sender,
            Recipient = recipient,
            Subject = subject,
            Body = body
        }));

        ex.Message.ShouldBe(expectedError);
    }

    [Fact]
    public async Task EnsureSendEmailReturnsMessageId()
    {
        var fakeRecipient = "testRecipient@test.com";
        var fakeSender = "testSender@test.com";
        var fakeSubject = "test subject";
        var fakeBody = "test body";

        var response = await _sut.Send(new SendRequest
        {
            Body = fakeBody,
            Recipient = fakeRecipient,
            Sender = fakeSender,
            Subject = fakeSubject
        });

        response.ShouldNotBeNull();
    }

}