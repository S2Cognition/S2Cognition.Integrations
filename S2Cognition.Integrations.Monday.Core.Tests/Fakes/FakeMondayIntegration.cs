using GraphQL.Client.Http;
using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.Core.Tests.Fakes;
using S2Cognition.Integrations.Monday.Core.Data;
using S2Cognition.Integrations.Monday.Core.Models;
using System.Net;

namespace S2Cognition.Integrations.Monday.Core.Tests.Fakes;

public class FakeMondayIntegration : IMondayIntegration
{
    private readonly IFakeGraphQlHttpClient _graphQlHttpClient;
    private readonly IMondayIntegration _integration;

    public FakeMondayIntegration(IServiceProvider ioc)
    {
        _graphQlHttpClient = ioc.GetRequiredService<IFakeGraphQlHttpClient>();

        _integration = new MondayIntegration(ioc);
    }

    public async Task Initialize(MondayConfiguration configuration)
    {
        await _integration.Initialize(configuration);
    }

    public async Task<bool> IsInitialized()
    {
        return await _integration.IsInitialized();
    }

    public async Task<GetUsersResponse> GetUsers()
    {
        return await _integration.GetUsers();
    }

    public async Task<GetItemsResponse> GetItems(GetItemsRequest request)
    {
        return await _integration.GetItems(request);
    }

    public async Task<CreateItemResponse> CreateItem(CreateItemRequest request)
    {
        return await _integration.CreateItem(request);
    }

    public async Task<CreateSubItemResponse> CreateSubItem(CreateSubItemRequest request)
    {
        return await _integration.CreateSubItem(request);
    }
}
