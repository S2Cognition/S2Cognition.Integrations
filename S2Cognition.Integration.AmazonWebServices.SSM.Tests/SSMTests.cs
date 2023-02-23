using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.Core.Tests;
using S2Cognition.Integrations.AmazonWebServices.SSM.Data;
using S2Cognition.Integrations.AmazonWebServices.SSM.Models;
using S2Cognition.Integrations.AmazonWebServices.SSM.Tests.Fakes;
using S2Cognition.Integrations.Core.Tests;

namespace S2Cognition.Integrations.AmazonWebServices.SSM.Tests
{
    internal class SSMTests : UnitTestBase
    {
        private IAmazonWebServicesSsmIntegration _sut = default!;
        private IFakeAwsSsmClient _client = default!;

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
    }
}
