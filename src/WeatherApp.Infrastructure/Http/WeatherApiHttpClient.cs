using System.Globalization;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using WeatherApp.Application.Models;
using WeatherApp.Infrastructure.Configuration;
using WeatherApp.Infrastructure.Dtos;

namespace WeatherApp.Infrastructure.Http;

public sealed class WeatherApiHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly WeatherApiOptions _options;

    public WeatherApiHttpClient(HttpClient httpClient, IOptions<WeatherApiOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;

        if (string.IsNullOrWhiteSpace(_options.BaseUrl))
        {
            throw new ArgumentException("Weather API base URL не может быть пустым.", nameof(options));
        }

        var baseUrl = _options.BaseUrl.TrimEnd('/') + "/";
        _httpClient.BaseAddress = new Uri(baseUrl);
    }

    public Task<ForecastWeatherResponseDto?> GetForecastAsync(GetWeatherReportQuery query, CancellationToken cancellationToken = default)
    {
        var url = BuildRequestUri(query);
        return _httpClient.GetFromJsonAsync<ForecastWeatherResponseDto>(url, cancellationToken);
    }

    private string BuildRequestUri(GetWeatherReportQuery query)
    {
        var latitude = query.Latitude.ToString(CultureInfo.InvariantCulture);
        var longitude = query.Longitude.ToString(CultureInfo.InvariantCulture);
        return $"forecast.json?key={_options.ApiKey}&q={latitude},{longitude}&days={query.Days}&aqi=no&alerts=no";
    }
}
