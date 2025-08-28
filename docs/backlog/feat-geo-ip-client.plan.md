# Implementation Plan: feat-geo-ip-client

Tasks:

1. Create DTO and domain mapping
   - Add `IpApiResponseDto` under `Dtos/GeoIp`.
   - Implement mapping to `Domain/Location.cs`.

2. Define client interface
   - Create `IGeoIpClient` in `Http/GeoIp/IGeoIpClient.cs`.

3. Implement GeoIpClient
   - Implement `GeoIpClient` using typed HttpClient.
   - Handle success and failure (status field and http errors).

4. Register client in DI
   - Wire typed client registration in `Program.cs`.

5. Add minimal unit tests (deferred to testing phase).

Mark tasks as done in this document after implementation.
