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
            AwsRegion = "fake AwsRegion"
        };

        _sut = _ioc.GetRequiredService<IAmazonWebServicesCloudWatchIntegration>();

        await _sut.Initialize(configuration);

        var configFactory = _ioc.GetRequiredService<IAwsCloudWatchConfigFactory>();
        var config = configFactory.Create();

        var clientFactory = _ioc.GetRequiredService<IAwsCloudWatchClientFactory>();
        _client = clientFactory.Create(config);
    }

    [Theory]
    [InlineData("ALARM", AlarmState.InAlarm)]
    [InlineData("OK", AlarmState.Ok)]
    [InlineData("INSUFFICIENT_DATA", AlarmState.Unknown)]
    [InlineData("GARBAGE", AlarmState.Unknown)]
    public async Task EnsureGetAlarmStatusShouldReturnCorrectAlarmState(string awsState, AlarmState expected)
    {
        var alarmName = "fake Alarm Name";

        A.CallTo(() => _client.DescribeAlarms(A<GetAlarmsStateRequest>.Ignored))
            .Returns(Task.FromResult(new DescribeAlarmsResponse
            {
                MetricAlarms = new List<MetricAlarm> {
                    new MetricAlarm {
                        AlarmName = alarmName,
                        StateValue = new StateValue(awsState)
                    }
                }
            }));

        var response = await _sut.GetAlarmsState(new GetAlarmsStateRequest());
        //{
        //    AlarmNames = new List<string>
        //    {
        //        { alarmName }
        //    },
        //    StateValue = new StateValue(awsState),
        //    MaxRecords = 1
        //});

        response.ShouldNotBeNull();
        response.ShouldNotBeNull();
        response.Alarms?[0].AlarmName.ShouldBe(alarmName);
        response.Alarms?[0].AlarmArn.ShouldNotBeNull();
        response.Alarms?[0].State.ShouldBe(expected);
    }

    //[Fact]
    //public async Task EnsureGetAlarmStatusShouldReturnOkWhenNotInError()
    //{
    //var alarmArn = "fake Alarm ARN";

    //A.CallTo(() => _client.DescribeAlarms()).Returns(new DescribeAlarmsResponse
    //{
    //    MetricAlarms = new List<MetricAlarm>
    //    {
    //        new MetricAlarm
    //        {
    //            AlarmArn = alarmArn,
    //            StateValue = StateValue.OK
    //        }
    //    }
    //});

    //var response = await _sut.GetAlarmsState(new GetAlarmsStateRequest
    //{
    //    AlarmNames = new List<string>
    //    {
    //        { "fake alarm name" }
    //    },
    //    StateValue = StateValue.OK,
    //    MaxRecords = 1
    //});

    //response.ShouldNotBeNull();
    //response.Alarms?[0].AlarmName.ShouldBe(response.Alarms[0].AlarmName);
    //response.Alarms?[0].AlarmArn.ShouldNotBeNull();
    //response.Alarms?[0].State.ShouldBe(AlarmState.Ok);

    //response.Arn.ShouldBe(alarmArn);
    //response.State.ShouldBe(AlarmState.Ok);
    // }

    //[Fact]
    //public async Task EnsureGetAlarmStatusShouldReturnUnknownWhenUnknown()
    //{
    //    var alarmArn = "fake Alarm ARN";

    //A.CallTo(() => _client.DescribeAlarms()).Returns(new DescribeAlarmsResponse
    //{
    //    MetricAlarms = new List<MetricAlarm>
    //    {
    //        new MetricAlarm
    //        {
    //            AlarmArn = alarmArn,
    //            StateValue = StateValue.INSUFFICIENT_DATA
    //        }
    //    }
    //});

    //var response = await _sut.GetAlarmsState(new GetAlarmsStateRequest
    //{
    //    AlarmNames = new List<string>
    //    {
    //        { "fake alarm name" }
    //    },
    //    StateValue = StateValue.INSUFFICIENT_DATA,
    //    MaxRecords = 1
    //});

    //response.ShouldNotBeNull();
    //response.Alarms?[0].AlarmName.ShouldBe(response.Alarms[0].AlarmName);
    //response.Alarms?[0].AlarmArn.ShouldNotBeNull();
    //response.Alarms?[0].State.ShouldBe(AlarmState.Unknown);
    //response.Arn.ShouldBe(alarmArn);
    //response.State.ShouldBe(AlarmState.Unknown);
    //}
}
