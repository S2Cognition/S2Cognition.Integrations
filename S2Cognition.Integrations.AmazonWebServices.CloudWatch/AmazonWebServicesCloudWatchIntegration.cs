using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models;
using S2Cognition.Integrations.AmazonWebServices.Core.Models;
using S2Cognition.Integrations.Core;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch;

public interface IAmazonWebServicesCloudWatchIntegration : IIntegration<AmazonWebServicesCloudWatchConfiguration>
{
    Task<GetAlarmsStateResponse> GetAlarmsState(GetAlarmsStateRequest req);
}


internal class AmazonWebServicesCloudWatchIntegration : Integration<AmazonWebServicesCloudWatchConfiguration>, IAmazonWebServicesCloudWatchIntegration
{
    private IAwsCloudWatchClient? _client;
    private IAwsCloudWatchClient Client
    {
        get
        {
            if (_client == null)
            {
                var factory = _ioc.GetRequiredService<IAwsCloudWatchClientFactory>();

                var regionFactory = _ioc.GetRequiredService<IAwsRegionFactory>();

                _client = factory.Create(new AwsCloudWatchConfig
                {
                    ServiceUrl = Configuration.ServiceUrl,
                    RegionEndpoint = regionFactory.Create(Configuration.AwsRegion).Result
                });
            }

            return _client;
        }
    }

    internal AmazonWebServicesCloudWatchIntegration(IServiceProvider serviceProvider)
    : base(serviceProvider)
    {
    }

    public async Task<GetAlarmsStateResponse> GetAlarmsState(GetAlarmsStateRequest req)
    {
        var request = new DescribeAlarmsRequest();

        if (req.AlarmNames != null)
            request.AlarmNames = req.AlarmNames;

        if (req.AlarmNamePrefix != null)
            request.AlarmNamePrefix = req.AlarmNamePrefix;

        if (req.StateValue != null)
            request.StateValue = req.StateValue;

        if (req.MaxRecords.HasValue)
            request.MaxRecords = req.MaxRecords.Value;

        var returnedAlarmsDetails = await Client.DescribeAlarms(request);

        AlarmState alarmState;
        var response = new List<GetAlarmStateDetails>();

        do
        {
            foreach (var alarm in returnedAlarmsDetails.MetricAlarms)
            {
                if (alarm.AlarmName != null)
                {
                    if (alarm.StateValue == StateValue.OK)
                    {
                        alarmState = AlarmState.Ok;
                    }
                    else if (alarm.StateValue == StateValue.ALARM)
                    {
                        alarmState = AlarmState.InAlarm;
                    }
                    else
                    {
                        alarmState = AlarmState.Unknown;
                    }

                    response.Add(new GetAlarmStateDetails
                    {
                        AlarmName = alarm.AlarmName,
                        AlarmArn = alarm.AlarmArn,
                        AlarmDescription = alarm.AlarmDescription,
                        State = alarmState
                    });
                }
            }
        } while (!string.IsNullOrEmpty(returnedAlarmsDetails.NextToken));

        return new GetAlarmsStateResponse { Alarms = response };
    }

}

