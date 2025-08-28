# Test Report — F1.2 Command routing and option validation

Status: ✅ Passed

Scope
- Host behavior remains: default help, --help precedence, --version/-v prints semver
- Command routing: `hello` command executes
- Option validation: required `--name|-n` and non-empty validator
- Error handling: unknown command produces parse error and non-zero exit

Test Artifacts
- Unit/Process tests (xUnit):
  - `CliHostTests` (existing)
  - `CommandRoutingTests` (new)

Summary
- All tests passed locally on .NET 9.0. Build and test completed in CI time bounds.
- No regressions observed.

> End of F1.2 test report.
