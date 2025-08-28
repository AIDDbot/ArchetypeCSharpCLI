# feat-exit-codes Exit code policy and mapping Specification

Define a consistent, minimal exit code policy and mapping across the CLI so users and scripts can reliably handle outcomes.

- Epic: E5 Exit Codes & Error Experience
- Priority: ❕ Normal
- Product Requirements:
  - R7 Errors and Exit Codes: predictable exit codes and actionable failures

## User Story 1

- As a script author
- I want to get stable, documented exit codes for common failure categories
- So that I can write portable scripts that branch on error types

### Acceptance Criteria

- [ ] SHALL define and reserve exit codes for: success, validation errors, network/timeouts, HTTP client errors (4xx), HTTP server errors (5xx), and unexpected exceptions.
- [ ] WHEN a command fails due to invalid input/options or parse errors THEN the process exit code SHALL be 2.
- [ ] WHEN a network or timeout error occurs (DNS, connection refused, request canceled) THEN the process exit code SHALL be 1.
- [ ] WHEN an HTTP 4xx is returned THEN the process exit code SHALL be 2.
- [ ] WHEN an HTTP 5xx is returned THEN the process exit code SHALL be 3.
- [ ] WHEN an unexpected exception occurs THEN the process exit code SHALL be 4.
- [ ] WHERE multiple failures occur in a pipeline THEN the first failure determines the final exit code.

## User Story 2

- As a CLI user
- I want a single place in the codebase that defines exit codes
- So that behavior is consistent across commands and infrastructure

### Acceptance Criteria

- [ ] SHALL provide a central ExitCodes contract (enum or static class) with XML doc comments.
- [ ] SHALL add command-line middleware that maps parse errors and unhandled exceptions to ExitCodes.
- [ ] SHALL map HttpRequestException and HTTP responses to ExitCodes consistently with the policy.

> End of Feature Specification for feat-exit-codes, last updated 2025-08-28.
