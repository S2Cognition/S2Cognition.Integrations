using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using S2Cognition.Integrations.Core.Models;
using S2Cognition.Integrations.Monday.Core.Models;
using S2Cognition.Integrations.Monday.Core.Models.Extensions;
using S2Cognition.Integrations.Monday.Core.Models.Mutations;
using S2Cognition.Integrations.Monday.Core.Models.Options;
using S2Cognition.Integrations.Monday.Core.Models.Requests;
using S2Cognition.Integrations.Monday.Core.Models.Responses;
using System.Net.Http.Headers;

namespace S2Cognition.Integrations.Monday.Core.Models;

/// <summary>
///     Creates client for accessing Monday's endpoints.
/// </summary>
internal class MondayClient
{
    private readonly IGraphQlHttpClient _graphQlHttpClient;
    private readonly OptionsBuilder _optionsBuilder;

    /// <summary>
    ///     Creates client for accessing Monday's endpoints.
    /// </summary>
    /// <param name="apiKey">The version 2 key.</param>
    internal MondayClient(IGraphQlHttpClient graphQlClient)
    {
        _graphQlHttpClient = graphQlClient;
        _optionsBuilder = new OptionsBuilder();
    }

    /// <summary>
    ///     Helper method for throwing all errors reported from the response.
    /// </summary>
    /// <param name="graphQlErrors"></param>
    private void ThrowResponseErrors(GraphQLError[]? graphQlErrors)
    {
        if (graphQlErrors == null)
            return;

        var error = graphQlErrors.FirstOrDefault();
        if (error != default)
            throw new InvalidOperationException(error.Message);
    }

    /// <summary>
    ///     Get all users.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<User>> GetUsers()
    {
        return (await GetUsers(new GetUsersRequest())).Data;
    }

    public async Task<IGetUsersResult> GetUsers(IGetUsersRequest req)
    {
        GraphQLRequest request;
        if (req.UserAccessType.HasValue)
        {
            request = new GraphQLRequest
            {
                Query = $@"
query request($userKind:UserKind) {{ 
    {_optionsBuilder.Build(req.UserOptions, OptionBuilderMode.Multiple, ("kind", "$userKind"))}
}}",
                Variables = new
                {
                    userKind = req.UserAccessType.Value.GetVariableUserAccessType()
                }
            };
        }
        else
        {
            request = new GraphQLRequest
            {
                Query = $@"
query {{ 
    {_optionsBuilder.Build(req.UserOptions, OptionBuilderMode.Multiple)}
}}"
            };
        }

        var result = await _graphQlHttpClient.SendQueryAsync<GetUsersResponse>(request);

        ThrowResponseErrors(result.Errors);

        return new GetUsersResult(result.Data.Users);
    }

    /// <summary>
    ///     Get all users filtered by user access type [user kind].
    /// </summary>
    /// <param name="userAccessType">The user access type.</param>
    /// <returns></returns>
    public async Task<IEnumerable<User>> GetUsers(UserAccessTypes userAccessType)
    {
        return (await GetUsers(new GetUsersRequest
        {
            UserAccessType = userAccessType
        })).Data;
    }

    /// <summary>
    ///     Return a specific user.
    /// </summary>
    /// <param name="userId">The user’s unique identifier.</param>
    /// <returns></returns>
    public async Task<User?> GetUser(ulong userId)
    {
        return (await GetUser(new GetUserRequest(userId))).Data;
    }

    public async Task<IGetUserResult> GetUser(IGetUserRequest req)
    {
        var request = new GraphQLRequest
        {
            Query = $@"
query request($id:Int) {{ 
    {_optionsBuilder.Build(req.UserOptions, OptionBuilderMode.Multiple, ("ids", "[$id]"))}
}}",
            Variables = new
            {
                id = req.UserId
            }
        };

        var result = await _graphQlHttpClient.SendQueryAsync<GetUsersResponse>(request);

        ThrowResponseErrors(result.Errors);

        return new GetUserResult(result.Data.Users.FirstOrDefault());
    }

