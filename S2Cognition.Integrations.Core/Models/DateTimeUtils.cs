namespace S2Cognition.Integrations.Core.Models;

public interface IDateTimeUtils 
{
    DateTime Now { get; }
}

internal class DateTimeUtils : IDateTimeUtils
{
    public DateTime Now => DateTime.UtcNow;

    internal DateTimeUtils()
    { 
    }
}