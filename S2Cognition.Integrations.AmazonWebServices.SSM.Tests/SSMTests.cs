using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.Core.Tests;
using S2Cognition.Integrations.AmazonWebServices.Ssm.Data;
using S2Cognition.Integrations.AmazonWebServices.Ssm.Models;
using S2Cognition.Integrations.AmazonWebServices.Ssm.Tests.Fakes;
using S2Cognition.Integrations.Core.Tests;
using Shouldly;

namespace S2Cognition.Integrations.AmazonWebServices.Ssm.Tests;

public class SsmTests : UnitTestBase
{
    private IAmazonWebServicesSsmIntegration _sut = default!;
    private IFakeAwsSsmClient _client = default!;
    private static string FakeParameterValue => "re9345-23DKL39kddg";

    protected override async Task IocSetup(IServiceCollection sc)
    {
        sc.AddAmazonWebServicesSsmIntegration();
        sc.AddFakeAmazonWebServices();
        sc.AddFakeAmazonWebServicesSsm();

        await Task.CompletedTask;
    }

    protected override async Task TestSetup()
    {
        var configuration = new AmazonWebServicesSsmConfiguration(_ioc)
        {
            AccessKey = "fake AccessKey",
            SecretKey = "fake SecretKey",
            AwsRegion = "fake AwsRegion",
            Environment = EnvironmentType.Development
        };

        _sut = _ioc.GetRequiredService<IAmazonWebServicesSsmIntegration>();

        await _sut.Initialize(configuration);

        var configFactory = _ioc.GetRequiredService<IAwsSsmConfigFactory>();
        var config = configFactory.Create();

        var clientFactory = _ioc.GetRequiredService<IAwsSsmClientFactory>();
        _client = clientFactory.Create(config) as IFakeAwsSsmClient ?? throw new InvalidOperationException();
    }

    [Theory]
    [InlineData(null, nameof(GetSsmParameterRequest.Name))]
    public async Task EnsureGetParameterReturnsExceptionWIthNullParams(string name, string expectedError)
    {
        _client.ExpectedParameterValue(FakeParameterValue);

        var ex = await Should.ThrowAsync<ArgumentException>(async () => await _sut.GetSsmParameter(new GetSsmParameterRequest
        {
            Name = name
        }));

        ex.Message.ShouldBe(expectedError);
    }

    [Theory]
    [InlineData(null, "Value", "string", nameof(PutSsmParameterRequest.Name))]
    [InlineData("Name", null, "string", nameof(PutSsmParameterRequest.Value))]
    [InlineData("Name", "Value", null, nameof(PutSsmParameterRequest.Type))]
    public async Task EnsurePutParameterReturnsExceptionWIthNullParams(string name, string value, string type, string expectedError)
    {
        _client.ExpectedParameterValue(FakeParameterValue);

        var ex = await Should.ThrowAsync<ArgumentException>(async () => await _sut.PutSsmParameter(new PutSsmParameterRequest
        {
            Name = name,
            Value = value,
            Type = type
        }));

        ex.Message.ShouldBe(expectedError);
    }

    [Fact]
    public async Task EnsureGetSsmParameterReturnsExpectedResult()
    {
        var fakeParameterName = "fake parameter name";

        _client.ExpectedParameterValue(FakeParameterValue);

        var response = await _sut.GetSsmParameter(new GetSsmParameterRequest
        {
            Name = fakeParameterName
        });

        response.ShouldNotBeNull();
        response.Value.ShouldNotBeNull();
        response.Value.ShouldBeEquivalentTo(FakeParameterValue);
    }

    [Fact]
    public async Task EnsurePutSsmParameterReturnsExpectedResult()
    {
        var fakeParameterName = "fake parameter name";
        var fakeParameterValue = "aie-3454-adrUIEP";
        var fakeParameterType = "string";

        _client.ExpectedParameterValue(FakeParameterValue);

        var response = await _sut.PutSsmParameter(new PutSsmParameterRequest
        {
            Name = fakeParameterName,
            Value = fakeParameterValue,
            Type = fakeParameterType
        });

        response.ShouldNotBeNull();
    }
}

