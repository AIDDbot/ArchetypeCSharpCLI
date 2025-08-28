# F4.1 Typed HttpClient with sensible timeouts — Test Report

Date: 2025-08-28
Status: ✅ Passed
Scope: Registration of IHttpClientFactory, default timeout and clamping, typed client inheritance, and default User-Agent header.

## Summary

- Registers IHttpClientFactory and applies global HttpClient defaults via `AddHttpCore`.
- Timeout sourced from configuration and clamped to [1, 60] seconds; default 30.
- Typed clients created via `AddHttpClient<TClient,TImpl>` inherit the defaults.
- Default `User-Agent` header is set to `ArchetypeCSharpCLI/{InformationalVersion}`.

## Tests

- Unit tests: `src/ArchetypeCSharpCLI.Tests/HttpTypedClientTests.cs`
- Runner: xUnit on .NET 9
- Result: All tests passed (see TRX under `src/ArchetypeCSharpCLI.Tests/TestResults/test-results.trx`).

## Notable Cases

- Configured timeout 0 -> clamped to 1 second
- Configured timeout 100 -> clamped to 60 seconds
- Environment precedence: env-specific file applies; environment variable `App__HttpTimeoutSeconds` overrides file

> End of Test Report.
