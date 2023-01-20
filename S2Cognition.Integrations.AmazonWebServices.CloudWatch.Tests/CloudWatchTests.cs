using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models;
using S2Cognition.Integrations.AmazonWebServices.Core.Tests;
using S2Cognition.Integrations.Core.Tests;
using Shouldly;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Tests.InternalTests;

public class CloudWatchTests : UnitTestBase
{
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
        var configuration = new AmazonWebServicesCloudWatchConfiguration(_ioc)
        {
            AccessKey = "fake AccessKey",
            SecretKey = "fake SecretKey",
            AwsRegion = "fake AwsRegion",
            ServiceUrl = "fake ServiceUrl"
        };

        _sut = _ioc.GetRequiredService<IAmazonWebServicesCloudWatchIntegration>();

        await _sut.Initialize(configuration);

        var configFactory = _ioc.GetRequiredService<IAwsCloudWatchConfigFactory>();
        var config = configFactory.Create();

        var clientFactory = _ioc.GetRequiredService<IAwsCloudWatchClientFactory>();
        _client = clientFactory.Create(config);
    }

    [Fact]
    public async Task EnsureGetAlarmStatusShouldReturnInAlarmWhenInError()
    {
        var alarmArn = "fake Alarm ARN";

        A.CallTo(() => _client.DescribeAlarms()).Returns(new DescribeAlarmsResponse
        {
            MetricAlarms = new List<MetricAlarm>
            {
                new MetricAlarm
                {
                    AlarmArn = alarmArn,
                    StateValue = StateValue.ALARM
                }
            }
        });

        var response = await _sut.GetAlarmState(new GetAlarmStateRequest
        {
            Arn = alarmArn
        });

        response.ShouldNotBeNull();
        response.Arn.ShouldBe(alarmArn);
        response.State.ShouldBe(AlarmState.InAlarm);
    }

    [Fact]
    public async Task EnsureGetAlarmStatusShouldReturnOkWhenNotInError()
    {
        var alarmArn = "fake Alarm ARN";

        A.CallTo(() => _client.DescribeAlarms()).Returns(new DescribeAlarmsResponse
        {
            MetricAlarms = new List<MetricAlarm>
            {
                new MetricAlarm
                {
                    AlarmArn = alarmArn,
                    StateValue = StateValue.OK
                }
            }
        });

        var response = await _sut.GetAlarmState(new GetAlarmStateRequest
        {
            Arn = alarmArn
        });

        response.ShouldNotBeNull();
        response.Arn.ShouldBe(alarmArn);
        response.State.ShouldBe(AlarmState.Ok);
    }

    [Fact]
    public async Task EnsureGetAlarmStatusShouldReturnUnknownWhenUnknown()
    {
        var alarmArn = "fake Alarm ARN";

        A.CallTo(() => _client.DescribeAlarms()).Returns(new DescribeAlarmsResponse
        {
            MetricAlarms = new List<MetricAlarm>
            {
                new MetricAlarm
                {
                    AlarmArn = alarmArn,
                    StateValue = StateValue.INSUFFICIENT_DATA
                }
            }
        });

        var response = await _sut.GetAlarmState(new GetAlarmStateRequest
        {
            Arn = alarmArn
        });

        response.ShouldNotBeNull();
        response.Arn.ShouldBe(alarmArn);
        response.State.ShouldBe(AlarmState.Unknown);
    }
}
