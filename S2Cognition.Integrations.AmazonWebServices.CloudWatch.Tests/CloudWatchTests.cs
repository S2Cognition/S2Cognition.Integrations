using Amazon.CloudWatch.Model;
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
        private IAwsCloudWatchClient _client = default!;

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

            var configFactory = _ioc.GetRequiredService<IAwsCloudWatchConfigFactory>();
            var config = configFactory.Create();

            var clientFactory = _ioc.GetRequiredService<IAwsCloudWatchClientFactory>();
            _client = clientFactory.Create(config);

            await Task.CompletedTask;
        }

        [Fact]
        public async Task EnsureCheckingForInitializationReturnsExpectedResults()
        {
            var isInitialized = await _sut.IsInitialized();
            isInitialized.ShouldBeFalse();

            await _sut.Initialize(_configuration);

            isInitialized = await _sut.IsInitialized();
            isInitialized.ShouldBeTrue();
        }

        [Fact]
        public async Task EnsureGetDashboardReturnsResults()
        {
            await _sut.Initialize(_configuration);

            var dashboardName = "test dashboard name";

            var request = new GetDashboardRequest
            {
                DashboardName = dashboardName
            };

            var response = await _client.GetDashboardAsync(request);

            response.ShouldNotBeNull();
            response.DashboardName.ShouldBe(dashboardName);
        }

        [Fact]
        public async Task EnsureListDashboardsReturnsResults()
        {
            await _sut.Initialize(_configuration);

            var response = await _client.ListDashboardsAsync();

            response.ShouldNotBeNull();
            response.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task EnsureDescribeAlarmsReturnsResults()
        {
            await _sut.Initialize(_configuration);

            var response = await _client.DescribeAlarmsAsync();

            response.ShouldNotBeNull();
            response.MetricAlarms.ShouldNotBeNull();
            response.MetricAlarms[0].AlarmName.ShouldNotBeNull();
            response.MetricAlarms[0].StateValue.ShouldNotBeNull();
        }

        [Fact]
        public async Task EnsureDescribeAlarmHistoryReturnsResults()
        {
            await _sut.Initialize(_configuration);

            var alarmName = "Test Alarm";

            var response = await _client.DescribeAlarmsHistriesAsync(alarmName);

            response.ShouldNotBeNull();
            response.AlarmHistoryItems.ShouldNotBeNull();
            response.AlarmHistoryItems[0].AlarmName.ShouldBe(alarmName);

        }
    }
}
