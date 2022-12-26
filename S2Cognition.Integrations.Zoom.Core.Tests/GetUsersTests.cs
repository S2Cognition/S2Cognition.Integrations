namespace S2Cognition.Integrations.Zoom.Core.Tests;

public class GetUsersTests : UnitTestBase
{
    private ZoomConfiguration _configuration = default!;
    private IZoomIntegration _sut = default!;

    protected override async Task IocSetup(IServiceCollection sc)
    {
        sc.AddZoomIntegration();
        await Task.CompletedTask;
    }

    protected override async Task TestSetup()
    {
        _httpClient.ExpectsPost("https://zoom.us/oauth/token?grant_type=account_credentials&account_id=fake_account_id", "{ \"access_token\": \"fake access token\", \"token_type\": \"bearer\", \"expires_in\": 3599, \"scope\": \"fake scopes\" }");
        _httpClient.ExpectsGet("https://api.zoom.us/v2/users", "{ \"page_count\": 1, \"page_number\": 1, \"page_size\": 30, \"total_records\": 2, \"next_page_token\": \"\", \"users\": [ { \"id\": \"fake id 1\", \"first_name\": \"Test\", \"last_name\": \"User\", \"email\": \"dummy1@example.com\", \"type\": 2, \"pmi\": 123123123, \"timezone\": \"America/Chicago\", \"verified\": 1, \"dept\": \"\", \"created_at\": \"2020-03-31T15:02:16Z\", \"last_login_time\": \"2022-12-08T20:58:38Z\", \"last_client_version\": \"5.12.9.5692(iphone)\", \"language\": \"en-US\", \"status\": \"active\", \"role_id\": \"1\", \"employee_unique_id\": \"fake id 1\", \"user_created_at\": \"2020-03-31T15:01:23Z\" }, { \"id\": \"fake id 2\", \"first_name\": \"Another\", \"last_name\": \"User\", \"email\": \"dummy2@example.com\", \"type\": 2, \"pmi\": 987987987, \"timezone\": \"America/Chicago\", \"verified\": 1, \"dept\": \"\", \"created_at\": \"2022-05-18T17:42:48Z\", \"last_login_time\": \"2022-12-06T14:46:52Z\", \"last_client_version\": \"5.12.8.5518(iphone)\", \"language\": \"en-US\", \"phone_number\": \"+1 1231231231\", \"status\": \"active\", \"role_id\": \"2\", \"employee_unique_id\": \"fake id 2\", \"user_created_at\": \"2022-05-12T11:38:23Z\" } ] }");

        _configuration = new ZoomConfiguration(_ioc)
        {
            AccountId = "fake_account_id",
            ClientId = "fake_client_id",
            ClientSecret = "fake_client_secret"
        };

        _sut = _ioc.GetRequiredService<IZoomIntegration>();
        await _sut.Initialize(_configuration);
    }

    [Fact]
    public async Task EnsureGetUsersIsCallable()
    {
        var users = await _sut.GetUsers();

        users.ShouldNotBeNull();
    }
}