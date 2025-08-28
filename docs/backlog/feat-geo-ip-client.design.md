# Design: IP Geolocation client (feat-geo-ip-client)

## Overview

Provide a small, testable client that calls ip-api.com/json and maps the response to the domain `Location` model.

## Containers and components

- CLI app (host): will register the typed client and consume it from the weather command handler.
- Http layer: `GeoIpClient` implementing `IGeoIpClient` under `src/ArchetypeCSharpCLI/Http/GeoIp`.
- DTOs: `IpApiResponseDto` in `src/ArchetypeCSharpCLI/Dtos/GeoIp`.
- Mapping: `GeoIpMapper` or internal mapping function to convert DTO → Domain `Location`.

## API Contract

- Interface: IGeoIpClient
  - Task<LocationResult> GetLocationAsync(CancellationToken ct)

LocationResult is a small discriminated-result (Success with Location | Failure with ErrorCode/message).

## Error handling

- Map network timeouts to a network error exit code (use existing `ExitCodes` mapping).
- On non-success provider responses, return a provider error with message.

## Data models

- DTO: lat, lon, city, regionName, country, status, message
- Domain: Location { double Latitude, double Longitude, string City, string Region, string Country }

## Dependencies

- Microsoft.Extensions.Http (already present by project conventions)

<!-- Commit with docs message -->
