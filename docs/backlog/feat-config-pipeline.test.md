# F3.1 Configuration pipeline — Test Report

Date: 2025-08-28

Scope
- Validate configuration sources precedence and safe defaults.
- Confirm environment variable overrides and invalid value clamping.

Test Types
- Unit tests (xUnit) in `ConfigPipelineTests.cs`

Cases
- Defaults when no files or env present: Production/30/Information
- appsettings.json values are read
- appsettings.{Environment}.json overrides base
- Env vars (App__HttpTimeoutSeconds) override files
- Invalid HttpTimeoutSeconds values are clamped to 30

Result
- PASS — 17 tests total (including existing host and routing), 0 failures.

Artifacts
- Test run: `dotnet test` on Windows, .NET 9.0.8

Notes
- Binding order updated so nested section (App__) overrides flat keys, matching spec.
