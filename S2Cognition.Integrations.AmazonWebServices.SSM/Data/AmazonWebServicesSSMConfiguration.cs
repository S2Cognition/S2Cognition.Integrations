using S2Cognition.Integrations.AmazonWebServices.Core.Data;

namespace S2Cognition.Integrations.AmazonWebServices.Ssm.Data;

public class AmazonWebServicesSsmConfiguration : AmazonWebServicesConfiguration
{
    public string ServiceUrl { get; set; } = string.Empty;

    public AmazonWebServicesSsmConfiguration(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }
}
