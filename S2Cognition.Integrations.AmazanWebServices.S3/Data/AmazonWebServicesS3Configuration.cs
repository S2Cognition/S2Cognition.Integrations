using S2Cognition.Integrations.AmazonWebServices.Core.Data;

namespace S2Cognition.Integrations.AmazonWebServices.S3.Data;

public class AmazonWebServicesS3Configuration : AmazonWebServicesConfiguration
{
    public string ServiceUrl { get; set; } = string.Empty;

    public AmazonWebServicesS3Configuration(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }
}
