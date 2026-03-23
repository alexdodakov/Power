namespace WeatherApp.Infrastructure.Parsing;

public interface IDateTimeParser
{
    DateTime ParseDateTime(string value);
}
