using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;
using S2Cognition.Integrations.Core;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch
{
    public interface ICloudWatchIntegration : IIntegration<CloudWatchConfiguration>
    {
        Task<string> GetDashboard(IAmazonCloudWatch client, string dashboardName);
    }

    internal class CloudWatchIntegration : Integration<CloudWatchConfiguration>, ICloudWatchIntegration
    {
        public CloudWatchIntegration(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        private IAwsCloudWatchClient? _client = null;
        private async Task<IAwsCloudWatchClient> Client()
        {
            if (_client == null)
            {

                var clientFactory = _ioc.GetRequiredService<IAwsCloudWatchClientFactory>();
                _client = clientFactory.Create();
            }

            return await Task.FromResult(_client);
        }

        public async Task<string> GetDashboard(string dashboardName)
        {
            var request = new GetDashboardRequest
            {
                DashboardName = dashboardName,
            };

            var response = await Client.GetDashboard(request);

            return response.DashboardBody;
        }
    }
}

//interface IAwsCloudWatchClientFactory
//{
//    IAwsCloudWatchClient Create();
//}

//public class AwsCloudWatchClientFactory : IAwsCloudWatchClientFactory
//{
//    public IAwsCloudWatchClient Create()
//    {
//        return new AwsCloudWatchClient();
//    }
//}

//public class FakeAwsCloudWatchClientFactory : IAwsCloudWatchClientFactory
//{
//    public IAwsCloudWatchClient Create()
//    {
//        return new FakeAwsCloudWatchClient();
//    }
//}
