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
    Task<GetAlarmsStateResponse> GetAlarmsState(GetAlarmsStateRequest request);
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

    public async Task<GetAlarmsStateResponse> GetAlarmsState(GetAlarmsStateRequest request)
    {
        var response = new List<GetAlarmStateDetails>();
        DescribeAlarmsResponse returnedAlarmsDetails;

        returnedAlarmsDetails = await Client.DescribeAlarms(request);

        AlarmState alarmState;

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