    /// <summary>
    ///     Get all (up to 100,000) boards [excluding columns] with a simplified user.
    /// </summary>
    /// <remarks>
    ///     Groups, items, subscribers, tags and updates can be loaded with their functions to further reduce complexity
    /// </remarks>
    /// <returns></returns>
    public async Task<IEnumerable<Board>> GetBoards()
    {
        return (await GetBoards(new GetBoardsRequest())).Data;
    }

    public async Task<IGetBoardsResult> GetBoards(IGetBoardsRequest req)
    {
        var request = new GraphQLRequest
        {
            Query = $@"
query {{ 
    {_optionsBuilder.Build(req.BoardOptions, OptionBuilderMode.Multiple, ("limit", req.Limit))}
}}"
        };

        var result = await _graphQlHttpClient.SendQueryAsync<GetBoardsResponse>(request);

        ThrowResponseErrors(result.Errors);

        return new GetBoardsResult(result.Data.Boards);
    }

    /// <summary>
    ///     Return a specific board with it's columns.
    /// </summary>
    /// <param name="boardId">The board’s unique identifier.</param>
    /// <returns></returns>
    public async Task<Board?> GetBoard(ulong boardId)
    {
        return (await GetBoard(new GetBoardRequest(boardId))).Data;
    }

    public async Task<IGetBoardResult> GetBoard(IGetBoardRequest req)
    {
        var request = new GraphQLRequest
        {
            Query = $@"
query request($id:Int) {{ 
    {_optionsBuilder.Build(req.BoardOptions, OptionBuilderMode.Multiple, ("ids", "[$id]"))}
}}",
            Variables = new
            {
                id = req.BoardId
            }
        };

        var result = await _graphQlHttpClient.SendQueryAsync<GetBoardsResponse>(request);

        ThrowResponseErrors(result.Errors);

        return new GetBoardResult(result.Data.Boards.FirstOrDefault());
    }

    /// <summary>
    ///     Gets all groups on a board.
    /// </summary>
    /// <param name="boardId">The board’s unique identifier.</param>
    /// <returns></returns>
    public async Task<IEnumerable<Group>> GetGroups(ulong boardId)
    {
        return (await GetGroups(new GetGroupsRequest(boardId))).Data;
    }

    public async Task<IGetGroupsResult> GetGroups(IGetGroupsRequest req)
    {
        var request = new GraphQLRequest
        {
            Query = $@"
query request($id:Int!) {{ 
    boards(ids: [$id]) {{ 
        {_optionsBuilder.Build(req.GroupOptions, OptionBuilderMode.Multiple)}
    }}
}}",
            Variables = new
            {
                id = req.BoardId
            }
        };

        var result = await _graphQlHttpClient.SendMutationAsync<GetGroupsResponse>(request);

        ThrowResponseErrors(result.Errors);

        var groups = result.Data.Boards.First().Groups;
        if (groups != null)
            return new GetGroupsResult(groups);

        throw new InvalidOperationException();
    }

    /// <summary>
    ///     Get all (up to 100,000) items [excluding columns and subscribers] with simplified boards, groups, and users.
    /// </summary>
    /// <param name="boardId">The board’s unique identifier.</param>
    /// <returns></returns>
    public async Task<IEnumerable<Item>?> GetItems(ulong boardId)
    {
        return (await GetItems(new GetItemsRequest(boardId))).Data;
    }

