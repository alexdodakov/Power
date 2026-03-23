using WeatherApp.Application.Abstractions;
using WeatherApp.Application.Models;
using WeatherApp.Domain.Models;

namespace WeatherApp.Api.Tests;

internal sealed class FakeWeatherFacade : IWeatherFacade
{
    public WeatherReport Result { get; set; } = new();

    public GetWeatherReportQuery? ReceivedQuery { get; private set; }

    public CancellationToken ReceivedCancellationToken { get; private set; }

    public WeatherReport? ReceivedReportRequestResultSource { get; private set; }

    public Task<WeatherReport> GetWeatherReportAsync(GetWeatherReportQuery query, CancellationToken cancellationToken = default)
    {
        ReceivedQuery = query;
        ReceivedCancellationToken = cancellationToken;
        ReceivedReportRequestResultSource = Result;
        return Task.FromResult(Result);
    }
}
