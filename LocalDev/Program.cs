using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.Zoom.Core;
using S2Cognition.Integrations.Zoom.Core.Data;
using System.Text.Json;

// This is just a simple example of a local-development harness.   Please be sure not to include this in any commits,
// as it will likely contain company secrets in the configuration block.

var serviceCollection = new ServiceCollection();
serviceCollection.AddZoomIntegration();

var serviceProvider = serviceCollection.BuildServiceProvider();

var configuration = new ZoomConfiguration(serviceProvider)
{
    AccountId = "your account id",
    ClientId = "your client id",
    ClientSecret = "your client secret"
};

var sut = serviceProvider.GetRequiredService<IZoomIntegration>();
await sut.Initialize(configuration);

var users = await sut.GetUsers();

Console.WriteLine(JsonSerializer.Serialize(users));
