using System;
using System.Linq;

namespace S2Cognition.Integrations.Monday.Core.Models.Extensions;

internal static class StringExtensions
{
    internal static string FirstCharacterToUpper(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        return $"{char.ToUpper(value.Trim().First())}{value.Trim().Substring(1)}";
    }
}