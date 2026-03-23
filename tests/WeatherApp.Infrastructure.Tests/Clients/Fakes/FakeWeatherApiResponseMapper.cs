using WeatherApp.Application.Models;
using WeatherApp.Infrastructure.Dtos;
using WeatherApp.Infrastructure.Mapping;

namespace WeatherApp.Infrastructure.Tests;

internal sealed class FakeWeatherApiResponseMapper : IWeatherApiResponseMapper
{
    public bool WasCalled { get; private set; }

    public WeatherApiPayload Result { get; set; } = new();

    public WeatherApiPayload ToPayload(ForecastWeatherResponseDto response)
    {
        WasCalled = true;
        return Result;
    }
}
