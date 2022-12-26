using S2Cognition.Integrations.Monday.Core.Models.Requests;

namespace S2Cognition.Integrations.Monday.Core.Models.Options;

public interface ITeamOptions : IBaseOptions
{
    bool IncludeName { get; set; }
    bool IncludePhoto { get; set; }
    bool IncludeUsers { get; set; }

    UserOptions? UserOptions { get; set; }
}

public class TeamOptions : BaseOptions, ITeamOptions
{
    public bool IncludeName { get; set; }
    public bool IncludePhoto { get; set; }
    public bool IncludeUsers { get; set; }

    public UserOptions? UserOptions { get; set; }

    public TeamOptions()
        : this(RequestMode.Default)
    {
    }

    public TeamOptions(RequestMode mode)
       : base("team")
    {
        switch (mode)
        {
            case RequestMode.Minimum:
                IncludeName = false;
                IncludePhoto = false;
                IncludeUsers = false;
                UserOptions = null;
                break;

            case RequestMode.Maximum:
                IncludeName = true;
                IncludePhoto = true;
                IncludeUsers = true;
                UserOptions = new UserOptions(RequestMode.MaximumChild);
                break;

            case RequestMode.MaximumChild:
                IncludeName = true;
                IncludePhoto = true;
                IncludeUsers = false;
                UserOptions = null;
                break;

            default:
                IncludeName = true;
                IncludePhoto = true;
                IncludeUsers = true;
                UserOptions = new UserOptions(RequestMode.Minimum)
                {
                    IncludeEmail = true,
                    IncludeName = true
                };
                break;
        }
    }

    internal override string Build(OptionBuilderMode mode, (string key, object val)[]? attrs = null)
    {
        var modelName = GetModelName(mode);
        var modelAttributes = GetModelAttributes(attrs);

        var name = GetField(IncludeName, "name");
        var photo = GetField(IncludePhoto, "picture_url");

        var users = GetField(IncludeUsers, UserOptions?.Build(OptionBuilderMode.Multiple));

        return $@"
{modelName}{modelAttributes} {{
    id {name} {photo} {users}
}}";
    }
}
