using WeatherApp.Infrastructure.Parsing;
using Xunit;

namespace WeatherApp.Infrastructure.Tests;

public sealed class DateTimeParsersTests
{
    [Fact]
    public void DateTimeParser_ParseDateTime_WhenValueIsValid_ShouldReturnParsedDate()
    {
        var parser = new DateTimeParser();

        var result = parser.ParseDateTime("2026-03-23 14:30");

        Assert.Equal(2026, result.Year);
        Assert.Equal(3, result.Month);
        Assert.Equal(23, result.Day);
        Assert.Equal(14, result.Hour);
        Assert.Equal(30, result.Minute);
    }

    [Fact]
    public void DateTimeParser_ParseDateTime_WhenValueIsInvalid_ShouldReturnUtcNowFallback()
    {
        var parser = new DateTimeParser();
        var before = DateTime.UtcNow;

        var result = parser.ParseDateTime("not-a-date");

        var after = DateTime.UtcNow;
        Assert.InRange(result, before, after);
    }

    [Fact]
    public void DateParser_ParseDate_WhenValueIsValid_ShouldReturnParsedDate()
    {
        var parser = new DateParser();

        var result = parser.ParseDate("2026-03-24");

        Assert.Equal(2026, result.Year);
        Assert.Equal(3, result.Month);
        Assert.Equal(24, result.Day);
    }

    [Fact]
    public void DateParser_ParseDate_WhenValueIsInvalid_ShouldReturnUtcNowFallback()
    {
        var parser = new DateParser();
        var before = DateTime.UtcNow;

        var result = parser.ParseDate("not-a-date");

        var after = DateTime.UtcNow;
        Assert.InRange(result, before, after);
    }
}
