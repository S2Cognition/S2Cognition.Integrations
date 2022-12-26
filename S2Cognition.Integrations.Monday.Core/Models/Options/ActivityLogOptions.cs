using S2Cognition.Integrations.Monday.Core.Models.Requests;

namespace S2Cognition.Integrations.Monday.Core.Models.Options;

public interface IActivityLogOptions : IBaseOptions
{
    bool IncludeAccountId { get; set; }
    bool IncludeCreatedAt { get; set; }
    bool IncludeData { get; set; }
    bool IncludeEntity { get; set; }
    bool IncludeEvent { get; set; }
    bool IncludeUserId { get; set; }
}

public class ActivityLogOptions : BaseOptions, IActivityLogOptions
{
    public bool IncludeAccountId { get; set; }
    public bool IncludeCreatedAt { get; set; }
    public bool IncludeData { get; set; }
    public bool IncludeEntity { get; set; }
    public bool IncludeEvent { get; set; }
    public bool IncludeUserId { get; set; }

    public ActivityLogOptions()
        : this(RequestMode.Default)
    {
    }

    public ActivityLogOptions(RequestMode mode)
        : base("activity_log")
    {
        switch (mode)
        {
            case RequestMode.Minimum:
                IncludeAccountId = false;
                IncludeCreatedAt = false;
                IncludeData = false;
                IncludeEntity = false;
                IncludeEvent = false;
                IncludeUserId = false;
                break;

            case RequestMode.Maximum:
            case RequestMode.MaximumChild:
                IncludeAccountId = true;
                IncludeCreatedAt = true;
                IncludeData = true;
                IncludeEntity = true;
                IncludeEvent = true;
                IncludeUserId = true;
                break;

            case RequestMode.Default:
            default:
                IncludeAccountId = false;
                IncludeCreatedAt = false;
                IncludeData = true;
                IncludeEntity = false;
                IncludeEvent = true;
                IncludeUserId = true;
                break;
        }
    }

    internal override string Build(OptionBuilderMode mode, (string key, object val)[]? attrs = null)
    {
        var modelName = GetModelName(mode);
        var modelAttributes = GetModelAttributes(attrs);

        var accountId = GetField(IncludeAccountId, "account_id");
        var createdAt = GetField(IncludeCreatedAt, "created_at");
        var data = GetField(IncludeData, "data");
        var entity = GetField(IncludeEntity, "entity");
        var evt = GetField(IncludeEvent, "event");
        var userId = GetField(IncludeUserId, "user_id");

        return $@"
{modelName}{modelAttributes} {{
    id {accountId} {createdAt} {data} {entity} {evt} {userId} 
}}";
    }
}