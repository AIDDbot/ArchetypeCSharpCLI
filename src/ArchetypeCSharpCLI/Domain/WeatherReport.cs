namespace ArchetypeCSharpCLI.Domain;

/// <summary>
/// Represents a weather report for a specific location and time.
/// </summary>
public class WeatherReport
{
  /// <summary>
  /// Current temperature.
  /// </summary>
  public decimal Temperature { get; }

  /// <summary>
  /// Unit system for the temperature (metric or imperial).
  /// </summary>
  public string Units { get; }

  /// <summary>
  /// Human-readable weather condition description.
  /// </summary>
  public string Condition { get; }

  /// <summary>
  /// Timestamp when the weather was observed.
  /// </summary>
  public DateTime ObservedAt { get; }

  /// <summary>
  /// Source of the weather data (e.g., "open-meteo").
  /// </summary>
  public string Source { get; }

  /// <summary>
  /// Initializes a new instance of the WeatherReport class.
  /// </summary>
  public WeatherReport(decimal temperature, string units, string condition, DateTime observedAt, string source)
  {
    if (string.IsNullOrWhiteSpace(units))
      throw new ArgumentException("Units cannot be null or empty.", nameof(units));

    if (string.IsNullOrWhiteSpace(condition))
      throw new ArgumentException("Condition cannot be null or empty.", nameof(condition));

    if (string.IsNullOrWhiteSpace(source))
      throw new ArgumentException("Source cannot be null or empty.", nameof(source));

    Temperature = temperature;
    Units = units;
    Condition = condition;
    ObservedAt = observedAt;
    Source = source;
  }
}
