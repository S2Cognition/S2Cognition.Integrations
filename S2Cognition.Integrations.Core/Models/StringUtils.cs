using System.Text;

namespace S2Cognition.Integrations.Core.Models;

public interface IStringUtils
{
    string ToBase64(string src);
}

internal class StringUtils : IStringUtils
{
    public string ToBase64(string src)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(src));
    }
}
