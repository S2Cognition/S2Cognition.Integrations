using S2Cognition.Integrations.AmazonWebServices.Core.Data;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data
{
    public class AmazonWebServicesCloudWatchConfiguration : AmazonWebServicesConfiguration
    {
        public string ServiceUrl { get; set; } = string.Empty;

        public AmazonWebServicesCloudWatchConfiguration(IServiceProvider serviceProvider)
    : base(serviceProvider)
        {
        }
    }
}
