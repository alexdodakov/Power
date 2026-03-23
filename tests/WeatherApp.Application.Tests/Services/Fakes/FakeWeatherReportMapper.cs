using WeatherApp.Application.Mapping;
using WeatherApp.Application.Models;
using WeatherApp.Domain.Models;

namespace WeatherApp.Application.Tests;

internal sealed class FakeWeatherReportMapper : IWeatherReportMapper
{
    public WeatherReport Result { get; set; } = new();

    public WeatherReport Map(WeatherApiPayload payload)
    {
        return Result;
    }
}
