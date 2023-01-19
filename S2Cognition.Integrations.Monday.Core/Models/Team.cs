using Newtonsoft.Json;
using System.Collections.Generic;

namespace S2Cognition.Integrations.Monday.Core.Models;

/// <summary>
///     Teams are the most efficient way to manage groups of users in monday.com. Teams are comprised of one or multiple
///     users, and every user can be a part of multiple teams (or none).
/// </summary>
internal class Team
{
    /// <summary>
    ///     The team's unique identifier.
    /// </summary>
    [JsonProperty("id")]
    public ulong? Id { get; set; }

    /// <summary>
    ///     The team's name.
    /// </summary>
    [JsonProperty("name")]
    public string? Name { get; set; }

    /// <summary>
    ///     The team's picture url.
    /// </summary>
    [JsonProperty("picture_url")]
    public string? Photo { get; set; }

    /// <summary>
    ///     The users in the team.
    /// </summary>
    [JsonProperty("users")]
    public IEnumerable<User>? Users { get; set; }
}