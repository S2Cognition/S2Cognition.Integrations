using S2Cognition.Integrations.AmazonWebServices.CloudWatch.Data;
using S2Cognition.Integrations.Core;

namespace S2Cognition.Integrations.AmazonWebServices.CloudWatch;

public interface ICloudWatchIntegration : IIntegration<CloudWatchConfiguration>
{

}

internal class CloudWatchIntegration : Integration<CloudWatchConfiguration>, ICloudWatchIntegration
{
    public CloudWatchIntegration(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    public override async Task Initialize(CloudWatchConfiguration configuration)
    {
        await base.Initialize(configuration);


    }
    //public static async Task Main()
    //{
    //    IAmazonCloudWatch cwClient = new AmazonCloudWatchClient();
    //    string dashboardName = "CloudWatch-Default";

    //    var body = await GetDashboardAsync(cwClient, dashboardName);

    //    Console.WriteLine(body);
    //}

    /// <summary>
    /// Get the json that represents the dashboard.
    /// </summary>
    /// <param name="client">An initialized CloudWatch client.</param>
    /// <param name="dashboardName">The name of the dashboard.</param>
    /// <returns>The string containing the json value describing the
    /// contents and layout of the CloudWatch dashboard.</returns>
    //public static async Task<List<DashboardEntry>> ListDashboardsAsync(IAmazonCloudWatch client)
    //{
    //    var response = await client.ListDashboardsAsync(new ListDashboardsRequest());
    //    return response.DashboardEntries;
    //}
}



