using System.Threading;
using System.Threading.Tasks;
using ArchetypeCSharpCLI.Domain;

namespace ArchetypeCSharpCLI.Http.Weather;

public interface IWeatherClient
{
    Task<WeatherResult> GetCurrentAsync(decimal latitude, decimal longitude, string units = "metric", CancellationToken ct = default);
}

public sealed class WeatherResult
{
    public bool IsSuccess { get; }
    public WeatherReport? Report { get; }
    public string? ErrorMessage { get; }

    private WeatherResult(bool success, WeatherReport? report, string? error)
    {
        IsSuccess = success;
        Report = report;
        ErrorMessage = error;
    }

    public static WeatherResult Success(WeatherReport r) => new(true, r, null);
    public static WeatherResult Failure(string message) => new(false, null, message);
}
