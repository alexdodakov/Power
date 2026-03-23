using System.Net;
using System.Text;
using Microsoft.Extensions.Options;
using WeatherApp.Application.Models;
using WeatherApp.Infrastructure.Clients;
using WeatherApp.Infrastructure.Configuration;
using WeatherApp.Infrastructure.Http;
using Xunit;

namespace WeatherApp.Infrastructure.Tests;

public sealed class WeatherApiClientTests
{
    [Fact]
    public async Task GetWeatherAsync_WhenHttpClientReturnsNull_ShouldThrowInvalidOperationException()
    {
        var handler = new TestHttpMessageHandler(_ =>
            Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("null", Encoding.UTF8, "application/json"),
            }));
        using var httpClient = new HttpClient(handler);
        var options = Options.Create(new WeatherApiOptions
        {
            BaseUrl = "https://api.weather.local",
            ApiKey = "secret",
        });
        var apiHttpClient = new WeatherApiHttpClient(httpClient, options);
        var mapper = new FakeWeatherApiResponseMapper();
        var client = new WeatherApiClient(apiHttpClient, mapper);

        await Assert.ThrowsAsync<InvalidOperationException>(() => client.GetWeatherAsync(new GetWeatherReportQuery()));
        Assert.False(mapper.WasCalled);
    }

    [Fact]
    public async Task GetWeatherAsync_WhenHttpClientReturnsData_ShouldMapAndReturnPayload()
    {
        var handler = new TestHttpMessageHandler(_ =>
        {
            var json = """
            {
              "location": { "name": "Moscow", "region": "Moscow" },
              "current": {
                "temp_c": 1,
                "feelslike_c": 1,
                "condition": { "text": "Clear", "icon": "//icon" },
                "humidity": 1,
                "wind_kph": 1,
                "pressure_mb": 1,
                "last_updated": "2026-03-23 10:00"
              },
              "forecast": { "forecastday": [] }
            }
            """;

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            });
        });
        using var httpClient = new HttpClient(handler);
        var options = Options.Create(new WeatherApiOptions
        {
            BaseUrl = "https://api.weather.local",
            ApiKey = "secret",
        });
        var apiHttpClient = new WeatherApiHttpClient(httpClient, options);
        var mapper = new FakeWeatherApiResponseMapper
        {
            Result = new WeatherApiPayload { CityName = "Mapped" },
        };
        var client = new WeatherApiClient(apiHttpClient, mapper);

        var result = await client.GetWeatherAsync(new GetWeatherReportQuery());

        Assert.True(mapper.WasCalled);
        Assert.Equal("Mapped", result.CityName);
    }
}
