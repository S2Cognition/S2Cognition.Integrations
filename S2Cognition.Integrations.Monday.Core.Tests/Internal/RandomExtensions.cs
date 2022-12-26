using System;

namespace S2Cognition.Integrations.Monday.Core.Tests.Internal;

public static class RandomExtensions
{
    public static ulong NextUInt64(this Random random)
    {
        return (ulong)random.NextInt64();
    }

    public static string NextString(this Random random)
    {
        return Guid.NewGuid().ToString();
    }
}