    public async Task<IGetItemsResult> GetItems(IGetItemsRequest req)
    {
        if (String.IsNullOrWhiteSpace(req.FilterColumnName) || String.IsNullOrWhiteSpace(req.FilterColumnValue))
        {
            var query = $@"
query request($id:Int) {{ 
    boards(ids:[$id]) {{ 
        {_optionsBuilder.Build(req.ItemOptions, OptionBuilderMode.Multiple, ("limit", req.Limit))}
    }} 
}}";

            var request = new GraphQLRequest
            {
                Query = query,
                Variables = new
                {
                    id = req.BoardId
                }
            };

            var result = await _graphQlHttpClient.SendQueryAsync<GetBoardItemsResponse>(request);

            ThrowResponseErrors(result.Errors);

            var items = result.Data.Boards.First().Items;
            if (items != null)
                return new GetItemsResult(items);

            throw new InvalidOperationException();
        }
        else
        {
            var query = $@"
query request {{ 
    items_by_column_values ( 
        board_id: {req.BoardId},
        column_id: ""{req.FilterColumnName}"",
        column_value: ""{req.FilterColumnValue}"",
        {((req.FilterState.HasValue && (req.FilterState.Value == StateFilter.Active)) ? "state: active" : String.Empty)}
    ) {{
        {_optionsBuilder.Build(req.ItemOptions, OptionBuilderMode.Child, ("limit", req.Limit))}
    }}
}}";

            var request = new GraphQLRequest
            {
                Query = query
            };

            var result = await _graphQlHttpClient.SendQueryAsync<GetItemsResponse>(request);

            ThrowResponseErrors(result.Errors);

            return new GetItemsResult(result.Data.Items);
        }
    }

    /// <summary>
    ///     Return a specific item with it's board, group, columns, subscribers, updates, replies and user.
    /// </summary>
    /// <param name="itemId">The item’s unique identifier.</param>
    /// <returns></returns>
    public async Task<Item?> GetItem(ulong itemId)
    {
        return (await GetItem(new GetItemRequest(itemId))).Data;
    }

    public async Task<IGetItemResult> GetItem(IGetItemRequest req)
    {
        var request = new GraphQLRequest
        {
            Query = $@"
query request($id:Int) {{ 
    {_optionsBuilder.Build(req.ItemOptions, OptionBuilderMode.Multiple, ("ids", "[$id]"))}
}}",
            Variables = new
            {
                id = req.ItemId
            }
        };

        var result = await _graphQlHttpClient.SendQueryAsync<GetItemsResponse>(request);

        ThrowResponseErrors(result.Errors);

        return new GetItemResult(result.Data.Items.FirstOrDefault());
    }

    /// <summary>
    ///     Gets all tags on a board.
    /// </summary>
    /// <param name="boardId">The board’s unique identifier.</param>
    /// <returns></returns>
    public async Task<IEnumerable<Tag>> GetTags(ulong boardId)
    {
        return (await GetTags(new GetTagsRequest(boardId))).Data;
    }

    public async Task<IGetTagsResult> GetTags(IGetTagsRequest req)
    {
        var request = new GraphQLRequest
        {
            Query = $@"
query request($id:Int!) {{ 
    boards(ids: [$id]) {{
        {_optionsBuilder.Build(req.TagOptions, OptionBuilderMode.Multiple)}
    }} 
}}",
            Variables = new
            {
                id = req.BoardId
            }
        };

        var result = await _graphQlHttpClient.SendMutationAsync<GetBoardTagsResponse>(request);

        ThrowResponseErrors(result.Errors);

        var tags = result.Data.Boards.First().Tags;
        if (tags != null)
            return new GetTagsResult(tags);

        throw new InvalidOperationException();
    }

    /// <summary>
    ///     Returns a specific tag.
    /// </summary>
    /// <param name="tagId">The tag’s unique identifier.</param>
    /// <returns></returns>
    public async Task<Tag?> GetTag(ulong tagId)
    {
        return (await GetTag(new GetTagRequest(tagId))).Data;
    }

