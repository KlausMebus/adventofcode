---
name: xunit-v3-fsharp-mtp
description: Use when creating, updating, or troubleshooting F# unit tests with xUnit.net v3, especially package selection and Microsoft.Testing.Platform setup.
---

# xUnit.net v3 with F# and Microsoft.Testing.Platform

Use xUnit.net v3 for F# tests when that fits the repository's existing project structure. xUnit v3 supports F#, and its test projects are executables.

## Project setup

- Prefer the official `xunit3` template with `--language F#` when creating a new project; adapt existing project conventions when adding tests to an established repository.
- Set the test project to `OutputType` `Exe`, target a supported .NET framework, and list F# source files explicitly in compile order.
- Reference the latest stable `xunit.v3` package. Check the current xUnit.net release information rather than copying an old version.
- The `xunit.v3` package selects xUnit's default Microsoft.Testing.Platform (MTP) integration. `xunit.v3.mtp-v2` explicitly pins MTP v2; use it only when that pin is intentional. The default MTP major version can change with xUnit releases.
- In the xUnit package line starting at `4.0.0`, the default is MTP v2; earlier `3.x` packages defaulted to MTP v1.
- MTP is cross-platform, including Windows. xUnit v3 also retains VSTest compatibility; keep VSTest-related packages only when supported IDEs or CI still require that path.
- Ionide (VS Code F#) discovers and runs tests through VSTest and only recognizes projects referencing `Microsoft.NET.Test.Sdk`. For its Test Explorer, also reference `Microsoft.NET.Test.Sdk` and `xunit.runner.visualstudio`; otherwise it reports "No test projects found". These packages don't interfere with MTP `dotnet test`.

## Selecting the runner

These are separate choices:

- To make `dotnet test` use its native MTP mode, select MTP in a `global.json` at the solution or repository scope:

  ```json
  {
    "test": {
      "runner": "Microsoft.Testing.Platform"
    }
  }
  ```

- To use MTP's command-line experience when invoking the test project with `dotnet run`, set `UseMicrosoftTestingPlatformRunner` to `true` in the project. Without this property, xUnit's own command-line runner can remain the `dotnet run` experience even when MTP support is included.

Choose the narrowest `global.json` scope that covers the intended test projects, and ensure all test projects in that scope support the selected runner.

## F# test shape

Use `open Xunit` and `[<Fact>]` / `[<Theory>]` attributes. Top-level test functions in an F# module are supported; class members are also useful for data-driven tests. Reference the production project and test its computation directly, avoiding file I/O when the behavior under test does not require it.

## References

- [xUnit.net v3 for F#](https://xunit.net/?tabs=fs)
- [xUnit.net v3 with Microsoft.Testing.Platform](https://xunit.net/docs/getting-started/v3/microsoft-testing-platform.html)
- [Microsoft `dotnet test` runner selection](https://learn.microsoft.com/dotnet/core/tools/dotnet-test#choose-a-test-runner)
