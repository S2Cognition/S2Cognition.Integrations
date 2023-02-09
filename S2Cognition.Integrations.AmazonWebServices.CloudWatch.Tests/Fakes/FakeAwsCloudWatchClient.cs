using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Tests.Fakes;

internal interface IFakeAwsCloudWatchClient
{
    void ExpectsAlarms(DescribeAlarmsResponse expectedAlarms);
}

internal class FakeAwsCloudWatchClient : IAwsCloudWatchClient, IFakeAwsCloudWatchClient
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

    private DescribeAlarmsResponse? _expectedAlarms = null;
    public void ExpectsAlarms(DescribeAlarmsResponse expectedAlarms)
    {
        _expectedAlarms = expectedAlarms;
    }

    public async Task<DescribeAlarmsResponse> DescribeAlarms(GetAlarmsStateRequest req)
    {
        if (_expectedAlarms == null)
            throw new InvalidOperationException("Expectations were not set on fake.");

        return await Task.FromResult(_expectedAlarms);
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
