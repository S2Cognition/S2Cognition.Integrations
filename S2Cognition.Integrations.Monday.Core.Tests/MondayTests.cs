using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.Core.Tests;
using S2Cognition.Integrations.Core.Tests.Fakes;
using S2Cognition.Integrations.Monday.Core.Data;
using Shouldly;

namespace S2Cognition.Integrations.Monday.Core.Tests;

public class MondayTests : UnitTestBase
{
    private IMondayIntegration _sut = default!;
    private IFakeGraphQlHttpClient _graphQl = default!;

    protected override async Task IocSetup(IServiceCollection sc)
    {
        sc.AddFakeClients();
        sc.AddFakeMondayIntegration();
        await Task.CompletedTask;
    }

    protected override async Task TestSetup()
    {
        var configuration = new MondayConfiguration(_ioc)
        {
            ApiKey = "fake api key"
        };

        _sut = _ioc.GetRequiredService<IMondayIntegration>();
        await _sut.Initialize(configuration);

        _graphQl = _ioc.GetRequiredService<IFakeGraphQlHttpClient>();
    }

    [Fact]
    public async Task EnsureGetUsersIsCallable()
    {
        _graphQl.ExpectQuery(new[]
        {
            new[]{ "query", "query { users { id name email url photo_original title birthday country_code location time_zone_identifier phone mobile_phone is_guest is_pending enabled created_at } }" }
        }, new Models.Responses.GetUsersResponse());

        var users = await _sut.GetUsers(new GetUsersRequest());

        users.ShouldNotBeNull();
    }

    [Fact]
    public async Task EnsureGetItemsIsCallable()
    {
        _graphQl.ExpectQuery(new[]
        {
            new [] { "query", "query request($id:Int) { boards(ids:[$id]) { items(limit:100000) { id name group { id title } column_values { id title value type text } } } }" },
            new [] { "variables", "{ id = 1234 }" }
        }, new Models.Responses.GetBoardItemsResponse(new[]
        {
            new Models.Board
            {
                Items = new[]
                {
                    new Models.Item
                    {
                        Id = 2345,
                        Name = "filter by name",
                        ColumnValues = new[]
                        {
                            new Models.ColumnValue { Id = "first_name", Name = "A", Value = "a" },
                            new Models.ColumnValue { Id = "lastname", Name = "A", Value = "a" },
                            new Models.ColumnValue { Id = "comments0", Name = "A", Value = "a" }
                        }
                    }
                }
            }
        }));

        var options = new GetItemsRequest 
        {
            BoardId = 1234,
            Name = "filter by name", // optional
            State = ItemState.Active // optional
        };

        var response = await _sut.GetItems(options);

        response.ShouldNotBeNull();

        response.Items.First().Name.ShouldNotBeNull();
        response.Items.First().State.ShouldBe(ItemState.Active);

        response.Items.First().GetColumn("first_name").Value.ShouldNotBeNull();
        response.Items.First().GetColumn("lastname").Value.ShouldNotBeNull();
        response.Items.First().GetColumn("comments0").Value.ShouldNotBeNull();
    }

    [Fact]
    public async Task EnsureCreateItemIsCallable()
    {
        var options = new CreateItemRequest
        {
            BoardId = 1234,
            Name = "item name / title",
            GroupId = "group-id"
        };

        options.SetColumnById("first_name", "some value");
        options.SetColumnById("lastname", "some value");
        options.SetColumnById("comments0", "some value");

        var item = await _sut.CreateItem(options);

        item.ShouldNotBeNull();
    }

    [Fact]
    public async Task EnsureCreateSubItemIsCallable()
    {
        var options = new CreateSubItemRequest 
        {
            ItemId = 1234,
            Name = "name data"
        };

        var item = await _sut.CreateSubItem(options);

        item.ShouldNotBeNull();
    }
}
