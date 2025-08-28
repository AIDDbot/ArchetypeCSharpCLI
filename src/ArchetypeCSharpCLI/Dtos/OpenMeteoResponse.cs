using System.Text.Json.Serialization;

namespace ArchetypeCSharpCLI.Dtos;

/// <summary>
/// DTO for open-meteo.com weather API response.
/// Provides current weather data with minimal fields.
/// </summary>
public class OpenMeteoResponse
{
    /// <summary>
    /// Current weather conditions.
    /// </summary>
    [JsonPropertyName("current")]
    public CurrentWeather? Current { get; set; }
}

/// <summary>
/// Current weather data from open-meteo.com.
/// </summary>
public class CurrentWeather
{
    /// <summary>
    /// Air temperature at 2 meters above ground in Celsius.
    /// </summary>
    [JsonPropertyName("temperature_2m")]
    public decimal? Temperature2m { get; set; }

    /// <summary>
    /// WMO weather interpretation code.
    /// </summary>
    [JsonPropertyName("weather_code")]
    public int? WeatherCode { get; set; }

    /// <summary>
    /// ISO 8601 timestamp of the current weather observation.
    /// </summary>
    [JsonPropertyName("time")]
    public string? Time { get; set; }
}
