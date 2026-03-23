namespace WeatherApp.Infrastructure.Parsing;

public interface IDateParser
{
    DateTime ParseDate(string value);
}
