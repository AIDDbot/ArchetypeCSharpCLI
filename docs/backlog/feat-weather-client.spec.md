# Feature: feat-weather-client

Call Open‑Meteo to retrieve current weather given coordinates.

## User Stories

- As a user I WANT the CLI to fetch current weather FOR given coordinates SO THAT I can see the local conditions.

## Acceptance Criteria (EARS)

- SHALL call Open‑Meteo's current weather endpoint WHEN provided coordinates are available; IF the call succeeds THEN map response to domain WeatherReport model.

- SHALL surface concise errors WHEN network or provider errors occur IF non-2xx or timeout THEN map to exit codes and user-facing messages.

- SHALL support a minimal query (latitude, longitude) and return temperature, wind speed, and weather code.

## Implementation notes

- Provide IWeatherClient with Task<WeatherResult> GetCurrentAsync(decimal lat, decimal lon, CancellationToken ct).
- Minimal DTO for Open‑Meteo current_weather subset.
