using S2Cognition.Integrations.Zoom.Core.Tests.Fakes;

namespace S2Cognition.Integrations.Zoom.Core.Tests;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFakeZoomIntegration(this IServiceCollection sc)
    {
        return sc.AddSingleton<IZoomIntegration, FakeZoomIntegration>();
    }
}