    public async Task<IGetTagResult> GetTag(IGetTagRequest req)
    {
        var request = new GraphQLRequest
        {
            Query = $@"
query request($id:Int!) {{ 
    {_optionsBuilder.Build(req.TagOptions, OptionBuilderMode.Multiple, ("ids", "[$id]"))}
}}",
            Variables = new
            {
                id = req.TagId
            }
        };

        var result = await _graphQlHttpClient.SendMutationAsync<GetTagsResponse>(request);

        ThrowResponseErrors(result.Errors);

        return new GetTagResult(result.Data.Tags.FirstOrDefault());
    }

    /// <summary>
    ///     Gets all teams with simplified users.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Team>> GetTeams()
    {
        return (await GetTeams(new GetTeamsRequest())).Data;
    }

    public async Task<IGetTeamsResult> GetTeams(IGetTeamsRequest req)
    {
        var request = new GraphQLRequest
        {
            Query = $@"
query request {{
    {_optionsBuilder.Build(req.TeamOptions, OptionBuilderMode.Multiple)}
}}"
        };

        var result = await _graphQlHttpClient.SendMutationAsync<GetTeamsResponse>(request);

        ThrowResponseErrors(result.Errors);

        return new GetTeamsResult(result.Data.Teams);
    }

    /// <summary>
    ///     Returns a specific team with simplified users.
    /// </summary>
    /// <returns></returns>
    public async Task<Team?> GetTeam(ulong teamId)
    {
        return (await GetTeam(new GetTeamRequest(teamId))).Data;
    }

    public async Task<IGetTeamResult> GetTeam(IGetTeamRequest req)
    {
        var request = new GraphQLRequest
        {
            Query = $@"
query request($id:Int!) {{ 
    {_optionsBuilder.Build(req.TeamOptions, OptionBuilderMode.Multiple, ("ids", "[$id]"))}
}}",
            Variables = new
            {
                id = req.TeamId
            }
        };

        var result = await _graphQlHttpClient.SendMutationAsync<GetTeamsResponse>(request);

        ThrowResponseErrors(result.Errors);

        return new GetTeamResult(result.Data.Teams.FirstOrDefault());
    }

    /// <summary>
    ///     Returns the api rate limit (how much of your 1 minute cap is left)
    /// </summary>
    /// <returns></returns>
    public async Task<int> GetRateLimit()
    {
        var request = new GraphQLRequest
        {
            Query = @"query request { complexity { after } }"
        };

        var result = await _graphQlHttpClient.SendMutationAsync<GetComplexityResponse>(request);

        ThrowResponseErrors(result.Errors);

        return result.Data.Complexity.After;
    }

    /// <summary>
    ///     Get the complexity data of your queries.
    /// </summary>
    /// <param name="query">The request query.</param>
    /// <returns></returns>
    public async Task<Complexity> GetComplexity(string query)
    {
        var requestQuery = @"query request { complexity { before after query }";
        var complexityQuery = $"{requestQuery} \n {query}}}";

        var request = new GraphQLRequest
        {
            Query = complexityQuery
        };

        var result = await _graphQlHttpClient.SendMutationAsync<GetComplexityResponse>(request);

        ThrowResponseErrors(result.Errors);

        return result.Data.Complexity;
    }

    /// <summary>
    ///     Creates a board.
    /// </summary>
    /// <param name="createBoard">The mutation model.</param>
    /// <returns></returns>
    public async Task<ulong> CreateBoard(CreateBoard createBoard)
    {
        var request = new GraphQLRequest
        {
            Query = @"mutation request($name:String! $boardKind:BoardKind!) { create_board (board_name: $name, board_kind: $boardKind) { id }}",
            Variables = new
            {
                name = createBoard.Name,
                boardKind = createBoard.BoardAccessType.GetVariableBoardAccessType()
            }
        };

        var result = await _graphQlHttpClient.SendMutationAsync<CreateBoardResponse>(request);

        ThrowResponseErrors(result.Errors);

        if (result.Data.Board.Id.HasValue)
            return result.Data.Board.Id.Value;

        throw new InvalidOperationException();
    }

