using S2Cognition.Integrations.Monday.Core.Models.Requests;

namespace S2Cognition.Integrations.Monday.Core.Models.Options;

internal interface IColumnOptions : IBaseOptions
{
    bool IncludeTitle { get; set; }
    bool IncludeType { get; set; }
    bool IncludeIsArchived { get; set; }
    bool IncludeSettings { get; set; }
}

internal class ColumnOptions : BaseOptions, IColumnOptions
{
    public bool IncludeTitle { get; set; }
    public bool IncludeType { get; set; }
    public bool IncludeIsArchived { get; set; }
    public bool IncludeSettings { get; set; }

    internal ColumnOptions()
        : this(RequestMode.Default)
    {
    }

    internal ColumnOptions(RequestMode mode)
        : base("column")
    {
        switch (mode)
        {
            case RequestMode.Minimum:
                IncludeTitle = false;
                IncludeType = false;
                IncludeIsArchived = false;
                IncludeSettings = false;
                break;

            case RequestMode.Maximum:
            case RequestMode.MaximumChild:
                IncludeTitle = true;
                IncludeType = true;
                IncludeIsArchived = true;
                IncludeSettings = true;
                break;

            case RequestMode.Default:
            default:
                IncludeTitle = true;
                IncludeType = true;
                IncludeIsArchived = true;
                IncludeSettings = true;
                break;
        }
    }

    internal override string Build(OptionBuilderMode mode, (string key, object val)[]? attrs = null)
    {
        var modelName = GetModelName(mode);
        var modelAttributes = GetModelAttributes(attrs);

        var title = GetField(IncludeTitle, "title");
        var type = GetField(IncludeType, "type");
        var archived = GetField(IncludeIsArchived, "archived");
        var settings = GetField(IncludeSettings, "settings_str");

        return $@"
{modelName}{modelAttributes} {{
    id {title} {type} {archived} {settings}
}}";
    }
}
