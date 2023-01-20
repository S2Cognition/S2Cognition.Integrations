using S2Cognition.Integrations.Monday.Core.Models.Requests;

namespace S2Cognition.Integrations.Monday.Core.Models.Options;

internal interface IBoardOptions : IBaseOptions
{
    bool IncludeName { get; set; }
    bool IncludeDescription { get; set; }
    bool IncludeBoardAccessType { get; set; }
    bool IncludeBoardStateType { get; set; }
    bool IncludeBoardFolderId { get; set; }
    bool IncludeColumns { get; set; }
    bool IncludePermissions { get; set; }
    bool IncludeActivityLogs { get; set; }
    bool IncludeCommunication { get; set; }
    bool IncludeGroups { get; set; }
    bool IncludeItems { get; set; }
    bool IncludeOwner { get; set; }
    bool IncludePosition { get; set; }
    bool IncludeSubscribers { get; set; }
    bool IncludeTags { get; set; }
    bool IncludeTopGroup { get; set; }
    bool IncludeUpdatedAt { get; set; }
    bool IncludeUpdates { get; set; }
    bool IncludeViews { get; set; }
    bool IncludeWorkspace { get; set; }
    bool IncludeWorkspaceId { get; set; }

    ColumnOptions? ColumnOptions { get; set; }
    ActivityLogOptions? ActivityLogOptions { get; set; }
    GroupOptions? GroupOptions { get; set; }
    ItemOptions? ItemOptions { get; set; }
    OwnerOptions? OwnerOptions { get; set; }
    SubscriberOptions? SubscriberOptions { get; set; }
    TagOptions? TagOptions { get; set; }
    TopGroupOptions? TopGroupOptions { get; set; }
    UpdateOptions? UpdateOptions { get; set; }
    BoardViewOptions? BoardViewOptions { get; set; }
    WorkspaceOptions? WorkspaceOptions { get; set; }
}

internal class BoardOptions : BaseOptions, IBoardOptions
{
    public bool IncludeName { get; set; }
    public bool IncludeDescription { get; set; }
    public bool IncludeBoardAccessType { get; set; }
    public bool IncludeBoardStateType { get; set; }
    public bool IncludeBoardFolderId { get; set; }
    public bool IncludeColumns { get; set; }
    public bool IncludePermissions { get; set; }
    public bool IncludeActivityLogs { get; set; }
    public bool IncludeCommunication { get; set; }
    public bool IncludeGroups { get; set; }
    public bool IncludeItems { get; set; }
    public bool IncludeOwner { get; set; }
    public bool IncludePosition { get; set; }
    public bool IncludeSubscribers { get; set; }
    public bool IncludeTags { get; set; }
    public bool IncludeTopGroup { get; set; }
    public bool IncludeUpdatedAt { get; set; }
    public bool IncludeUpdates { get; set; }
    public bool IncludeViews { get; set; }
    public bool IncludeWorkspace { get; set; }
    public bool IncludeWorkspaceId { get; set; }

    public ColumnOptions? ColumnOptions { get; set; }
    public ActivityLogOptions? ActivityLogOptions { get; set; }
    public GroupOptions? GroupOptions { get; set; }
    public ItemOptions? ItemOptions { get; set; }
    public OwnerOptions? OwnerOptions { get; set; }
    public SubscriberOptions? SubscriberOptions { get; set; }
    public TagOptions? TagOptions { get; set; }
    public TopGroupOptions? TopGroupOptions { get; set; }
    public UpdateOptions? UpdateOptions { get; set; }
    public BoardViewOptions? BoardViewOptions { get; set; }
    public WorkspaceOptions? WorkspaceOptions { get; set; }

    internal BoardOptions()
        : this(RequestMode.Default)
    {
    }

