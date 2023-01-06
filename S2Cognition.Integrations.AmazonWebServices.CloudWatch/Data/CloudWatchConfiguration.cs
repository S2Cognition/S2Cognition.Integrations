using S2Cognition.Integrations.Core.Data;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;

public class CloudWatchConfiguration : Configuration
{
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string AwsRegion { get; set; } = string.Empty;


    public CloudWatchConfiguration(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }
}

