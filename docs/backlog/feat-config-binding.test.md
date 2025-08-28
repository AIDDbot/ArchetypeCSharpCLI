# F3.3 Bind typed options/POCOs — Test Report

Date: 2025-08-28

Scope
- Validate options binding from configuration sections with defaults and data annotations.
- Confirm environment variable precedence over files and custom validation hook.
- Verify IOptionsMonitor reflects file changes (reload) when using reloadOnChange.

Test Types
- Unit tests (xUnit) in `ConfigBindingTests.cs`

Cases
- Binds from section and preserves defaults when not provided
- Missing required property triggers OptionsValidationException
- Custom validation rejects invalid combinations with clear message
- Environment variables (Sample__Threshold) override file values
- IOptionsMonitor sees changes after rewriting appsettings.json

Result
- PASS — All new tests pass locally with the existing suite.

Artifacts
- Test run: `dotnet test` on Windows, .NET 9.0

Notes
- Tests write temporary `appsettings.json` under `AppContext.BaseDirectory` and clean up after run.
- The binding helper `AddBoundOptions<T>` is exercised with DataAnnotations + custom validation.

> End of Test Report for F3.3.