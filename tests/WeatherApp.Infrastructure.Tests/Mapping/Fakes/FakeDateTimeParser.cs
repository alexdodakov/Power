using WeatherApp.Infrastructure.Parsing;

namespace WeatherApp.Infrastructure.Tests;

internal sealed class FakeDateTimeParser : IDateTimeParser
{
    public List<string> ReceivedValues { get; } = [];

    public DateTime ParseDateTime(string value)
    {
        ReceivedValues.Add(value);
        return ReceivedValues.Count == 1
            ? new DateTime(2030, 1, 1, 8, 0, 0, DateTimeKind.Unspecified)
            : ReceivedValues.Count == 2
                ? new DateTime(2030, 1, 1, 9, 0, 0, DateTimeKind.Unspecified)
                : new DateTime(2030, 1, 1, 8, 0, 0, DateTimeKind.Unspecified);
    }
}
