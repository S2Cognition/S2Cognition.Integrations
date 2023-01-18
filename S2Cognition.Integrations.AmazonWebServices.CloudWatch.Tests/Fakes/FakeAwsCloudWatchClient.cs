using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Tests.Fakes
{
    public class FakeAwsCloudWatchClient : IAwsCloudWatchClient
    {
        public AmazonCloudWatchClient Native => throw new NotImplementedException();

        public async Task<GetDashboardResponse> GetDashboardAsync(GetDashboardRequest request)
        {
            GetDashboardResponse response = default!;

            response = await Native.GetDashboardAsync(request);

            return response;
        }

        public async Task<List<DashboardEntry>> ListDashboardsAsync()
        {
            var response = await Native.ListDashboardsAsync(new ListDashboardsRequest());

            return response.DashboardEntries;
        }

        public async Task<DescribeAlarmsResponse> DescribeAlarmsAsync()
        {
            var response = await Native.DescribeAlarmsAsync();
            return response;
        }

        public async Task<DescribeAlarmHistoryResponse> DescribeAlarmsHistriesAsync(string alarmName)
        {
            var request = new DescribeAlarmHistoryRequest
            {
                AlarmName = alarmName,
                EndDateUtc = DateTime.Today,
                HistoryItemType = HistoryItemType.Action,
                MaxRecords = 1,
                StartDateUtc = DateTime.Today.Subtract(TimeSpan.FromDays(30)),
            };

            var response = await Native.DescribeAlarmHistoryAsync(request);

            return response;
        }
    }
}
