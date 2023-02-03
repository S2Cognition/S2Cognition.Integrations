using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Tests.Fakes;
using S2Cognition.Integrations.AmazonWebServices.Core.Tests;
using S2Cognition.Integrations.Core.Tests;
using Shouldly;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Tests.InternalTests;

public class CloudWatchTests : UnitTestBase
{
    private IAmazonWebServicesCloudWatchIntegration _sut = default!;
    private IFakeAwsCloudWatchClient _client = default!;

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
            AwsRegion = "fake AwsRegion"
        };

        _sut = _ioc.GetRequiredService<IAmazonWebServicesCloudWatchIntegration>();

        await _sut.Initialize(configuration);

        var configFactory = _ioc.GetRequiredService<IAwsCloudWatchConfigFactory>();
        var config = configFactory.Create();

        var clientFactory = _ioc.GetRequiredService<IAwsCloudWatchClientFactory>();
        _client = clientFactory.Create(config) as IFakeAwsCloudWatchClient ?? throw new InvalidOperationException();
    }

    [Theory]
    [InlineData("ALARM", AlarmState.InAlarm)]
    [InlineData("OK", AlarmState.Ok)]
    [InlineData("INSUFFICIENT_DATA", AlarmState.Unknown)]
    //[InlineData("GARBAGE", AlarmState.Unknown)]
    public async Task EnsureGetAlarmReturnsCorrectAlarmState(string awsState, AlarmState expected)
    {
        var alarmName = "fake Alarm Name";
        var arnName = "fake Arn Name";

        _client.ExpectsAlarms(new DescribeAlarmsResponse
        {
            MetricAlarms = new List<MetricAlarm> {
                    new MetricAlarm {
                        AlarmName = alarmName,
                        AlarmArn = arnName,
                        StateValue = new StateValue(awsState)
                    }
                }
        });

        var response = await _sut.GetAlarmsState(new GetAlarmsStateRequest
        {
            AlarmNames = new List<string>
            {
                { alarmName }
            },
            StateValue = new StateValue(awsState),
            MaxRecords = 1
        });

        response.ShouldNotBeNull();
        response.Alarms?[0].AlarmName.ShouldBe(alarmName);
        response.Alarms?[0].AlarmArn.ShouldNotBeNull();
        response.Alarms?[0].State.ShouldBe(expected);
    }
}
