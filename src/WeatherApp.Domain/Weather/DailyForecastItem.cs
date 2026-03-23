namespace WeatherApp.Domain.Models;

public sealed class DailyForecastItem
{
    public DateTime Date { get; set; }

    public decimal MinTemperatureC { get; set; }

    public decimal MaxTemperatureC { get; set; }

    public string ConditionText { get; set; } = string.Empty;

    public string ConditionIconUrl { get; set; } = string.Empty;
}
