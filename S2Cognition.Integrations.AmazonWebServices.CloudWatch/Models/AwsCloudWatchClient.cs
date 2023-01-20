using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models;

internal interface IAwsCloudWatchClient
{
    AmazonCloudWatchClient Native { get; }

    Task<GetDashboardResponse> GetDashboard(GetDashboardRequest request);
    Task<ICollection<DashboardEntry>> ListDashboards();
    Task<DescribeAlarmsResponse> DescribeAlarms();
    Task<DescribeAlarmHistoryResponse> DescribeAlarmsHistories(string alarmName);
}

internal class AwsCloudWatchClient : IAwsCloudWatchClient
{
    private readonly AmazonCloudWatchClient _client;

    public AmazonCloudWatchClient Native => _client;

    internal AwsCloudWatchClient(IAwsCloudWatchConfig config)
    {
        _client = new AmazonCloudWatchClient(config.Native);
    }

    public async Task<GetDashboardResponse> GetDashboard(GetDashboardRequest request)
    {
        return await Native.GetDashboardAsync(request);
    }

    public async Task<ICollection<DashboardEntry>> ListDashboards()
    {
        var response = await Native.ListDashboardsAsync(new ListDashboardsRequest());

        return response.DashboardEntries;
    }

    public async Task<DescribeAlarmsResponse> DescribeAlarms()
    {
        return await Native.DescribeAlarmsAsync();
    }

    public async Task<DescribeAlarmHistoryResponse> DescribeAlarmsHistories(string alarmName)
    {
        var request = new DescribeAlarmHistoryRequest
        {
            AlarmName = alarmName,
            EndDateUtc = DateTime.Today,
            HistoryItemType = HistoryItemType.Action,
            MaxRecords = 1,
            StartDateUtc = DateTime.Today.Subtract(TimeSpan.FromDays(30)),
        };

        return await Native.DescribeAlarmHistoryAsync(request);
    }
}
