using S2Cognition.Integrations.Monday.Core.Models.Requests;

namespace S2Cognition.Integrations.Monday.Core.Models.Options;

internal interface IWorkspaceOptions : IBaseOptions
{
}

internal class WorkspaceOptions : BaseOptions, IWorkspaceOptions
{
    internal WorkspaceOptions()
        : this(RequestMode.Default)
    {
    }

    internal WorkspaceOptions(RequestMode mode)
        : base("workspace")
    {
        switch (mode)
        {
            case RequestMode.Minimum:
            case RequestMode.Maximum:
            case RequestMode.MaximumChild:
            case RequestMode.Default:
            default:
                break;
        }
    }

    internal override string Build(OptionBuilderMode mode, (string key, object val)[]? attrs = null)
    {
        var modelName = GetModelName(mode);
        var modelAttributes = GetModelAttributes(attrs);

        return $@"
{modelName}{modelAttributes} {{
    id 
}}";
    }
}