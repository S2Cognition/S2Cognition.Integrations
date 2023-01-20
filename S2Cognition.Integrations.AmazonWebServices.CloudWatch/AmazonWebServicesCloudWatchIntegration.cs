using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;
using S2Cognition.Integrations.Core;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch;

public interface IAmazonWebServicesCloudWatchIntegration : IIntegration<AmazonWebServicesCloudWatchConfiguration>
{
    Task<GetAlarmStateResponse> GetAlarmState(GetAlarmStateRequest request);
}


internal class AmazonWebServicesCloudWatchIntegration : Integration<AmazonWebServicesCloudWatchConfiguration>, IAmazonWebServicesCloudWatchIntegration
{
    internal AmazonWebServicesCloudWatchIntegration(IServiceProvider serviceProvider)
    : base(serviceProvider)
    {
    }

    public async Task<GetAlarmStateResponse> GetAlarmState(GetAlarmStateRequest request)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}

