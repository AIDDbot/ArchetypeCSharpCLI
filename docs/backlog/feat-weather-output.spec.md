# Feature: feat-weather-output

Provide human-friendly output formatting and support `--units` option (metric|imperial).

## User Stories

- As a user I WANT to choose units (metric or imperial) WHEN requesting weather SO THAT temperatures are shown in my preferred system.

## Acceptance Criteria (EARS)

- SHALL accept `--units` with values `metric` or `imperial` WHEN provided; IF omitted THEN default to `metric`.

- SHALL format output with one decimal place for temperature and include unit label (°C or °F) WHEN printing human-friendly output.
