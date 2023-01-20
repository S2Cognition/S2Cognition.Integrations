using S2Cognition.Integrations.AmazonWebServices.Core.Data;
using S2Cognition.Integrations.Core;

namespace S2Cognition.Integrations.AmazonWebServices.Core;

public interface IAmazonWebServicesIntegration : IIntegration<AmazonWebServicesConfiguration>
{
}

internal class AmazonWebServicesIntegration : Integration<AmazonWebServicesConfiguration>, IAmazonWebServicesIntegration
{
    internal AmazonWebServicesIntegration(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }
}
