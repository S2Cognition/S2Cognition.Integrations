using FakeItEasy;
using GraphQL;
using S2Cognition.Integrations.Core.Models;
using S2Cognition.Integrations.Monday.Core.Models;
using S2Cognition.Integrations.Monday.Core.Models.Requests;
using S2Cognition.Integrations.Monday.Core.Models.Responses;
using Shouldly;
using System.Text.RegularExpressions;

using Group = S2Cognition.Integrations.Monday.Core.Models.Group;

namespace S2Cognition.Integrations.Monday.Core.Tests.InternalTests;

public class QueryOptionsTests
{
    private readonly MondayClient _mondayClient;
    private readonly IGraphQlHttpClient _graphQlClient;
    private GraphQLRequest _latestGraphQlRequest = A.Fake<GraphQLRequest>();

    public QueryOptionsTests()
    {
        _graphQlClient = A.Fake<IGraphQlHttpClient>();
        _mondayClient = A.Fake<MondayClient>(_ => _.WithArgumentsForConstructor(new object[] { _graphQlClient }));

        // setup fake graphql client responses:
        A.CallTo(() => _graphQlClient.SendQueryAsync<GetBoardItemsResponse>(A<GraphQLRequest>.Ignored))
            .ReturnsLazily(async (_) =>
            {
                if (_.Arguments[0] is not GraphQLRequest latestGraphQlRequest)
                    throw new InvalidOperationException();

                _latestGraphQlRequest = latestGraphQlRequest;
                return await Task.FromResult(new GraphQLResponse<GetBoardItemsResponse>()
                {
                    Data = new GetBoardItemsResponse(
                        new[] {
                            new Board { Items = new List<Item>() }
                        }
                    )
                });
            });

        A.CallTo(() => _graphQlClient.SendQueryAsync<GetItemsResponse>(A<GraphQLRequest>.Ignored))
            .ReturnsLazily(async (_) =>
            {
                if (_.Arguments[0] is not GraphQLRequest latestGraphQlRequest)
                    throw new InvalidOperationException();

                _latestGraphQlRequest = latestGraphQlRequest;
                return await Task.FromResult(new GraphQLResponse<GetItemsResponse>()
                {
                    Data = new GetItemsResponse(
                        new List<Item> {
                            new Item()
                        }
                    )
                });
            });

        A.CallTo(() => _graphQlClient.SendQueryAsync<GetUsersResponse>(A<GraphQLRequest>.Ignored))
            .ReturnsLazily(async (_) =>
            {
                if (_.Arguments[0] is not GraphQLRequest latestGraphQlRequest)
                    throw new InvalidOperationException();

                _latestGraphQlRequest = latestGraphQlRequest;
                return await Task.FromResult(new GraphQLResponse<GetUsersResponse>()
                {
                    Data = new GetUsersResponse
                    {
                        Users = new List<User> {
                          new User()
                        }
                    }
                });
            });

        A.CallTo(() => _graphQlClient.SendQueryAsync<GetBoardsResponse>(A<GraphQLRequest>.Ignored))
            .ReturnsLazily(async (_) =>
            {
                if (_.Arguments[0] is not GraphQLRequest latestGraphQlRequest)
                    throw new InvalidOperationException();

                _latestGraphQlRequest = latestGraphQlRequest;
                return await Task.FromResult(new GraphQLResponse<GetBoardsResponse>()
                {
                    Data = new GetBoardsResponse(
                        new List<Board> {
                            new Board()
                        }
                    )
                });
            });

        A.CallTo(() => _graphQlClient.SendMutationAsync<GetGroupsResponse>(A<GraphQLRequest>.Ignored))
            .ReturnsLazily(async (_) =>
            {
                if (_.Arguments[0] is not GraphQLRequest latestGraphQlRequest)
                    throw new InvalidOperationException();

                _latestGraphQlRequest = latestGraphQlRequest;
                return await Task.FromResult(new GraphQLResponse<GetGroupsResponse>()
                {
                    Data = new GetGroupsResponse(
                        new List<Board> {
                            new Board {
                                Groups = new List<Group>()
                            }
                        }
                    )
                });
            });

        A.CallTo(() => _graphQlClient.SendMutationAsync<GetBoardTagsResponse>(A<GraphQLRequest>.Ignored))
            .ReturnsLazily(async (_) =>
            {
                if (_.Arguments[0] is not GraphQLRequest latestGraphQlRequest)
                    throw new InvalidOperationException();

                _latestGraphQlRequest = latestGraphQlRequest;
                return await Task.FromResult(new GraphQLResponse<GetBoardTagsResponse>()
                {
                    Data = new GetBoardTagsResponse(
                        new List<Board> {
                            new Board{
                                Tags = new List<Tag>()
                            }
                        }
                    )
                });
            });

        A.CallTo(() => _graphQlClient.SendMutationAsync<GetTagsResponse>(A<GraphQLRequest>.Ignored))
            .ReturnsLazily(async (_) =>
            {
                if (_.Arguments[0] is not GraphQLRequest latestGraphQlRequest)
                    throw new InvalidOperationException();

                _latestGraphQlRequest = latestGraphQlRequest;
                return await Task.FromResult(new GraphQLResponse<GetTagsResponse>()
                {
                    Data = new GetTagsResponse(
                        new List<Tag> {
                            new Tag()
                        }
                    )
                });
            });

        A.CallTo(() => _graphQlClient.SendMutationAsync<GetTeamsResponse>(A<GraphQLRequest>.Ignored))
            .ReturnsLazily(async (_) =>
            {
                if (_.Arguments[0] is not GraphQLRequest latestGraphQlRequest)
                    throw new InvalidOperationException();

                _latestGraphQlRequest = latestGraphQlRequest;
                return await Task.FromResult(new GraphQLResponse<GetTeamsResponse>()
                {
                    Data = new GetTeamsResponse(
                        new List<Team> {
                            new Team()
                        }
                    )
                });
            });
    }

