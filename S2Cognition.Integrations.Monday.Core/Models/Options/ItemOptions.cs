using S2Cognition.Integrations.Monday.Core.Models.Requests;

namespace S2Cognition.Integrations.Monday.Core.Models.Options;

public interface IItemOptions : IBaseOptions
{
    bool IncludeBoard { get; set; }
    bool IncludeGroup { get; set; }
    bool IncludeColumnValues { get; set; }
    bool IncludeName { get; set; }
    bool IncludeCreatorId { get; set; }
    bool IncludeCreatedAt { get; set; }
    bool IncludeUpdatedAt { get; set; }
    bool IncludeCreator { get; set; }
    bool IncludeSubscribers { get; set; }

    BoardOptions? BoardOptions { get; set; }
    GroupOptions? GroupOptions { get; set; }
    ColumnValueOptions? ColumnValueOptions { get; set; }
    CreatorOptions? CreatorOptions { get; set; }
    SubscriberOptions? SubscriberOptions { get; set; }
}

public class ItemOptions : BaseOptions, IItemOptions
{
    public bool IncludeBoard { get; set; }
    public bool IncludeGroup { get; set; }
    public bool IncludeColumnValues { get; set; }
    public bool IncludeName { get; set; }
    public bool IncludeCreatorId { get; set; }
    public bool IncludeCreatedAt { get; set; }
    public bool IncludeUpdatedAt { get; set; }
    public bool IncludeCreator { get; set; }
    public bool IncludeSubscribers { get; set; }

    public BoardOptions? BoardOptions { get; set; }
    public GroupOptions? GroupOptions { get; set; }
    public ColumnValueOptions? ColumnValueOptions { get; set; }
    public CreatorOptions? CreatorOptions { get; set; }
    public SubscriberOptions? SubscriberOptions { get; set; }

    public ItemOptions()
        : this(RequestMode.Default)
    {
    }

    public ItemOptions(RequestMode mode)
        : base("item", "items")
    {
        switch (mode)
        {
            case RequestMode.Minimum:
                IncludeBoard = false;
                IncludeGroup = false;
                IncludeColumnValues = false;
                IncludeName = false;
                IncludeCreatorId = false;
                IncludeCreatedAt = false;
                IncludeUpdatedAt = false;
                IncludeCreator = false;
                IncludeSubscribers = false;
                BoardOptions = null;
                GroupOptions = null;
                ColumnValueOptions = null;
                CreatorOptions = null;
                SubscriberOptions = null;
                break;

            case RequestMode.Maximum:
                IncludeBoard = true;
                IncludeGroup = true;
                IncludeColumnValues = true;
                IncludeName = true;
                IncludeCreatorId = true;
                IncludeCreatedAt = true;
                IncludeUpdatedAt = true;
                IncludeCreator = true;
                IncludeSubscribers = true;
                BoardOptions = new BoardOptions(RequestMode.MaximumChild);
                GroupOptions = new GroupOptions(RequestMode.MaximumChild);
                ColumnValueOptions = new ColumnValueOptions(RequestMode.MaximumChild);
                CreatorOptions = new CreatorOptions(RequestMode.MaximumChild);
                SubscriberOptions = new SubscriberOptions(RequestMode.MaximumChild);
                break;

            case RequestMode.MaximumChild:
                IncludeBoard = false;
                IncludeGroup = false;
                IncludeColumnValues = false;
                IncludeName = true;
                IncludeCreatorId = true;
                IncludeCreatedAt = true;
                IncludeUpdatedAt = true;
                IncludeCreator = false;
                IncludeSubscribers = false;
                BoardOptions = null;
                GroupOptions = null;
                ColumnValueOptions = null;
                CreatorOptions = null;
                SubscriberOptions = null;
                break;

            case RequestMode.Default:
            default:
                IncludeBoard = true;
                IncludeGroup = true;
                IncludeColumnValues = true;
                IncludeName = true;
                IncludeCreatorId = true;
                IncludeCreatedAt = true;
                IncludeUpdatedAt = true;
                IncludeCreator = true;
                IncludeSubscribers = true;

                ColumnValueOptions = new ColumnValueOptions(RequestMode.Minimum)
                {
                    IncludeText = true,
                    IncludeTitle = true,
                    IncludeType = true,
                    IncludeAdditionalInfo = true,
                    IncludeValue = true
                };
                CreatorOptions = new CreatorOptions(RequestMode.Minimum)
                {
                    IncludeEmail = true,
                    IncludeName = true
                };
                BoardOptions = new BoardOptions(RequestMode.Minimum)
                {
                    IncludeBoardStateType = true,
                    IncludeBoardFolderId = true,
                    IncludeName = true,
                    IncludeDescription = true,
                    IncludeBoardAccessType = true
                };
                GroupOptions = new GroupOptions(RequestMode.Minimum)
                {
                    IncludeColor = true,
                    IncludeTitle = true,
                    IncludeIsArchived = true,
                    IncludeIsDeleted = true
                };
                SubscriberOptions = new SubscriberOptions(RequestMode.Minimum)
                {
                    IncludeName = true,
                    IncludeEmail = true
                };
                break;
        }
    }

    internal override string Build(OptionBuilderMode mode, (string key, object val)[]? attrs = null)
    {
        var modelName = GetModelName(mode);
        var modelAttributes = GetModelAttributes(attrs);

        var name = GetField(IncludeName, "name");
        var createdAt = GetField(IncludeCreatedAt, "created_at");
        var creatorId = GetField(IncludeCreatorId, "creator_id");
        var updatedAt = GetField(IncludeUpdatedAt, "updated_at");

        var creator = GetField(IncludeCreator, CreatorOptions?.Build(OptionBuilderMode.Single));
        var board = GetField(IncludeBoard, BoardOptions?.Build(OptionBuilderMode.Single));
        var group = GetField(IncludeGroup, GroupOptions?.Build(OptionBuilderMode.Single));
        var columnValues = GetField(IncludeColumnValues, ColumnValueOptions?.Build(OptionBuilderMode.Multiple));
        var subscribers = GetField(IncludeSubscribers, SubscriberOptions?.Build(OptionBuilderMode.Multiple));

        var child = $"id {name} {creatorId} {createdAt} {updatedAt} {creator} {board} {group} {columnValues} {subscribers}";
        if (mode == OptionBuilderMode.Child)
            return child;

        return $@"
{modelName}{modelAttributes} {{
    {child}
}}";
    }
}