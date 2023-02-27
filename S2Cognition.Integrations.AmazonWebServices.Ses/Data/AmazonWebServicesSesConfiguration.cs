using S2Cognition.Integrations.AmazonWebServices.Core.Data;

namespace S2Cognition.Integrations.AmazonWebServices.Ses.Data;

public class AmazonWebServicesSesConfiguration : AmazonWebServicesConfiguration
{
    public AmazonWebServicesSesConfiguration(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }
}
