using WeatherApp.Application.Mapping;

namespace WeatherApp.Application.Tests;

internal sealed class PrefixTranslator : IWeatherConditionTextTranslator
{
    public List<string> Received { get; } = [];

    public string Translate(string text)
    {
        Received.Add(text);
        return $"RU:{text}";
    }
}