    /// <summary>
    ///     Archives a board.
    /// </summary>
    /// <param name="boardId">The board’s unique identifier.</param>
    /// <returns></returns>
    public async Task<bool> ArchiveBoard(ulong boardId)
    {
        var request = new GraphQLRequest
        {
            Query = @"mutation request($id:Int!) { archive_board (board_id: $id) { id }}",
            Variables = new
            {
                id = boardId
            }
        };

        var result = await _graphQlHttpClient.SendMutationAsync<dynamic>(request);

        ThrowResponseErrors(result.Errors);

        return result.Errors == null;
    }

    /// <summary>
    ///     Create a new column in board.
    /// </summary>
    /// <param name="createColumn">The mutation model.</param>
    /// <returns></returns>
    public async Task<string?> CreateColumn(CreateColumn createColumn)
    {
        var request = new GraphQLRequest
        {
            Query = @"mutation request($boardId:Int! $name:String! $columnType:ColumnType $defaults:JSON) { create_column (board_id: $boardId, title: $name, column_type: $columnType, defaults: $defaults) { id }}",
            Variables = new
            {
                boardId = createColumn.BoardId,
                name = createColumn.Name,
                columnType = createColumn.ColumnType.GetVariableColumnType(),
                defaults = createColumn.Defaults
            }
        };

        var result = await _graphQlHttpClient.SendMutationAsync<CreateColumnResponse>(request);

        ThrowResponseErrors(result.Errors);

        return result.Data.Column.Id;
    }

    /// <summary>
    ///     Update the value of a column for a specific item.
    /// </summary>
    /// <param name="updateColumn">The mutation model.</param>
    /// <returns></returns>
    public async Task<bool> UpdateColumn(UpdateColumn updateColumn)
    {
        var request = new GraphQLRequest
        {
            Query = @"mutation request($boardId:Int! $itemId:Int $columnId: String! $value:JSON!) { change_column_value (board_id: $boardId, item_id: $itemId, column_id: $columnId, value: $value) { id }}",
            Variables = new
            {
                boardId = updateColumn.BoardId,
                itemId = updateColumn.ItemId,
                columnId = updateColumn.ColumnId,
                value = updateColumn.Value
            }
        };

        var result = await _graphQlHttpClient.SendMutationAsync<dynamic>(request);

        ThrowResponseErrors(result.Errors);

        return result.Errors == null;
    }

    /// <summary>
    ///     Creates a new group in a specific board.
    /// </summary>
    /// <param name="createGroup">The mutation model.</param>
    /// <returns></returns>
    public async Task<string?> CreateGroup(CreateGroup createGroup)
    {
        var request = new GraphQLRequest
        {
            Query = @"mutation request($boardId:Int! $name:String!) { create_group (board_id: $boardId, group_name: $name) { id } }",
            Variables = new
            {
                boardId = createGroup.BoardId,
                name = createGroup.Name
            }
        };

        var result = await _graphQlHttpClient.SendMutationAsync<CreateGroupResponse>(request);

        ThrowResponseErrors(result.Errors);

        return result.Data.Group.Id;
    }

    /// <summary>
    ///     Archives a group in a specific board.
    /// </summary>
    /// <param name="boardId">The board’s unique identifier.</param>
    /// <param name="groupId">The board’s group identifier.</param>
    /// <returns></returns>
    public async Task<bool> ArchiveGroup(ulong boardId, string groupId)
    {
        var request = new GraphQLRequest
        {
            Query = @"mutation request($boardId:Int! $groupId:String!) { archive_group (board_id: $boardId, group_id: $groupId) { id }}",
            Variables = new
            {
                boardId,
                groupId
            }
        };

        var result = await _graphQlHttpClient.SendMutationAsync<dynamic>(request);

        ThrowResponseErrors(result.Errors);

        return result.Errors == null;
    }

