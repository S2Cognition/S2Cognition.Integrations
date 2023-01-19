using S2Cognition.Integrations.Monday.Core.Models.Requests;

namespace S2Cognition.Integrations.Monday.Core.Models.Options;

internal interface IGroupOptions : IBaseOptions
{
    bool IncludeTitle { get; set; }
    bool IncludeColor { get; set; }
    bool IncludeIsArchived { get; set; }
    bool IncludeIsDeleted { get; set; }
}

internal class GroupOptions : BaseOptions, IGroupOptions
{
    public bool IncludeTitle { get; set; }
    public bool IncludeColor { get; set; }
    public bool IncludeIsArchived { get; set; }
    public bool IncludeIsDeleted { get; set; }

    internal GroupOptions()
        : this(RequestMode.Default)
    {
    }

    internal GroupOptions(RequestMode mode)
       : base("group")
    {
        switch (mode)
        {
            case RequestMode.Minimum:
                IncludeTitle = false;
                IncludeColor = false;
                IncludeIsArchived = false;
                IncludeIsDeleted = false;
                break;

            case RequestMode.Maximum:
            case RequestMode.MaximumChild:
                IncludeTitle = true;
                IncludeColor = true;
                IncludeIsArchived = true;
                IncludeIsDeleted = true;
                break;

            default:
                IncludeTitle = true;
                IncludeColor = true;
                IncludeIsArchived = true;
                IncludeIsDeleted = true;
                break;
        }
    }

    internal override string Build(OptionBuilderMode mode, (string key, object val)[]? attrs = null)
    {
        var modelName = GetModelName(mode);
        var modelAttributes = GetModelAttributes(attrs);

        var title = GetField(IncludeTitle, "title");
        var color = GetField(IncludeColor, "color");
        var isArchived = GetField(IncludeIsArchived, "archived");
        var isDeleted = GetField(IncludeIsDeleted, "deleted");

        return $@"
{modelName}{modelAttributes} {{
    id {title} {color} {isArchived} {isDeleted}
}}";
    }
}

internal class TopGroupOptions : GroupOptions
{
    internal TopGroupOptions()
        : this(RequestMode.Default)
    {
    }

    internal TopGroupOptions(RequestMode mode)
        : base(mode)
    {
        NameSingular = "top_group";
        NamePlural = "top_group";
    }
}
