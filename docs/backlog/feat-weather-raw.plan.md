# Implementation Plan: feat-weather-raw

Tasks:

1. Add `--raw` option to WeatherCommand and thread to handler.
2. Extend WeatherResult with `RawJson` property.
3. WeatherClient: when requested, capture raw JSON response (GetStringAsync) and return it in WeatherResult.RawJson.
4. WeatherHandler: if raw is requested, print RawJson and skip formatted output.
