using WeatherApp.Application.Abstractions;
using WeatherApp.Application.Models;
using WeatherApp.Infrastructure.Http;
using WeatherApp.Infrastructure.Mapping;

namespace WeatherApp.Infrastructure.Clients;

public sealed class WeatherApiClient : IWeatherApiClient
{
    private readonly WeatherApiHttpClient _httpClient;
    private readonly IWeatherApiResponseMapper _mapper;

    public WeatherApiClient(WeatherApiHttpClient httpClient, IWeatherApiResponseMapper mapper)
    {
        _httpClient = httpClient;
        _mapper = mapper;
    }

    public async Task<WeatherApiPayload> GetWeatherAsync(GetWeatherReportQuery query, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetForecastAsync(query, cancellationToken);

        if (response == null)
        {
            throw new InvalidOperationException("Weather API возвратил пустой ответ.");
        }

        return _mapper.ToPayload(response);
    }
}
