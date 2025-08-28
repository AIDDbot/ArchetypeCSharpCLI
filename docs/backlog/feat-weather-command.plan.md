# Implementation Plan: feat-weather-command

Tasks:

1. Add Command registration
   - Create `Commands/Weather/WeatherCommand.cs` to register options and handler delegate.

2. Implement WeatherHandler
   - Create `Commands/Weather/WeatherHandler.cs` to compose IGeoIpClient + IWeatherClient.

3. Wire command into CommandFactory
   - Register the `weather` command in `Commands/CommandFactory.cs`.

4. Update BACKLOG status to ✨ CODED after implementation.
