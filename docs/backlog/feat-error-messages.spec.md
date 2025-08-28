# feat-error-messages User-friendly error messages Specification

Provide clear, concise, and actionable error output by default, hiding technical details unless verbose logging is enabled.

- Epic: E5 Exit Codes & Error Experience
- Priority: ❕ Normal
- Product Requirements:
  - R7 Errors and Exit Codes: actionable failures without noise

## User Story 1

- As a CLI user
- I want short and actionable errors on stderr
- So that I can understand what went wrong and how to fix it

### Acceptance Criteria

- [ ] SHALL print a single-line summary starting with "Error:" followed by a human-readable message.
- [ ] WHEN a correlation ID exists THEN append it as "(Correlation ID: <id>)".
- [ ] SHALL not print stack traces by default in Release configuration.

## User Story 2

- As a developer debugging issues
- I want to enable verbose details
- So that I can inspect technical data without changing the command behavior

### Acceptance Criteria

- [ ] WHEN environment variable AIDDBOT_VERBOSE=1 is set THEN include exception details in logs (not in stderr user message).
- [ ] SHALL keep process exit codes unaffected by verbosity.

> End of Feature Specification for feat-error-messages, last updated 2025-08-28.
