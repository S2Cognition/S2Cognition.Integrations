using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.Core.Tests;
using S2Cognition.Integrations.AmazonWebServices.SSM.Data;
using S2Cognition.Integrations.AmazonWebServices.SSM.Models;
using S2Cognition.Integrations.AmazonWebServices.SSM.Tests.Fakes;
using S2Cognition.Integrations.Core.Tests;
using Shouldly;

namespace S2Cognition.Integrations.AmazonWebServices.SSM.Tests
{
    public class SSMTests : UnitTestBase
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
                AwsRegion = "fake AwsRegion"
            };

            _sut = _ioc.GetRequiredService<IAmazonWebServicesSsmIntegration>();

            await _sut.Initialize(configuration);

            var configFactory = _ioc.GetRequiredService<IAwsSsmConfigFactory>();
            var config = configFactory.Create();

            var clientFactory = _ioc.GetRequiredService<IAwsSsmClientFactory>();
            _client = clientFactory.Create(config) as IFakeAwsSsmClient ?? throw new InvalidOperationException();
        }

        [Fact]
        public async Task EnsureGetParameterReturnsExceptionWIthNullParams()
        {

            _client.ExpectedParameterValue(FakeParameterValue);

            await Should.ThrowAsync<InvalidDataException>(async () => await _sut.GetSSMParameter(new GetSSMParameterRequest
            {
                Name = null
            }));

            await Task.CompletedTask;
        }

        [Fact]
        public async Task EnsureStoreParameterReturnsExceptionWIthNullParams()
        {

            _client.ExpectedParameterValue(FakeParameterValue);

            await Should.ThrowAsync<InvalidDataException>(async () => await _sut.StoreSSMParameter(new PutSSMParameterRequest
            {
                Name = null,
                Value = null,
                Type = null
            }));

            await Task.CompletedTask;
        }

        [Fact]
        public async Task EnsureGetSSMParameterReturnsExpectedResult()
        {
            var fakeParameterName = "fake parameter name";

            _client.ExpectedParameterValue(FakeParameterValue);

            var response = await _sut.GetSSMParameter(new GetSSMParameterRequest
            {
                Name = fakeParameterName
            });

            response.ShouldNotBeNull();

        }
    }
}
