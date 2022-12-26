using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace S2Cognition.Integrations.Monday.Core.Models;

public class Update
{
    /// <summary>
    ///     The update's unique identifier.
    /// </summary>
    [JsonProperty("id")]
    public ulong? Id { get; set; }

    /// <summary>
    ///     The update's item unique identifier.
    /// </summary>
    [JsonProperty("item_id")]
    public ulong? ItemId { get; set; }

    /// <summary>
    ///     The unique identifier of the update creator.
    /// </summary>
    [JsonProperty("creator_id")]
    public ulong? UserId { get; set; }

    /// <summary>
    ///     The update's creator.
    /// </summary>
    [JsonProperty("creator")]
    public User? User { get; set; }

    /// <summary>
    ///     The update's html formatted body.
    /// </summary>
    [JsonProperty("body")]
    public string? Body { get; set; }

    /// <summary>
    ///     The update's text body.
    /// </summary>
    [JsonProperty("text_body")]
    public string? BodyText { get; set; }

    /// <summary>
    ///     The update's replies.
    /// </summary>
    [JsonProperty("replies")]
    public IEnumerable<Reply>? Replies { get; set; }

    /// <summary>
    ///     The update's creation date.
    /// </summary>
    [JsonProperty("created_at")]
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    ///     The update's last edit date.
    /// </summary>
    [JsonProperty("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}