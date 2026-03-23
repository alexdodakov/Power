using System.Globalization;

namespace WeatherApp.Infrastructure.Parsing;

public sealed class DateTimeParser : IDateTimeParser
{
    public DateTime ParseDateTime(string value)
    {
        return DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var parsed)
            ? parsed
            : DateTime.UtcNow;
    }
}

public sealed class DateParser : IDateParser
{
    public DateTime ParseDate(string value)
    {
        return DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var parsed)
            ? parsed
            : DateTime.UtcNow;
    }
}
