# Design: Weather API client (feat-weather-client)

## Overview

Provide a typed client that calls Open‑Meteo's endpoint to retrieve current weather for coordinates.

## Components

- IWeatherClient in `Http/Weather` with method GetCurrentAsync(lat, lon, ct).
- DTOs: OpenMeteoCurrentDto under `Dtos/OpenMeteo`.
- Mapper: IOpenMeteoMapper (already registered in AddHttpCore) maps DTO → Domain/WeatherReport.

## Client behavior

- Use typed HttpClient registration with base address https://api.open-meteo.com
- Build query parameters for latitude, longitude, and current_weather=true
- Use configured Http timeout and rely on HttpErrorHandler to map status codes.

## Data models

- DTO: current_weather { temperature, windspeed, weathercode }
- Domain: WeatherReport { decimal TemperatureC, decimal WindSpeedMs, int WeatherCode }
