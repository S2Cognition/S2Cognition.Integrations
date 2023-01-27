using System;
namespace S2Cognition.Integrations.Monday.Core.Models.Responses;

internal class GetUsersResponse
{
    public IEnumerable<User> Users { get; set; } = Array.Empty<User>();
}