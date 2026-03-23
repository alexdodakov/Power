using WeatherApp.Api.Mapping;
using WeatherApp.Api.Models;
using WeatherApp.Domain.Models;

namespace WeatherApp.Api.Tests;

internal sealed class FakeWeatherReportDtoMapper : IWeatherReportDtoMapper
{
    public WeatherPageDto Result { get; set; } = new("", new CurrentWeatherDto(0, 0, "", "", 0, 0, 0, default), [], []);

    public WeatherReport? ReceivedReport { get; private set; }

    public WeatherPageDto Map(WeatherReport report)
    {
        ReceivedReport = report;
        return Result;
    }
}
