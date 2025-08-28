# Feature Test Report — feat-dto-mapping

Scope: Validate mapping from external DTOs to internal domain models.

- Add unit tests to ensure:
  - IpApiResponse maps to valid Location (lat/lon bounds, city/country optional)
  - OpenMeteoResponse maps to WeatherReport (units conversion, description mapping, time parse)
  - Invalid responses throw meaningful exceptions (missing fields, bad ranges)

Note: Tests creation documented; execution deferred.

> Last updated: 2025-08-28.
