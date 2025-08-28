# Implementation Plan: feat-weather-output

Tasks:

1. Add `--units` option to WeatherCommand and thread value to handler.
2. Use units when calling IOpenMeteoMapper in WeatherClient or handler.
3. Format output string in WeatherHandler to include symbol and one decimal place.
