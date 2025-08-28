# Design: Weather output and units (feat-weather-output)

## Overview

Allow `--units` option and format the human-friendly output using WeatherReport properties. Prefer minimal changes: pass units to mapper when creating WeatherReport.

## Changes

- Add `--units` option to `WeatherCommand` with allowed values `metric` and `imperial`.
- Ensure `IOpenMeteoMapper.MapToWeatherReport` supports units parameter (already present).
- Format output in `WeatherHandler` to include unit symbol and one decimal.
