# Design: Weather command handler (feat-weather-command)

## Overview

Implement a `weather` command under `Commands/Weather` that resolves coordinates, calls the weather client, and prints results.

## Components

- Command: `WeatherCommand` which registers `weather` with options `--lat`, `--lon`, `--timeout`, `--units`, `--raw`.
- Handler: `WeatherHandler` class that composes `IGeoIpClient` and `IWeatherClient`.

## Flow

1. Parse options.
2. If lat/lon provided use them; otherwise call `IGeoIpClient.GetLocationAsync`.
3. Call `IWeatherClient.GetCurrentAsync` with coordinates and cancellation token based on timeout.
4. On success, print a human-readable output via console (Console.Out) using WeatherReport properties.
5. Map known errors to ExitCodes.

## Edge cases

- GeoIP failure → map to network/provider exit codes.
- Weather provider failure → map to network/provider exit codes.
- Invalid coordinates → validation error.
