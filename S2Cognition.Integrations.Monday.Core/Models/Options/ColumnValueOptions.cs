using S2Cognition.Integrations.Monday.Core.Models.Requests;

namespace S2Cognition.Integrations.Monday.Core.Models.Options;

internal interface IColumnValuesOptions : IBaseOptions
{
    bool IncludeTitle { get; set; }
    bool IncludeValue { get; set; }
    bool IncludeType { get; set; }
    bool IncludeText { get; set; }
    bool IncludeAdditionalInfo { get; set; }
}

internal class ColumnValueOptions : BaseOptions, IColumnValuesOptions
{
    public bool IncludeTitle { get; set; }
    public bool IncludeValue { get; set; }
    public bool IncludeType { get; set; }
    public bool IncludeText { get; set; }
    public bool IncludeAdditionalInfo { get; set; }

    internal ColumnValueOptions()
        : this(RequestMode.Default)
    {
    }

    internal ColumnValueOptions(RequestMode mode)
        : base("column_value")
    {
        switch (mode)
        {
            case RequestMode.Minimum:
                IncludeTitle = false;
                IncludeValue = false;
                IncludeType = false;
                IncludeText = false;
                IncludeAdditionalInfo = false;
                break;

            case RequestMode.Maximum:
            case RequestMode.MaximumChild:
                IncludeTitle = true;
                IncludeValue = true;
                IncludeType = true;
                IncludeText = true;
                IncludeAdditionalInfo = true;
                break;

            case RequestMode.Default:
            default:
                IncludeTitle = true;
                IncludeValue = true;
                IncludeType = true;
                IncludeText = true;
                IncludeAdditionalInfo = true;
                break;
        }
    }

    internal override string Build(OptionBuilderMode mode, (string key, object val)[]? attrs = null)
    {
        var modelName = GetModelName(mode);
        var modelAttributes = GetModelAttributes(attrs);

        var title = GetField(IncludeTitle, "title");
        var value = GetField(IncludeValue, "value");
        var type = GetField(IncludeType, "type");
        var valueText = GetField(IncludeText, "text");
        var information = GetField(IncludeAdditionalInfo, "additional_info");

        return $@"
{modelName}{modelAttributes} {{
    id {title} {value} {type} {valueText} {information}
}}";
    }
}
