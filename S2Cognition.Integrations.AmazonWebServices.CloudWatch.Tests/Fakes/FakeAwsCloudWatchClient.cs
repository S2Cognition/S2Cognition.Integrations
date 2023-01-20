using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
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

    public async Task<DescribeAlarmsResponse> DescribeAlarms()
    {
        return await Task.FromResult(new DescribeAlarmsResponse
        {
            MetricAlarms = new List<MetricAlarm>
            {
                new MetricAlarm
                { 
                    AlarmName = "Unknown",
                    StateValue = StateValue.INSUFFICIENT_DATA
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
