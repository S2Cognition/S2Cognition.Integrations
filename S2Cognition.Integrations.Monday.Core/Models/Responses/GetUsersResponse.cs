using System;
namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

public class GetUsersResponse
{
    public IEnumerable<User> Users { get; set; } = Array.Empty<User>();
}