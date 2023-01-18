using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data
{
    public interface IAwsCloudWatchClient
    {
        AmazonCloudWatchClient Native { get; }

        Task<GetDashboardResponse> GetDashboardAsync(GetDashboardRequest request);
        Task<List<DashboardEntry>> ListDashboardsAsync();
        Task<DescribeAlarmsResponse> DescribeAlarmsAsync();
        Task<DescribeAlarmHistoryResponse> DescribeAlarmsHistriesAsync(string alarmName);
    }
}

