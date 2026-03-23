namespace WeatherApp.Web.Models;

public sealed class WeatherPageViewModel
{
    public string CityName { get; set; } = string.Empty;

    public bool HasError { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;
}
