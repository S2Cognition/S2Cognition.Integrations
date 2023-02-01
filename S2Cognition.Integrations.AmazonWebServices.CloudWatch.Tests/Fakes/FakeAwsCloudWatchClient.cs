using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Tests.Fakes;

internal class FakeAwsCloudWatchClient : IAwsCloudWatchClient
{
    public AmazonCloudWatchClient Native => throw new NotImplementedException();

    public async Task<GetDashboardResponse> GetDashboard(GetDashboardRequest request)
    {
        return await Task.FromResult(new GetDashboardResponse
        {
            DashboardName = request.DashboardName
        });
    }

    public async Task<ICollection<DashboardEntry>> ListDashboards()
    {
        return await Task.FromResult(new List<DashboardEntry>
        {
            new DashboardEntry()
        });
    }

    public async Task<GetAlarmsStateResponse> DescribeAlarms(GetAlarmsStateRequest req)
    {
        return await Task.FromResult(new GetAlarmsStateResponse
        {
            Alarms
            Alarms = new List<GetAlarmStateDetails>
            {
                new MetricAlarm
                {
                    AlarmArn = "Test Alarm Arn",
                    AlarmName = "Test Alarm Name",
                    AlarmDescription = "This is a test description",
                    StateValue = StateValue.OK,

                }
            }
        });
    }

    public async Task<DescribeAlarmHistoryResponse> DescribeAlarmsHistories(string alarmName)
    {
        return await Task.FromResult(new DescribeAlarmHistoryResponse
        {
            AlarmHistoryItems = new List<AlarmHistoryItem>
            {
                new AlarmHistoryItem
                {
                    AlarmName = alarmName
                }
            }
        });
    }
}
