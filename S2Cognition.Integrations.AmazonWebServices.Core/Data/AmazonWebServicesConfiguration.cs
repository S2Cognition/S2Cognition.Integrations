using S2Cognition.Integrations.Core.Data;

namespace S2Cognition.Integrations.AmazonWebServices.Core.Data;

public class AmazonWebServicesConfiguration : Configuration
{
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string AwsRegion { get; set; } = string.Empty;

    public AmazonWebServicesConfiguration(IServiceProvider ioc)
        : base(ioc)
    {
    }
}