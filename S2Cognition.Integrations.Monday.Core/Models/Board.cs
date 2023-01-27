using Newtonsoft.Json;
using S2Cognition.Integrations.Monday.Core.Models.Extensions;
using System;
using System.Collections.Generic;

namespace S2Cognition.Integrations.Monday.Core.Models;

/// <summary>
///     Monday.com boards are where our users input all of their data and as such it is one of the core components of the
///     platform and one of the main objects with which you will need to be familiar.
///     The board’s structure is composed of rows(called items), groups of rows(called groups), and columns.The data of the
///     board is stored in the items of the board and in the updates sections of each item. Each board has one or more
///     owners and subscribers.
///     
/// https://monday.com/developers/v2#queries-section-boards
/// </summary>
internal class Board
{
    /// <summary>
    ///     The unique identifier of the board.
    /// </summary>
    public ulong? Id { get; set; }

    /// <summary>
    ///     The board's name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     The board's description.
    /// </summary>
    public string? Description { get; set; }

    [JsonProperty("board_kind")] internal string? Access { get; set; }

    /// <summary>
    ///     The board's kind. (public / private / share)
    /// </summary>
    public BoardAccessTypes? BoardAccessType => Enum.TryParse(Access?.FirstCharacterToUpper(), out BoardAccessTypes type) ? type : BoardAccessTypes.Default;

    [JsonProperty("state")] internal string? State { get; set; }

    /// <summary>
    ///     The state of the board (all / active / archived / deleted), the default is active.
    /// </summary>
    public BoardStateTypes? BoardStateType => Enum.TryParse(State?.FirstCharacterToUpper(), out BoardStateTypes type) ? type : BoardStateTypes.Default;

    /// <summary>
    ///     The board's folder unique identifier.
    /// </summary>
    [JsonProperty("board_folder_id")]
    public int? BoardFolderId { get; set; }

    /// <summary>
    ///     The board's visible columns.
    /// </summary>
    public List<Column>? Columns { get; set; }

    /// <summary>
    ///     The board's permissions.
    /// </summary>
    public string? Permissions { get; set; }

    [JsonProperty("activity_logs")]
    public List<ActivityLog>? ActivityLogs { get; set; }
    public string? Communication { get; set; }
    public IEnumerable<Group>? Groups { get; set; }
    public IEnumerable<Item>? Items { get; set; }
    public User? Owner { get; set; }
    [JsonProperty("pos")]
    public string? Position { get; set; }
    public List<User>? Subscribers { get; set; }
    public IEnumerable<Tag>? Tags { get; set; }
    [JsonProperty("top_group")]
    public Group? TopGroup { get; set; }
    [JsonProperty("updated_at")]
    public DateTime? UpdatedAt { get; set; }
    public List<Update>? Updates { get; set; }
    public List<BoardView>? Views { get; set; }
    public Workspace? Workspace { get; set; }
    [JsonProperty("workspace_id")]
    public int? WorkspaceId { get; set; }
}

/// <summary>
/// https://monday.com/developers/v2#queries-section-activity-logs
/// </summary>
internal class ActivityLog
{
    public string? Id { get; set; }

    [JsonProperty("account_id")]
    public string? AccountId { get; set; }
    [JsonProperty("created_at")]
    public string? CreatedAt { get; set; }
    public string? Data { get; set; }
    public string? Entity { get; set; }
    public string? Event { get; set; }
    [JsonProperty("user_id")]
    public string? UserId { get; set; }
}

/// <summary>
/// https://monday.com/developers/v2#queries-section-board-views
/// </summary>
internal class BoardView
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    [JsonProperty("settings_str")]
    public string? Settings { get; set; }
    public string? Type { get; set; }
}

internal class Workspace
{
}
