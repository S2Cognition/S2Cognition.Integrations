using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models;

internal interface IAwsCloudWatchClient
{
    AmazonCloudWatchClient Native { get; }

    Task<GetDashboardResponse> GetDashboard(GetDashboardRequest request);
    Task<ICollection<DashboardEntry>> ListDashboards();
    Task<DescribeAlarmsResponse> DescribeAlarms(GetAlarmsStateRequest req);
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

    public async Task<DescribeAlarmsResponse> DescribeAlarms(GetAlarmsStateRequest req)
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

        var returnedAlarms = await Native.DescribeAlarmsAsync(request);
        return returnedAlarms;
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
