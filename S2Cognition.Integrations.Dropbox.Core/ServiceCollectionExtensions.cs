namespace S2Cognition.Integrations.Dropbox.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDropboxIntegration(this IServiceCollection sc)
    {
        return sc.AddSingleton<IDropboxIntegration>(_ => new DropboxIntegration(_))
            .AddSingleton<IDropboxNativeClient>(_ => new DropboxNativeClient());
    }
}