    internal BoardOptions(RequestMode mode)
        : base("board")
    {
        switch (mode)
        {
            case RequestMode.Minimum:
                IncludeName = false;
                IncludeDescription = false;
                IncludeBoardAccessType = false;
                IncludeBoardStateType = false;
                IncludeBoardFolderId = false;
                IncludeColumns = false;
                IncludePermissions = false;
                IncludeActivityLogs = false;
                IncludeCommunication = false;
                IncludeGroups = false;
                IncludeItems = false;
                IncludeOwner = false;
                IncludePosition = false;
                IncludeSubscribers = false;
                IncludeTags = false;
                IncludeTopGroup = false;
                IncludeUpdatedAt = false;
                IncludeUpdates = false;
                IncludeViews = false;
                IncludeWorkspace = false;
                IncludeWorkspaceId = false;
                ColumnOptions = null;
                ActivityLogOptions = null;
                GroupOptions = null;
                ItemOptions = null;
                OwnerOptions = null;
                SubscriberOptions = null;
                TagOptions = null;
                TopGroupOptions = null;
                UpdateOptions = null;
                BoardViewOptions = null;
                WorkspaceOptions = null;
                break;

            case RequestMode.Maximum:
                IncludeName = true;
                IncludeDescription = true;
                IncludeBoardAccessType = true;
                IncludeBoardStateType = true;
                IncludeBoardFolderId = true;
                IncludeColumns = true;
                IncludePermissions = true;
                IncludeActivityLogs = true;
                IncludeCommunication = true;
                IncludeGroups = true;
                IncludeItems = true;
                IncludeOwner = true;
                IncludePosition = true;
                IncludeSubscribers = true;
                IncludeTags = true;
                IncludeTopGroup = true;
                IncludeUpdatedAt = true;
                IncludeUpdates = true;
                IncludeViews = true;
                IncludeWorkspace = true;
                IncludeWorkspaceId = true;
                ColumnOptions = new ColumnOptions(RequestMode.MaximumChild);
                ActivityLogOptions = new ActivityLogOptions(RequestMode.MaximumChild);
                GroupOptions = new GroupOptions(RequestMode.MaximumChild);
                ItemOptions = new ItemOptions(RequestMode.MaximumChild);
                OwnerOptions = new OwnerOptions(RequestMode.MaximumChild);
                SubscriberOptions = new SubscriberOptions(RequestMode.MaximumChild);
                TagOptions = new TagOptions(RequestMode.MaximumChild);
                TopGroupOptions = new TopGroupOptions(RequestMode.MaximumChild);
                UpdateOptions = new UpdateOptions(RequestMode.MaximumChild);
                BoardViewOptions = new BoardViewOptions(RequestMode.MaximumChild);
                WorkspaceOptions = new WorkspaceOptions(RequestMode.MaximumChild);
                break;

            case RequestMode.MaximumChild:
                IncludeName = true;
                IncludeDescription = true;
                IncludeBoardAccessType = true;
                IncludeBoardStateType = true;
                IncludeBoardFolderId = true;
                IncludeColumns = false;
                IncludePermissions = true;
                IncludeActivityLogs = false;
                IncludeCommunication = true;
                IncludeGroups = false;
                IncludeItems = false;
                IncludeOwner = false;
                IncludePosition = true;
                IncludeSubscribers = false;
                IncludeTags = false;
                IncludeTopGroup = false;
                IncludeUpdatedAt = true;
                IncludeUpdates = false;
                IncludeViews = false;
                IncludeWorkspace = false;
                IncludeWorkspaceId = true;
                ColumnOptions = null;
                ActivityLogOptions = null;
                GroupOptions = null;
                ItemOptions = null;
                OwnerOptions = null;
                SubscriberOptions = null;
                TagOptions = null;
                TopGroupOptions = null;
                UpdateOptions = null;
                BoardViewOptions = null;
                WorkspaceOptions = null;
                break;

            default:
                IncludeName = true;
                IncludeDescription = true;
                IncludeBoardAccessType = true;
                IncludeBoardStateType = true;
                IncludeBoardFolderId = true;
                IncludeColumns = true;
                IncludePermissions = true;
                IncludeActivityLogs = false;
                IncludeCommunication = false;
                IncludeGroups = false;
                IncludeItems = false;
                IncludeOwner = false;
                IncludePosition = false;
                IncludeSubscribers = false;
                IncludeTags = false;
                IncludeTopGroup = false;
                IncludeUpdatedAt = false;
                IncludeUpdates = false;
                IncludeViews = false;
                IncludeWorkspace = false;
                IncludeWorkspaceId = false;
                ColumnOptions = new ColumnOptions(RequestMode.Minimum)
                {
                    IncludeTitle = true,
                    IncludeType = true,
                    IncludeIsArchived = true,
                    IncludeSettings = true
                };
                ActivityLogOptions = null;
                GroupOptions = null;
                ItemOptions = null;
                OwnerOptions = null;
                SubscriberOptions = null;
                TagOptions = null;
                TopGroupOptions = null;
                UpdateOptions = null;
                BoardViewOptions = null;
                WorkspaceOptions = null;
                break;
        }
    }

    internal override string Build(OptionBuilderMode mode, (string key, object val)[]? attrs = null)
    {
        var modelName = GetModelName(mode);
        var modelAttributes = GetModelAttributes(attrs);

        var name = GetField(IncludeName, "name");
        var description = GetField(IncludeDescription, "description");
        var boardAccessType = GetField(IncludeBoardAccessType, "board_kind");
        var boardStateType = GetField(IncludeBoardStateType, "state");
        var boardFolderId = GetField(IncludeBoardFolderId, "board_folder_id");
        var columns = GetField(IncludeColumns, ColumnOptions?.Build(OptionBuilderMode.Multiple));
        var permissions = GetField(IncludePermissions, "permissions");
        var activityLogs = GetField(IncludeActivityLogs, ActivityLogOptions?.Build(OptionBuilderMode.Multiple));
        var communication = GetField(IncludeCommunication, "communication");
        var groups = GetField(IncludeGroups, GroupOptions?.Build(OptionBuilderMode.Multiple));
        var items = GetField(IncludeItems, ItemOptions?.Build(OptionBuilderMode.Multiple));
        var owner = GetField(IncludeOwner, OwnerOptions?.Build(OptionBuilderMode.Single));
        var pos = GetField(IncludePosition, "pos");
        var subscribers = GetField(IncludeSubscribers, SubscriberOptions?.Build(OptionBuilderMode.Multiple));
        var tags = GetField(IncludeTags, TagOptions?.Build(OptionBuilderMode.Multiple));
        var topGroup = GetField(IncludeTopGroup, TopGroupOptions?.Build(OptionBuilderMode.Single));
        var updatedAt = GetField(IncludeUpdatedAt, "updated_at");
        var updates = GetField(IncludeUpdates, UpdateOptions?.Build(OptionBuilderMode.Multiple));
        var views = GetField(IncludeViews, BoardViewOptions?.Build(OptionBuilderMode.Multiple));
        var workspace = GetField(IncludeWorkspace, WorkspaceOptions?.Build(OptionBuilderMode.Single));
        var workspaceId = GetField(IncludeWorkspaceId, "workspace_id");

        return $@"
{modelName}{modelAttributes} {{
    id {name} {description} {boardAccessType} {boardStateType} {boardFolderId} {columns} {permissions}
    {activityLogs} {communication} {groups} {items} {owner} {pos} {subscribers} {tags} {topGroup} 
    {updatedAt} {updates} {views} {workspace} {workspaceId}
}}";
    }
}