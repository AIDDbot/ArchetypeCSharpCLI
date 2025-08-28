# Feature: feat-weather-command

Implement the `weather` command that composes GeoIP + Weather clients and prints user-friendly output.

## User Stories

- As a user I WANT to run `archetype weather` with optional `--lat` and `--lon` flags SO THAT I can view current weather for my location or a specified coordinate.

## Acceptance Criteria (EARS)

- SHALL use provided coordinates WHEN `--lat` and `--lon` are given; WHEN not provided SHALL call IGeoIpClient to obtain coordinates; IF geo lookup fails THEN return mapped exit code and message.

- SHALL call IWeatherClient with the final coordinates and print a human-friendly WeatherReport WHEN the call succeeds; IF `--raw` is provided THEN print raw JSON (handled in later feature).

- SHALL accept `--timeout` seconds to override the HTTP timeout for the composed operation.
