using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;
using S2Cognition.Integrations.Core;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch;

public interface IAmazonWebServicesCloudWatchIntegration : IIntegration<AmazonWebServicesCloudWatchConfiguration>
{

}


public class AmazonWebServicesCloudWatchIntegration : Integration<AmazonWebServicesCloudWatchConfiguration>, IAmazonWebServicesCloudWatchIntegration
{
    public AmazonWebServicesCloudWatchIntegration(IServiceProvider serviceProvider)
    : base(serviceProvider)
    {
    }
}

