# Design: Raw JSON output (feat-weather-raw)

## Overview

Add `--raw` option to `weather` command. When present, print the raw JSON body returned by the weather provider instead of formatted output.

## Implementation notes

- Thread `raw` flag from command to handler. Handler will request raw JSON from `IWeatherClient` (add an overload or include raw in response).
- Keep simplest approach: extend WeatherResult with an Optional RawJson string and have WeatherClient populate it when requested.
