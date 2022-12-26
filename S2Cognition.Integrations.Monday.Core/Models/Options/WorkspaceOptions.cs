using S2Cognition.Integrations.Monday.Core.Models.Requests;

namespace S2Cognition.Integrations.Monday.Core.Models.Options;

public interface IWorkspaceOptions : IBaseOptions
{
}

public class WorkspaceOptions : BaseOptions, IWorkspaceOptions
{
    public WorkspaceOptions()
        : this(RequestMode.Default)
    {
    }

    public WorkspaceOptions(RequestMode mode)
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