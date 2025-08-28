# Implementation Plan: feat-weather-client

Tasks:

1. Add DTO for OpenMeteo current weather
   - Create `Dtos/OpenMeteo/OpenMeteoCurrentDto.cs`.

2. Define IWeatherClient interface
   - Create `Http/Weather/IWeatherClient.cs` with GetCurrentAsync.

3. Implement WeatherClient
   - Implement typed client that calls `/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true`.
   - Map response to domain `WeatherReport` using `IOpenMeteoMapper`.

4. Register typed client in DI (AddHttpCore)
