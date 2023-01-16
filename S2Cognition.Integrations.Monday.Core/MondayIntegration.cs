using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.Core;
using S2Cognition.Integrations.Core.Models;
using S2Cognition.Integrations.Monday.Core.Data;
using S2Cognition.Integrations.Monday.Core.Models;
using S2Cognition.Integrations.Monday.Core.Models.Options;
using System.Net.Http.Headers;

namespace S2Cognition.Integrations.Monday.Core;

public interface IMondayIntegration : IIntegration<MondayConfiguration>
{
    Task<GetUsersResponse> GetUsers();
    Task<GetItemsResponse> GetItems(GetItemsRequest request);
    Task<CreateItemResponse> CreateItem(CreateItemRequest request);
    Task<CreateSubItemResponse> CreateSubItem(CreateSubItemRequest request);
}

public class MondayIntegration :  Integration<MondayConfiguration>, IMondayIntegration
{
    private MondayClient? _client;
    private MondayClient Client 
    {
        get 
        {
            if (_client == null)
            {
                var graphQlClientFactory = _ioc.GetRequiredService<IGraphQlHttpClientFactory>();
                var graphQlClient = graphQlClientFactory.Create();

                graphQlClient.BaseUrl = "https://api.monday.com/v2/";
                graphQlClient.Serializer = new NewtonsoftJsonSerializer();
                graphQlClient.Authorization = Configuration.ApiKey;

                _client = new MondayClient(graphQlClient);
            }

            return _client;
        }
    }

    public MondayIntegration(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    public async Task<GetUsersResponse> GetUsers()
    {
        var users = await Client.GetUsers();
        var orderedUsers = (users ?? Array.Empty<User>()).OrderBy(_ => _.Email);

        var response = new GetUsersResponse();
        foreach (var user in orderedUsers)
        {
            if (user.Id.HasValue)
            {
                response.Add(new UserRecord
                {
                    Id = user.Id.Value,
                    Email = user.Email ?? String.Empty,
                    Name = user.Name ?? $"User #{user.Id.Value}",
                    Url = user.Url,
                    Photo = user.Photo,
                    Title = user.Title,
                    Birthday = user.Birthday,
                    CountryCode = user.CountryCode,
                    Location = user.Location,
                    TimeZoneIdentifier = user.TimeZoneIdentifier,
                    Phone = user.Phone,
                    MobilePhone = user.MobilePhone,
                    IsGuest = user.IsGuest ?? true,
                    IsPending = user.IsPending ?? false,
                    IsEnabled = user.IsEnabled ?? false,
                    CreatedAt = user.CreatedAt ?? DateTime.Now,
                });
            }
        }

        return response;
    }

    public async Task<GetItemsResponse> GetItems(GetItemsRequest request)
    {
        var options = new Models.Requests.GetItemsRequest(request.BoardId)
        {
            ItemOptions = new ItemOptions(Models.Requests.RequestMode.Minimum)
            {
                IncludeName = true,
                
                IncludeColumnValues = true,
                ColumnValueOptions = new ColumnValueOptions(Models.Requests.RequestMode.Minimum)
                {
                    IncludeTitle = true,
                    IncludeText = true,
                    IncludeType = true,
                    IncludeValue = true
                },

                IncludeGroup = true,
                GroupOptions = new GroupOptions(Models.Requests.RequestMode.Minimum)
                { 
                    IncludeTitle = true
                }
            }
        };

        if (request.State.HasValue)
        {
            options.FilterState = request.State.Value switch 
            { 
                ItemState.Active => Models.Requests.StateFilter.Active,
                _ => Models.Requests.StateFilter.None
            };
        }

        var resp = await Client.GetItems(options);

        var items = resp.Data ?? Array.Empty<Item>();
        if (!String.IsNullOrWhiteSpace(request.Name))
        {
            items = items.Where(_ => String.Equals(_.Name, request.Name, StringComparison.InvariantCultureIgnoreCase));
        }

        var orderedItems = items.OrderBy(_ => _.UpdatedAt);

        var response = new GetItemsResponse();
        foreach (var item in orderedItems)
        {
            if (item.Id.HasValue)
            {
                var record = new ItemRecord
                {
                    Id = item.Id.Value,
                    Name = item.Name ?? $"Item #{item.Id.Value}",
                    State = ItemState.Active,
                    CreatedAt = item.CreatedAt ?? DateTime.Now,
                    UpdatedAt = item.UpdatedAt ?? DateTime.Now,
                    Group = new GroupRecord
                    {
                        Id = item.Group?.Id ?? String.Empty,
                        Name = item.Group?.Name ?? String.Empty
                    }
                };

                foreach (var cv in item.ColumnValues ?? Array.Empty<ColumnValue>())
                {
                    if (!String.IsNullOrWhiteSpace(cv.Id)
                        && !String.IsNullOrWhiteSpace(cv.Name)
                        && !String.IsNullOrWhiteSpace(cv.Value))
                    {
                        record.SetColumn(cv.Id, cv.Name, cv.Value);
                    }
                }

                response.Add(record);
            }
        }

        return response;
    }

    public async Task<CreateItemResponse> CreateItem(CreateItemRequest request)
    {
        return await Task.FromResult(new CreateItemResponse());
    }

    public async Task<CreateSubItemResponse> CreateSubItem(CreateSubItemRequest request)
    {
        return await Task.FromResult(new CreateSubItemResponse());
    }
}
