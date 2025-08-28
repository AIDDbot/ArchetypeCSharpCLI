# Feature: feat-geo-ip-client

Resolve public IP to geographic coordinates using ip-api.com.

## User Stories

- As a user I WANT the CLI to determine my current approximate location FROM my public IP SO THAT I can run commands that depend on location without providing coordinates.

## Acceptance Criteria (EARS)

- SHALL call ip-api.com/json to resolve the caller IP WHEN the user does not provide explicit coordinates; IF the network is available THEN map the successful response to an internal Location model WHILE returning latitude and longitude and a human-readable location name.

- SHALL return a clear, mapped error WHEN the http request times out or the provider returns an error status IF network problems occur THEN map to the appropriate exit code and produce a concise user-facing message.

- SHALL expose a simple client API (interface) to retrieve Location or error result WHEN consumed by command handlers IF caching or retry is required THEN keep the first implementation minimal (no cache, single attempt) to start.

## Implementation notes

- Use a typed HttpClient registration: IGeoIpClient with a method Task<Result<Location, Error>> GetLocationAsync(CancellationToken).
- Minimal DTO for ip-api.com subset: lat, lon, city, regionName, country.
- Map DTO to Domain `Location` model in `src/ArchetypeCSharpCLI/Domain/Location.cs`.

<!-- Commit with docs message -->
