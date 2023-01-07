using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.NetSuite.Core;
using S2Cognition.Integrations.NetSuite.Core.Data;
using System.Text.Json;

// This is just a simple example of a local-development harness.   Please be sure not to include this in any commits,
// as it will likely contain company secrets in the configuration block.

var serviceCollection = new ServiceCollection();
serviceCollection.AddNetSuiteIntegration();

var serviceProvider = serviceCollection.BuildServiceProvider();

var configuration = new NetSuiteConfiguration(serviceProvider)
{
    AccountId = "8116938_SB1",
    ConsumerKey = "926b309fe26ae88ce87660a06f09338927795890eaf17a216d700acb72e2abc7",
    ConsumerSecret = "777b735af2fb2615e4036102d5561905d1e9c966e3811b44f68309e98b204c4c",
    TokenId = "29017d215bd21ef54cd85c4aaffe62230fc6c4a601db8b3a0422bf9dc72a9d29",
    TokenSecret = "13b2a533369613b572403ebc7ca4cab08f03d334d505d17908b492e9f10235b9"
};

var sut = serviceProvider.GetRequiredService<INetSuiteIntegration>();
await sut.Initialize(configuration);

var res = await sut.ListProspects();

Console.WriteLine(JsonSerializer.Serialize(res));
