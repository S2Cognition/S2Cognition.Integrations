namespace S2Cognition.Integrations.Core.Models;

public interface IDateTime 
{
    DateTime Now { get; }
}

public class DateTimeUtils : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}