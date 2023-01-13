
using S2Cognition.Integrations.Core.Data;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data
{
    public class CloudWatchConfiguration : Configuration
    {
        public string AccountId { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;

        public CloudWatchConfiguration(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
    }
}
