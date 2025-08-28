using ArchetypeCSharpCLI.Domain;
using ArchetypeCSharpCLI.Dtos;

namespace ArchetypeCSharpCLI.Mappers;

/// <summary>
/// Interface for mapping OpenMeteoResponse to WeatherReport domain model.
/// </summary>
public interface IOpenMeteoMapper
{
  /// <summary>
  /// Maps an OpenMeteoResponse to a WeatherReport domain model.
  /// </summary>
  /// <param name="response">The API response to map.</param>
  /// <param name="units">The desired unit system (metric or imperial).</param>
  /// <returns>A WeatherReport domain model.</returns>
  /// <exception cref="InvalidOperationException">Thrown when required fields are missing or invalid.</exception>
  WeatherReport MapToWeatherReport(OpenMeteoResponse response, string units);
}

/// <summary>
/// Default implementation of Open-Meteo response mapper.
/// </summary>
public class OpenMeteoMapper : IOpenMeteoMapper
{
  /// <summary>
  /// Maps an OpenMeteoResponse to a WeatherReport domain model.
  /// </summary>
  public WeatherReport MapToWeatherReport(OpenMeteoResponse response, string units)
  {
    if (response == null)
      throw new ArgumentNullException(nameof(response));

    if (response.Current == null)
      throw new InvalidOperationException("Current weather data is required but was not provided by Open-Meteo.");

    if (!response.Current.Temperature2m.HasValue)
      throw new InvalidOperationException("Temperature is required but was not provided by Open-Meteo.");

    if (!response.Current.WeatherCode.HasValue)
      throw new InvalidOperationException("Weather code is required but was not provided by Open-Meteo.");

    if (string.IsNullOrEmpty(response.Current.Time))
      throw new InvalidOperationException("Observation time is required but was not provided by Open-Meteo.");

    // Parse observation time
    if (!DateTime.TryParse(response.Current.Time, out var observedAt))
      throw new InvalidOperationException($"Invalid observation time format: {response.Current.Time}");

    // Convert temperature if imperial units requested
    var temperature = response.Current.Temperature2m.Value;
    var actualUnits = units;

    if (units.Equals("imperial", StringComparison.OrdinalIgnoreCase))
    {
      temperature = CelsiusToFahrenheit(temperature);
    }
    else
    {
      actualUnits = "metric";
    }

    // Get weather description
    var condition = WeatherCodeMapper.GetWeatherDescription(response.Current.WeatherCode.Value);

    return new WeatherReport(
        temperature: Math.Round(temperature, 1),
        units: actualUnits,
        condition: condition,
        observedAt: observedAt,
        source: "open-meteo"
    );
  }

  /// <summary>
  /// Converts Celsius to Fahrenheit.
  /// </summary>
  private static decimal CelsiusToFahrenheit(decimal celsius)
  {
    return (celsius * 9 / 5) + 32;
  }
}
