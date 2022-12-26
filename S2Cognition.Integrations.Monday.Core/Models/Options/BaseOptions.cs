using System;
using System.Linq;

namespace S2Cognition.Integrations.Monday.Core.Models.Options;

public interface IBaseOptions
{
}

public abstract class BaseOptions : IBaseOptions
{
    protected string NameSingular { get; set; }
    protected string NamePlural { get; set; }

    protected BaseOptions(string singular)
        : this(singular, $"{singular}s")
    {
    }

    protected BaseOptions(string singular, string plural)
    {
        NameSingular = singular;
        NamePlural = plural;
    }

    internal abstract string Build(OptionBuilderMode mode, (string key, object val)[]? attrs = null);

    internal string GetModelName(OptionBuilderMode mode)
    {
        var modelName = NameSingular;
        if (mode == OptionBuilderMode.Multiple)
            modelName = NamePlural;

        return modelName;
    }

    internal string GetModelAttributes((string key, object val)[]? attrs)
    {
        var attributes = string.Empty;
        if (attrs != null)
        {
            attributes = attrs.Aggregate(string.Empty, (_c, _n) => $",{_n.key}:{_n.val}");
            if (attributes.Length > 0)
                attributes = $"({attributes.Substring(1)})";
        }
        return attributes;
    }

    internal string GetField(bool include, string? field)
    {
        if (include)
            return field ?? string.Empty;
        return string.Empty;
    }
}

public class OptionsBuilder
{
    internal string Build(IBaseOptions opt, params (string key, object val)[] attrs)
    {
        return Build(opt, OptionBuilderMode.Single, attrs);
    }

    internal string Build(IBaseOptions? opt, OptionBuilderMode mode, params (string key, object val)[] attrs)
    {
        if (opt is BaseOptions o)
            return o.Build(mode, attrs);

        return string.Empty;
    }
}
