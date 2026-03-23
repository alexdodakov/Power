using WeatherApp.Application.Abstractions;
using WeatherApp.Application.Models;

namespace WeatherApp.Application.Tests;

internal sealed class FakeWeatherApiClient : IWeatherApiClient
{
    public WeatherApiPayload Payload { get; set; } = new();

    public bool WasCalled { get; private set; }

    public GetWeatherReportQuery? ReceivedQuery { get; private set; }

    public Task<WeatherApiPayload> GetWeatherAsync(GetWeatherReportQuery query, CancellationToken cancellationToken = default)
    {
        WasCalled = true;
        ReceivedQuery = query;
        return Task.FromResult(Payload);
    }
}
