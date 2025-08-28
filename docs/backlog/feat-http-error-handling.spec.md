# feat-http-error-handling Error handling and non-2xx mapping Specification

Handle timeouts, network errors, and non-2xx responses with clear messages and mapped exit codes.

- **Epic**: E4
- **Priority**: ❗ High
- **Product Requirements**: 
  - R6 HTTP and Resilience
  - R7 Errors and Exit Codes

## User Story 1

- **As a** developer using the CLI
- **I want to** receive clear error messages when HTTP calls fail
- **So that** I can understand what went wrong and how to fix it

### Acceptance Criteria

- [ ] SHALL display user-friendly error messages for network timeouts
- [ ] SHALL display user-friendly error messages for DNS resolution failures
- [ ] SHALL display user-friendly error messages for connection refused errors
- [ ] SHALL display user-friendly error messages for SSL/TLS errors
- [ ] SHALL display user-friendly error messages for non-2xx HTTP status codes
- [ ] WHEN an HTTP error occurs, THEN log the technical details at Debug level
- [ ] WHEN an HTTP error occurs in Release mode, THEN suppress stack traces in user output

## User Story 2

- **As a** developer scripting the CLI
- **I want to** get consistent exit codes for different types of HTTP errors
- **So that** I can handle failures programmatically in scripts

### Acceptance Criteria

- [ ] SHALL return exit code 1 for network errors (timeout, DNS, connection)
- [ ] SHALL return exit code 2 for HTTP client errors (4xx status codes)
- [ ] SHALL return exit code 3 for HTTP server errors (5xx status codes)
- [ ] SHALL return exit code 4 for unexpected exceptions during HTTP calls
- [ ] WHEN multiple HTTP calls fail in sequence, THEN return the exit code of the first failure

## User Story 3

- **As a** developer using the CLI
- **I want to** see actionable guidance when HTTP errors occur
- **So that** I can resolve issues quickly

### Acceptance Criteria

- [ ] SHALL suggest checking network connectivity for connection errors
- [ ] SHALL suggest verifying API endpoints for 4xx errors
- [ ] SHALL suggest retrying later for 5xx errors
- [ ] SHALL include correlation information (command, timestamp) in error logs
- [ ] WHEN timeout occurs, THEN suggest increasing --timeout value if applicable

> End of Feature Specification for feat-http-error-handling, last updated 2025-08-28.</content>
<parameter name="filePath">c:\code\aidd\ArchetypeCSharpCLI\docs\backlog\feat-http-error-handling.spec.md
