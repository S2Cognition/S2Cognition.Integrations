using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;
using S2Cognition.Integrations.AmazonWebServices.Core.Tests;
using S2Cognition.Integrations.Core.Tests;
using Shouldly;


namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Tests
{
    public class CloudWatchTests : UnitTestBase
    {
        private AmazonWebServicesCloudWatchConfiguration _configuration = default!;
        private IAmazonWebServicesCloudWatchIntegration _sut = default!;

        protected override async Task IocSetup(IServiceCollection sc)
        {
            sc.AddAmazonWebServicesCloudWatchIntegration();
            sc.AddFakeAmazonWebServices();
            sc.AddFakeAmazonWebServicesCloudWatch();

            await Task.CompletedTask;
        }

        protected override async Task TestSetup()
        {
            _configuration = new AmazonWebServicesCloudWatchConfiguration(_ioc)
            {
                AccessKey = "fake AccessKey",
                SecretKey = "fake SecretKey",
                AwsRegion = "fake AwsRegion",
                ServiceUrl = "fake ServiceUrl"
            };

            _sut = _ioc.GetRequiredService<IAmazonWebServicesCloudWatchIntegration>();
            await Task.CompletedTask;
        }

        [Fact]
        public async Task EnsureCheckingForInitializationReturnsExpectedResults()
        {
            var isInitialized = await _sut.IsInitialized();
            isInitialized.ShouldBeFalse();

            await _sut.Initialize(_configuration);

            var x = _sut.IsInitialized;

            isInitialized = await _sut.IsInitialized();
            isInitialized.ShouldBeTrue();
        }
    }
}
