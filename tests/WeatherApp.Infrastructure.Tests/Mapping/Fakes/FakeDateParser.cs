using WeatherApp.Infrastructure.Parsing;

namespace WeatherApp.Infrastructure.Tests;

internal sealed class FakeDateParser : IDateParser
{
    public List<string> ReceivedValues { get; } = [];

    public DateTime ParseDate(string value)
    {
        ReceivedValues.Add(value);
        return ReceivedValues.Count == 1 ? new DateTime(2030, 1, 2) : new DateTime(2030, 1, 3);
    }
}
