# Feature Test Report — feat-http-error-handling

Scope: Validate error mapping for timeouts, network failures, and non-2xx responses.

- Added unit tests in `ArchetypeCSharpCLI.Tests/HttpErrorHandlingTests.cs`:
  - Maps SocketException/HttpRequestException to Network (exit 1)
  - Maps TaskCanceledException to Timeout (exit 1)
  - Maps 4xx → ClientError (exit 2), 5xx → ServerError (exit 3)
  - HttpClientExtensions throws on non-2xx responses

Note: Tests created as part of Craftsman flow; execution deferred per project constraints.

> Last updated: 2025-08-28.
