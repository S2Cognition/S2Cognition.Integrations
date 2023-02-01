﻿using Amazon.CloudWatch.Model;
using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models;
using S2Cognition.Integrations.AmazonWebServices.Core.Tests;
using S2Cognition.Integrations.Core.Tests;
using Shouldly;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Tests.InternalTests;

public class CloudWatchClientTests : UnitTestBase
{
    private IAwsCloudWatchClient _sut = default!;

    protected override async Task IocSetup(IServiceCollection sc)
    {
        sc.AddAmazonWebServicesCloudWatchIntegration();
        sc.AddFakeAmazonWebServices();
        sc.AddFakeAmazonWebServicesCloudWatch();

        await Task.CompletedTask;
    }

    protected override async Task TestSetup()
    {
        var configFactory = _ioc.GetRequiredService<IAwsCloudWatchConfigFactory>();
        var config = configFactory.Create();

        var clientFactory = _ioc.GetRequiredService<IAwsCloudWatchClientFactory>();
        _sut = clientFactory.Create(config);

        await Task.CompletedTask;
    }

    [Fact]
    public async Task EnsureGetDashboardReturnsResults()
    {
        var dashboardName = "test dashboard name";

        var request = new GetDashboardRequest
        {
            DashboardName = dashboardName
        };

        var response = await _sut.GetDashboard(request);

        response.ShouldNotBeNull();
        response.DashboardName.ShouldBe(dashboardName);
    }

    [Fact]
    public async Task EnsureListDashboardsReturnsResults()
    {
        var response = await _sut.ListDashboards();

        response.ShouldNotBeNull();
        response.Count.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task EnsureDescribeAlarmsReturnsResults()
    {
        var request = new GetAlarmsStateRequest
        {
            AlarmNames = new List<string>
            {
                { "fake alarm name" }
            },
            StateValue = "",
            MaxRecords = 1
        };
        var response = await _sut.DescribeAlarms(request);

        response.ShouldNotBeNull();
        response.MetricAlarms.ShouldNotBeNull();
        response.MetricAlarms[0].AlarmName.ShouldNotBeNull();
        response.MetricAlarms[0].StateValue.ShouldNotBeNull();
    }

    [Fact]
    public async Task EnsureDescribeAlarmHistoryReturnsResults()
    {
        var alarmName = "Test Alarm";

        var response = await _sut.DescribeAlarmsHistories(alarmName);

        response.ShouldNotBeNull();
        response.AlarmHistoryItems.ShouldNotBeNull();
        response.AlarmHistoryItems[0].AlarmName.ShouldBe(alarmName);

    }
}