    /// <summary>
    ///     Deletes a group in a specific board.
    /// </summary>
    /// <param name="boardId">The board’s unique identifier.</param>
    /// <param name="groupId">THe group's unique identifier</param>
    /// <returns></returns>
    public async Task<bool> DeleteGroup(ulong boardId, string groupId)
    {
        var request = new GraphQLRequest
        {
            Query = @"mutation request($boardId:Int! $groupId:String!) { delete_group (board_id: $boardId, group_id: $groupId) { id }}",
            Variables = new
            {
                boardId,
                groupId
            }
        };

        var result = await _graphQlHttpClient.SendMutationAsync<dynamic>(request);

        ThrowResponseErrors(result.Errors);

        return result.Errors == null;
    }

    /// <summary>
    ///     Create a new item.
    /// </summary>
    /// <param name="createItem">The mutation model.</param>
    /// <returns></returns>
    public async Task<ulong?> CreateItem(CreateItem createItem)
    {
        var response = await CreateItem(new CreateItemRequest
        {
            Name = createItem.Name,
            BoardId = createItem.BoardId,
            GroupId = createItem.GroupId,
            ColumnValues = String.IsNullOrWhiteSpace(createItem.ColumnValues) ? null : new MondayColumns(createItem.ColumnValues ?? String.Empty)
        });

        if (response.Data == null)
            throw new InvalidOperationException("Cannot Create Item");

        return response.Data.Id;
    }

    public async Task<ICreateItemResult> CreateItem(ICreateItemRequest createItem)
    {
        var request = new GraphQLRequest
        {
            Query = $@"
mutation request {{ 
    create_item (
        board_id: {createItem.BoardId}, 
        group_id: {createItem.GroupId}, 
        item_name: ""{createItem.Name}"", 
        column_values: {createItem.ColumnValues}
    ) {{
        id 
    }} 
}}"
        };

        var result = await _graphQlHttpClient.SendMutationAsync<CreateItemResponse>(request);

        ThrowResponseErrors(result.Errors);

        return new CreateItemResult
        {
            Data = result.Data.Item
        };
    }

    public async Task<CreateSubItemResult> CreateSubItem(CreateSubItemRequest createSubItem)
    {
        var request = new GraphQLRequest
        {
            Query = $@"
mutation {{ 
      create_subitem (
        parent_item_id: {createSubItem.ParentId}, 
        item_name: ""{createSubItem.ItemName}"",
        ) {{ id }}
}}"
        };

        var result = await _graphQlHttpClient.SendMutationAsync<CreateSubItemResponse>(request);

        ThrowResponseErrors(result.Errors);

        return new CreateSubItemResult();
    }

    /// <summary>
    ///     Clear an item's updates.
    /// </summary>
    /// <param name="itemId">The item’s unique identifier.</param>
    /// <returns></returns>
    public async Task<bool> ClearItemUpdates(ulong itemId)
    {
        var request = new GraphQLRequest
        {
            Query = @"mutation request($id:Int!) { clear_item_updates (item_id: $id) {id}}",
            Variables = new
            {
                id = itemId
            }
        };

        var result = await _graphQlHttpClient.SendMutationAsync<dynamic>(request);

        ThrowResponseErrors(result.Errors);

        return result.Errors == null;
    }

    /// <summary>
    ///     Move an item to a different group.
    /// </summary>
    /// <param name="itemId">The item’s unique identifier.</param>
    /// <param name="groupId">The group’s unique identifier</param>
    /// <returns></returns>
    public async Task<bool> UpdateItemGroup(ulong itemId, string groupId)
    {
        var request = new GraphQLRequest
        {
            Query = @"mutation request($itemId:Int! $groupId:String!) { move_item_to_group (item_id: $itemId, group_id: $groupId) {id}}",
            Variables = new
            {
                itemId,
                groupId
            }
        };

        var result = await _graphQlHttpClient.SendMutationAsync<dynamic>(request);

        ThrowResponseErrors(result.Errors);

        return result.Errors == null;
    }