    private void DumbCheckQueryEquivalence(string query1, string query2)
    {
        // I need to find a graphql query parser or comparitor.  until then,
        // this is a fair approximately-equal check, but does nothing to
        // check for validity of the generated graphql query.  (spaces could
        // be missing between field names, etc.)
        var q1 = Regex.Replace(query1, @"\s+", string.Empty).ToCharArray().ToList();
        q1.Sort();
        var q2 = Regex.Replace(query2, @"\s+", string.Empty).ToCharArray().ToList();
        q2.Sort();

        q1.ShouldBeEquivalentTo(q2, $@">>> GraphQl querys do not match:
    * Expected: {Regex.Replace(query2, @"\s+", " ").Trim()}
    * Received: {Regex.Replace(query1, @"\s+", " ").Trim()}
");
    }

    [Fact]
    public async Task EnsureGetItemWithNewQueryOptionsDefaultsToSameAsOriginalQuery()
    {
        await _mondayClient.GetItem(1234);

        // Original query included updates information, but the resulting model does not represent that
        // data.  Therefore, I am removing it from the query...
        // The query was: "query request($id:Int) { items(ids: [$id]) { id name board { id name description board_kind state board_folder_id } group { id title color archived deleted } column_values { id text title type value additional_info } subscribers { id name email } updates(limit: 100000) { id body text_body replies { id body text_body creator_id creator { id name email } created_at updated_at } creator_id creator { id name email } created_at updated_at } creator_id created_at updated_at creator { id name email } } }"
        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query request($id:Int) { items(ids: [$id]) { id name board { id name description board_kind state board_folder_id } group { id title color archived deleted } column_values { id text title type value additional_info } subscribers { id name email } creator_id created_at updated_at creator { id name email } } }");
    }

    [Fact]
    public async Task EnsureGetItemsWithNewQueryOptionsDefaultsToSameAsOriginalQuery()
    {
        await _mondayClient.GetItems(1234);

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query request($id:Int) { boards(ids:[$id]) { items(limit: 100000) { id name board { id name description board_kind } group { id title archived deleted } creator_id created_at updated_at creator { id name email } } } } ");
    }

    [Fact]
    public async Task EnsureGetUsersWithNewQueryOptionsDefaultsToSameAsOriginalQuery()
    {
        await _mondayClient.GetUsers();

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query { users { id name email url photo_original title birthday country_code location time_zone_identifier phone mobile_phone is_guest is_pending enabled created_at }}");
    }

    [Fact]
    public async Task EnsureGetUsersByAccessTypeWithNewQueryOptionsDefaultsToSameAsOriginalQuery()
    {
        await _mondayClient.GetUsers(UserAccessTypes.All);

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query request($userKind:UserKind) { users(kind:$userKind) { id name email url photo_original title birthday country_code location time_zone_identifier phone mobile_phone is_guest is_pending enabled created_at }}");
    }

    [Fact]
    public async Task EnsureGetUserWithNewQueryOptionsDefaultsToSameAsOriginalQuery()
    {
        await _mondayClient.GetUser(1234);

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query request($id:Int) { users(ids:[$id]) { id name email url photo_original title birthday country_code location time_zone_identifier phone mobile_phone is_guest is_pending enabled created_at }}");
    }

    [Fact]
    public async Task EnsureGetBoardsWithNewQueryOptionsDefaultsToSameAsOriginalQuery()
    {
        await _mondayClient.GetBoards();

        // Original query included user and owner information, but the resulting model does not represent that
        // data.  Therefore, I am removing it from the query...
        // The query was: "query { boards(limit: 100000) { id name description board_kind state board_folder_id permissions owner { id name email }}}"
        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query { boards(limit: 100000) { id name description board_kind state board_folder_id permissions }}");
    }

    [Fact]
    public async Task EnsureGetBoardWithNewQueryOptionsDefaultsToSameAsOriginalQuery()
    {
        await _mondayClient.GetBoard(1234);

        // Original query included user and owner information, but the resulting model does not represent that
        // data.  Therefore, I am removing it from the query...
        // The query was: "query request($id:Int) { boards(ids:[$id]) { id name description board_kind state board_folder_id permissions owner { id name email url photo_original title birthday country_code location time_zone_identifier phone mobile_phone is_guest is_pending enabled created_at } columns { id, title, type, archived settings_str } } }"
        // I also removed the unnecessary commas in the columns list.
        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query request($id:Int) { boards(ids:[$id]) { id name description board_kind state board_folder_id permissions columns { id title type archived settings_str } } }");
    }

    [Fact]
    public async Task EnsureGetGroupsWithNewQueryOptionsDefaultsToSameAsOriginalQuery()
    {
        await _mondayClient.GetGroups(1234);

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query request($id:Int!) { boards(ids: [$id]) { groups { id title color archived deleted }}}");
    }

    [Fact]
    public async Task EnsureGetTagsWithNewQueryOptionsDefaultsToSameAsOriginalQuery()
    {
        await _mondayClient.GetTags(1234);

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query request($id:Int!) { boards(ids: [$id]) { tags { id name color } } } ");
    }

    [Fact]
    public async Task EnsureGetTagWithNewQueryOptionsDefaultsToSameAsOriginalQuery()
    {
        await _mondayClient.GetTag(1234);

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query request($id:Int!) { tags(ids: [$id]) { id name color } }");
    }

    [Fact]
    public async Task EnsureGetTeamsWithNewQueryOptionsDefaultsToSameAsOriginalQuery()
    {
        await _mondayClient.GetTeams();

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query request { teams { id name picture_url users { id name email } } }");
    }

    [Fact]
    public async Task EnsureGetTeamWithNewQueryOptionsDefaultsToSameAsOriginalQuery()
    {
        await _mondayClient.GetTeam(1234);

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query request($id:Int!) { teams(ids: [$id]) { id name picture_url users { id name email } } }");
    }

    [Fact]
    public async Task EnsureGetItemCanReturnMinimums()
    {
        await _mondayClient.GetItem(new GetItemRequest(1234, RequestMode.Minimum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query, "query request($id:Int) { items(ids: [$id]) { id } }");
    }

    [Fact]
    public async Task EnsureGetItemsCanReturnMinimums()
    {
        await _mondayClient.GetItems(new GetItemsRequest(1234, RequestMode.Minimum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query, "query request($id:Int) { boards(ids:[$id]) { items(limit:100000) { id } } }");
    }

    [Fact]
    public async Task EnsureGetUsersCanReturnMinimums()
    {
        await _mondayClient.GetUsers(new GetUsersRequest(RequestMode.Minimum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query, "query { users { id } }");
    }

    [Fact]
    public async Task EnsureGetUserCanReturnMinimums()
    {
        await _mondayClient.GetUser(new GetUserRequest(1234, RequestMode.Minimum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query, "query request($id:Int) { users(ids: [$id]) { id } }");
    }

    [Fact]
    public async Task EnsureGetBoardsCanReturnMinimums()
    {
        await _mondayClient.GetBoards(new GetBoardsRequest(RequestMode.Minimum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query, "query { boards(limit:100000) { id } }");
    }

    [Fact]
    public async Task EnsureGetBoardCanReturnMinimums()
    {
        await _mondayClient.GetBoard(new GetBoardRequest(1234, RequestMode.Minimum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query, "query request($id:Int) { boards(ids: [$id]) { id } }");
    }

    [Fact]
    public async Task EnsureGetGroupsCanReturnMinimums()
    {
        await _mondayClient.GetGroups(new GetGroupsRequest(1234, RequestMode.Minimum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query, "query request($id:Int!) { boards(ids: [$id]) { groups { id } } }");
    }

    [Fact]
    public async Task EnsureGetTagsCanReturnMinimums()
    {
        await _mondayClient.GetTags(new GetTagsRequest(1234, RequestMode.Minimum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query, "query request($id:Int!) { boards(ids: [$id]) { tags { id } } }");
    }

    [Fact]
    public async Task EnsureGetTagCanReturnMinimums()
    {
        await _mondayClient.GetTag(new GetTagRequest(1234, RequestMode.Minimum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query, "query request($id:Int!) { tags(ids:[$id]) { id } }");
    }

    [Fact]
    public async Task EnsureGetTeamsCanReturnMinimums()
    {
        await _mondayClient.GetTeams(new GetTeamsRequest(RequestMode.Minimum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query, "query request { teams { id } }");
    }

    [Fact]
    public async Task EnsureGetTeamCanReturnMinimums()
    {
        await _mondayClient.GetTeam(new GetTeamRequest(1234, RequestMode.Minimum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query, "query request($id:Int!) { teams(ids:[$id]) { id } }");
    }

    [Fact]
    public async Task EnsureGetItemCanReturnMaximums()
    {
        await _mondayClient.GetItem(new GetItemRequest(1234, RequestMode.Maximum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query request($id:Int) { items(ids:[$id]) { id name creator_id created_at updated_at creator { id name email url photo_original title birthday country_code location time_zone_identifier phone mobile_phone is_guest is_pending enabled created_at } board { id name description board_kind state board_folder_id permissions communication pos updated_at workspace_id } group { id title color archived deleted } column_values { id title value type text additional_info } subscribers { id name email url photo_original title birthday country_code location time_zone_identifier phone mobile_phone is_guest is_pending enabled created_at } } }");
    }

    [Fact]
    public async Task EnsureGetItemsCanReturnMaximums()
    {
        await _mondayClient.GetItems(new GetItemsRequest(1234, RequestMode.Maximum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query request($id:Int) { boards(ids:[$id]) { items(limit:100000) { id name creator_id created_at updated_at creator { id name email url photo_original title birthday country_code location time_zone_identifier phone mobile_phone is_guest is_pending enabled created_at } board { id name description board_kind state board_folder_id permissions communication pos updated_at workspace_id } group { id title color archived deleted } column_values { id title value type text additional_info } subscribers { id name email url photo_original title birthday country_code location time_zone_identifier phone mobile_phone is_guest is_pending enabled created_at } } } }");
    }

    [Fact]
    public async Task EnsureGetUsersCanReturnMaximums()
    {
        await _mondayClient.GetUsers(new GetUsersRequest(RequestMode.Maximum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query { users { id name email url photo_original title birthday country_code location time_zone_identifier phone mobile_phone is_guest is_pending enabled created_at } }");
    }

    [Fact]
    public async Task EnsureGetUserCanReturnMaximums()
    {
        await _mondayClient.GetUser(new GetUserRequest(1234, RequestMode.Maximum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query request($id:Int) { users(ids:[$id]) { id name email url photo_original title birthday country_code location time_zone_identifier phone mobile_phone is_guest is_pending enabled created_at } }");
    }

    [Fact]
    public async Task EnsureGetBoardsCanReturnMaximums()
    {
        await _mondayClient.GetBoards(new GetBoardsRequest(RequestMode.Maximum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query { boards(limit:100000) { id name description board_kind state board_folder_id columns { id title type archived settings_str } permissions activity_logs { id account_id created_at data entity event user_id } communication groups { id title color archived deleted } items { id name creator_id created_at updated_at } owner { id name email url photo_original title birthday country_code location time_zone_identifier phone mobile_phone is_guest is_pending enabled created_at } pos subscribers { id name email url photo_original title birthday country_code location time_zone_identifier phone mobile_phone is_guest is_pending enabled created_at } tags { id name color } top_group { id title color archived deleted } updated_at updates { id item_id creator_id body text_body created_at updated_at } views { id name settings_str type } workspace { id } workspace_id } }");
    }

    [Fact]
    public async Task EnsureGetBoardCanReturnMaximums()
    {
        await _mondayClient.GetBoard(new GetBoardRequest(1234, RequestMode.Maximum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query request($id:Int) { boards(ids:[$id]) { id name description board_kind state board_folder_id columns { id title type archived settings_str } permissions activity_logs { id account_id created_at data entity event user_id } communication groups { id title color archived deleted } items { id name creator_id created_at updated_at } owner { id name email url photo_original title birthday country_code location time_zone_identifier phone mobile_phone is_guest is_pending enabled created_at } pos subscribers { id name email url photo_original title birthday country_code location time_zone_identifier phone mobile_phone is_guest is_pending enabled created_at } tags { id name color } top_group { id title color archived deleted } updated_at updates { id item_id creator_id body text_body created_at updated_at } views { id name settings_str type } workspace { id } workspace_id } }");
    }

    [Fact]
    public async Task EnsureGetGroupsCanReturnMaximums()
    {
        await _mondayClient.GetGroups(new GetGroupsRequest(1234, RequestMode.Maximum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query request($id:Int!) { boards(ids: [$id]) { groups { id title color archived deleted } } }");
    }

    [Fact]
    public async Task EnsureGetTagsCanReturnMaximums()
    {
        await _mondayClient.GetTags(new GetTagsRequest(1234, RequestMode.Maximum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query request($id:Int!) { boards(ids: [$id]) { tags { id name color } } }");
    }

    [Fact]
    public async Task EnsureGetTagCanReturnMaximums()
    {
        await _mondayClient.GetTag(new GetTagRequest(1234, RequestMode.Maximum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query request($id:Int!) { tags(ids:[$id]) { id name color } }");
    }

    [Fact]
    public async Task EnsureGetTeamsCanReturnMaximums()
    {
        await _mondayClient.GetTeams(new GetTeamsRequest(RequestMode.Maximum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query request { teams { id name picture_url users { id name email url photo_original title birthday country_code location time_zone_identifier phone mobile_phone is_guest is_pending enabled created_at } } }");
    }

    [Fact]
    public async Task EnsureGetTeamCanReturnMaximums()
    {
        await _mondayClient.GetTeam(new GetTeamRequest(1234, RequestMode.Maximum));

        DumbCheckQueryEquivalence(_latestGraphQlRequest.Query,
            "query request($id:Int!) { teams(ids:[$id]) { id name picture_url users { id name email url photo_original title birthday country_code location time_zone_identifier phone mobile_phone is_guest is_pending enabled created_at } } }");
    }
}
