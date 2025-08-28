# Implementation Plan

## Overview

Implement the HTTP foundation using IHttpClientFactory with centralized defaults (timeout and User-Agent) and minimal wiring in Program.cs. Keep scope small; no retries/policies yet.

## Context

- DOMAIN.md, SYSTEMS.md
- Feature spec: ./feat-http-typed-client.spec.md
- Feature design: ./feat-http-typed-client.design.md
- Architecture instructions: ../..//.github/instructions/bst_architecture.instructions.md
- STRUCTURE.md

## Tasks

- [x] 1. Add package reference for HttpClientFactory
  - Edit `src/ArchetypeCSharpCLI/ArchetypeCSharpCLI.csproj`
  - Add `<PackageReference Include="Microsoft.Extensions.Http" Version="9.0.0" />`

- [x] 2. Include new Http folder in project build
  - Edit `.csproj` and add `<Compile Include="Http\**\*.cs" />`

- [x] 3. Create VersionInfo helper
  - Add `src/ArchetypeCSharpCLI/Http/VersionInfo.cs`
  - Implement `GetInformationalVersion()` using `AssemblyInformationalVersionAttribute` fallback to `AssemblyName.Version`

- [x] 4. Create HttpServiceCollectionExtensions
  - Add `src/ArchetypeCSharpCLI/Http/HttpServiceCollectionExtensions.cs`
  - Implement `AddHttpCore(this IServiceCollection, IConfiguration)` overload (binds `AppConfig` and forwards)
  - Implement `AddHttpCore(this IServiceCollection, AppConfig)` overload
  - Register base factory: `services.AddHttpClient()`
  - Configure defaults for all clients via `services.ConfigureAll<HttpClientFactoryOptions>(opts => ...)`
    - Clamp timeout to [1, 60] using `AppConfig.HttpTimeoutSeconds`
    - Set `User-Agent` header to `ArchetypeCSharpCLI/{Version}` (ProductInfoHeaderValue)

- [x] 5. Wire AddHttpCore at startup
  - Edit `src/ArchetypeCSharpCLI/Program.cs`
  - In `OptionsBootstrap.Init(rawConfig, services => { ... })` call `services.AddHttpCore(rawConfig)`

- [x] 6. Ensure namespace and usings consistency
  - Use `ArchetypeCSharpCLI.Http` namespace for new files
  - Add necessary usings: `Microsoft.Extensions.DependencyInjection`, `Microsoft.Extensions.Http`, `System.Net.Http.Headers`, `System.Reflection`, `Microsoft.Extensions.Configuration`

> End of Feature Implementation Tasks for F4.1, last updated 2025-08-28.
