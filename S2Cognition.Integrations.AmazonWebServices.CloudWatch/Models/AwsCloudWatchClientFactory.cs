using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Models
{
    public class AwsCloudWatchClientFactory : IAwsCloudWatchClientFactory
    {
        public IAwsCloudWatchClient Create(IAwsCloudWatchConfig config)
        {
            return new AwsCloudWatchClient(config);
        }

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
    }

}
