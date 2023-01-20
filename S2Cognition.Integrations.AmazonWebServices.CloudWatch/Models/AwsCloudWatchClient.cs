using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models;

internal interface IAwsCloudWatchClient
{
    AmazonCloudWatchClient Native { get; }

    Task<GetDashboardResponse> GetDashboardAsync(GetDashboardRequest request);
    Task<List<DashboardEntry>> ListDashboardsAsync();
    Task<DescribeAlarmsResponse> DescribeAlarmsAsync();
    Task<DescribeAlarmHistoryResponse> DescribeAlarmsHistriesAsync(string alarmName);
}

internal class AwsCloudWatchClient : IAwsCloudWatchClient
{
    private readonly AmazonCloudWatchClient _client;

    public AmazonCloudWatchClient Native => _client;

    internal AwsCloudWatchClient(IAwsCloudWatchConfig config)
    {
        _client = new AmazonCloudWatchClient(config.Native);
    }

    public Task<GetDashboardResponse> GetDashboardAsync(GetDashboardRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<List<DashboardEntry>> ListDashboardsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<DescribeAlarmsResponse> DescribeAlarmsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<DescribeAlarmHistoryResponse> DescribeAlarmsHistriesAsync(string alarmName)
    {
        throw new NotImplementedException();
    }
}
