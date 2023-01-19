using S2Cognition.Integrations.Monday.Core.Models.Requests;

namespace S2Cognition.Integrations.Monday.Core.Models.Options;

internal interface ITagOptions : IBaseOptions
{
    bool IncludeName { get; set; }
    bool IncludeColor { get; set; }
}

internal class TagOptions : BaseOptions, ITagOptions
{
    public bool IncludeName { get; set; }
    public bool IncludeColor { get; set; }

    internal TagOptions()
        : this(RequestMode.Default)
    {
    }

    internal TagOptions(RequestMode mode)
       : base("tag")
    {
        switch (mode)
        {
            case RequestMode.Minimum:
                IncludeName = false;
                IncludeColor = false;
                break;

            case RequestMode.Maximum:
            case RequestMode.MaximumChild:
                IncludeName = true;
                IncludeColor = true;
                break;

            default:
                IncludeName = true;
                IncludeColor = true;
                break;
        }
    }

    internal override string Build(OptionBuilderMode mode, (string key, object val)[]? attrs = null)
    {
        var modelName = GetModelName(mode);
        var modelAttributes = GetModelAttributes(attrs);

        var name = GetField(IncludeName, "name");
        var color = GetField(IncludeColor, "color");

        return $@"
{modelName}{modelAttributes} {{
    id {name} {color}
}}";
    }
}
