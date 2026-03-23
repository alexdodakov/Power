using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WeatherApp.Application.Mapping;

public sealed class WeatherConditionTextTranslator : IWeatherConditionTextTranslator
{
    private static readonly Regex MultiSpaceRegex = new("\\s+", RegexOptions.Compiled);
    private readonly IReadOnlyDictionary<string, string> _conditionTranslationMap;

    public WeatherConditionTextTranslator()
    {
        _conditionTranslationMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["Sunny"] = "Солнечно",
            ["Clear"] = "Ясно",
            ["Partly cloudy"] = "Переменная облачность",
            ["Cloudy"] = "Облачно",
            ["Overcast"] = "Пасмурно",
            ["Mist"] = "Мгла",
            ["Fog"] = "Туман",
            ["Freezing fog"] = "Ледяной туман",
            ["Patchy rain nearby"] = "Дождь поблизости",
            ["Patchy light drizzle"] = "Местами слабая морось",
            ["Light drizzle"] = "Слабая морось",
            ["Light rain"] = "Небольшой дождь",
            ["Moderate rain"] = "Умеренный дождь",
            ["Heavy rain"] = "Сильный дождь",
            ["Patchy snow nearby"] = "Снег поблизости",
            ["Light snow"] = "Небольшой снег",
            ["Heavy snow"] = "Сильный снег",
            ["Blizzard"] = "Метель",
            ["Thundery outbreaks possible"] = "Возможна гроза",
        };
    }

    public string Translate(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return text ?? string.Empty;
        }

        var normalized = Normalize(text);

        return _conditionTranslationMap.TryGetValue(normalized, out var translated)
            ? translated
            : normalized;
    }

    private static string Normalize(string value)
    {
        var trimmed = value.Trim();
        return MultiSpaceRegex.Replace(trimmed, " ");
    }
}