    /// <summary>
    ///     Archive an item.
    /// </summary>
    /// <param name="itemId">The item’s unique identifier.</param>
    /// <returns></returns>
    public async Task<bool> ArchiveItem(ulong itemId)
    {
        var request = new GraphQLRequest
        {
            Query = @"mutation request($id:Int!) { archive_item (item_id: $id) {id}}",
            Variables = new
            {
                id = itemId
            }
        };

        var result = await _graphQlHttpClient.SendMutationAsync<dynamic>(request);

        ThrowResponseErrors(result.Errors);

        return result.Errors == null;
    }

    /// <summary>
    ///     Delete an item.
    /// </summary>
    /// <param name="itemId">The item’s unique identifier.</param>
    /// <returns></returns>
    public async Task<bool> DeleteItem(ulong itemId)
    {
        var request = new GraphQLRequest
        {
            Query = @"mutation request($id:Int!) { delete_item (item_id: $id) {id}}",
            Variables = new
            {
                id = itemId
            }
        };

        var result = await _graphQlHttpClient.SendMutationAsync<dynamic>(request);

        ThrowResponseErrors(result.Errors);

        return result.Errors == null;
    }

    /// <summary>
    ///     Create a new update.
    /// </summary>
    /// <param name="createUpdate">The mutation model.</param>
    /// <returns></returns>
    public async Task<ulong?> CreateUpdate(CreateUpdate createUpdate)
    {
        var request = new GraphQLRequest
        {
            Query = @"mutation request($itemId:Int $body:String!) { create_update (item_id: $itemId, body: $body) {id}}",
            Variables = new
            {
                itemId = createUpdate.ItemId,
                body = createUpdate.Body
            }
        };

        var result = await _graphQlHttpClient.SendMutationAsync<CreateUpdateResponse>(request);

        ThrowResponseErrors(result.Errors);

        return result.Data.Update.Id;
    }

    /// <summary>
    ///     Create a new tag or get it if it already exists.
    /// </summary>
    /// <param name="createTag">The mutation model.</param>
    /// <returns></returns>
    public async Task<ulong> CreateTag(CreateTag createTag)
    {
        var request = new GraphQLRequest
        {
            Query = @"mutation request($boardId:Int $name:String) { create_or_get_tag  (board_id: $boardId, tag_name: $name) {id}}",
            Variables = new
            {
                boardId = createTag.BoardId,
                name = createTag.Name
            }
        };

        var result = await _graphQlHttpClient.SendMutationAsync<CreateTagResponse>(request);

        ThrowResponseErrors(result.Errors);

        return result.Data.Tag.Id;
    }

    /// <summary>
    ///     Leverage a custom query or mutation using the available responses/models
    /// </summary>
    /// <typeparam name="T">Generic object return</typeparam>
    /// <param name="queryOrMutation">A custom query or mutation</param>
    /// <returns></returns>
    public async Task<T> CustomQueryOrMutation<T>(string queryOrMutation)
    {
        return await CustomQueryOrMutation<T>(queryOrMutation, null);
    }

    /// <summary>
    ///     Leverage a custom query or mutation using the available responses/models
    /// </summary>
    /// <typeparam name="T">Generic object return</typeparam>
    /// <param name="queryOrMutation">A custom query or mutation</param>
    /// <param name="variables">Anonymous object with variables and values</param>
    /// <returns></returns>
    public async Task<T> CustomQueryOrMutation<T>(string queryOrMutation, object? variables)
    {
        var request = new GraphQLRequest
        {
            Query = queryOrMutation
        };

        if (variables != null)
        {
            request.Variables = variables;
        }

        var result = await _graphQlHttpClient.SendMutationAsync<T>(request);

        ThrowResponseErrors(result.Errors);

        return result.Data;
    }
}
