using S2Cognition.Integrations.Monday.Core.Models.Requests;

namespace S2Cognition.Integrations.Monday.Core.Models.Options;

public interface IReplyOptions : IBaseOptions
{
    bool IncludeCreatorId { get; set; }
    bool IncludeCreator { get; set; }
    bool IncludeBody { get; set; }
    bool IncludeBodyText { get; set; }
    bool IncludeReplies { get; set; }
    bool IncludeCreatedAt { get; set; }
    bool IncludeUpdatedAt { get; set; }

    UserOptions? CreatorOptions { get; set; }
    ReplyOptions? RepliesOptions { get; set; }
}

public class ReplyOptions : BaseOptions, IReplyOptions
{
    public bool IncludeCreatorId { get; set; }
    public bool IncludeCreator { get; set; }
    public bool IncludeBody { get; set; }
    public bool IncludeBodyText { get; set; }
    public bool IncludeReplies { get; set; }
    public bool IncludeCreatedAt { get; set; }
    public bool IncludeUpdatedAt { get; set; }

    public UserOptions? CreatorOptions { get; set; }
    public ReplyOptions? RepliesOptions { get; set; }

    public ReplyOptions()
        : this(RequestMode.Default)
    {
    }

    public ReplyOptions(RequestMode mode)
       : base("reply", "replies")
    {
        switch (mode)
        {
            case RequestMode.Maximum:
                IncludeCreatorId = true;
                IncludeCreator = true;
                IncludeBody = true;
                IncludeBodyText = true;
                IncludeReplies = true;
                IncludeCreatedAt = true;
                IncludeUpdatedAt = true;
                CreatorOptions = new CreatorOptions(RequestMode.MaximumChild);
                RepliesOptions = new ReplyOptions(RequestMode.MaximumChild);
                break;

            case RequestMode.MaximumChild:
                IncludeCreatorId = true;
                IncludeCreator = false;
                IncludeBody = true;
                IncludeBodyText = true;
                IncludeReplies = false;
                IncludeCreatedAt = true;
                IncludeUpdatedAt = true;
                CreatorOptions = null;
                RepliesOptions = null;
                break;

            default:
                IncludeCreatorId = false;
                IncludeCreator = false;
                IncludeBody = false;
                IncludeBodyText = false;
                IncludeReplies = false;
                IncludeCreatedAt = false;
                IncludeUpdatedAt = false;
                CreatorOptions = null;
                RepliesOptions = null;
                break;
        }
    }

    internal override string Build(OptionBuilderMode mode, (string key, object val)[]? attrs = null)
    {
        var modelName = GetModelName(mode);
        var modelAttributes = GetModelAttributes(attrs);

        var creatorId = GetField(IncludeCreatorId, "creator_id");
        var body = GetField(IncludeBody, "body");
        var bodyText = GetField(IncludeBodyText, "text_body");
        var createdAt = GetField(IncludeCreatedAt, "created_at");
        var updatedAt = GetField(IncludeUpdatedAt, "updated_at");

        var creator = GetField(IncludeCreator, CreatorOptions?.Build(OptionBuilderMode.Single));
        var replies = GetField(IncludeReplies, RepliesOptions?.Build(OptionBuilderMode.Multiple));

        return $@"
{modelName}{modelAttributes} {{
    id {creatorId} {body} {bodyText} {createdAt} {updatedAt} {creator} {replies}
}}";
    }
}

