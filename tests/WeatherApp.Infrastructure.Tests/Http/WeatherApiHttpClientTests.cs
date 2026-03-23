using System.Net;
using System.Text;
using Microsoft.Extensions.Options;
using WeatherApp.Application.Models;
using WeatherApp.Infrastructure.Configuration;
using WeatherApp.Infrastructure.Http;
using Xunit;

namespace WeatherApp.Infrastructure.Tests;

public sealed class WeatherApiHttpClientTests
{
    [Fact]
    public void Constructor_WhenBaseUrlIsMissing_ShouldThrowArgumentException()
    {
        var handler = new TestHttpMessageHandler(_ => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));
        using var httpClient = new HttpClient(handler);
        var options = Options.Create(new WeatherApiOptions { BaseUrl = "  ", ApiKey = "key" });

        Assert.Throws<ArgumentException>(() => new WeatherApiHttpClient(httpClient, options));
    }

    [Fact]
    public async Task GetForecastAsync_ShouldCallExpectedUriWithInvariantCoordinates()
    {
        var capturedRequestUri = string.Empty;
        var handler = new TestHttpMessageHandler(request =>
        {
            capturedRequestUri = request.RequestUri!.ToString();
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
        var client = new WeatherApiHttpClient(httpClient, options);
        var query = new GetWeatherReportQuery
        {
            Latitude = 55.7558m,
            Longitude = 37.6176m,
            Days = 3,
        };

        var response = await client.GetForecastAsync(query);

        Assert.NotNull(response);
        Assert.Equal("https://api.weather.local/forecast.json?key=secret&q=55.7558,37.6176&days=3&aqi=no&alerts=no", capturedRequestUri);
    }
}
