# Test Report — F1.1 CLI host skeleton and help/version

Status: Passed

Summary:

- dotnet build: PASS
- dotnet test: PASS (5 tests)
- Manual smoke runs:
  - `--help`: shows usage and options (via default help)
  - `--version` and `-v`: print semantic version and exit 0
  - `--help --version`: help shown (help precedence)

Artifacts:

- Test results: `src/ArchetypeCSharpCLI.Tests/TestResults/test-results.trx`

> Updated on 2025-08-28.
