using System;
using System.IO;
using System.Text.Json;
using ArchetypeCSharpCLI.Configuration;
using FluentAssertions;
using Xunit;

namespace ArchetypeCSharpCLI.Tests;

public class ConfigPipelineTests
{
    private sealed class EnvVarScope : IDisposable
    {
        private readonly (string key, string? prev)[] _pairs;
        public EnvVarScope(params (string key, string? value)[] vars)
        {
            _pairs = new (string, string?)[vars.Length];
            for (int i = 0; i < vars.Length; i++)
            {
                var (k, v) = vars[i];
                _pairs[i] = (k, Environment.GetEnvironmentVariable(k));
                if (v is null)
                    Environment.SetEnvironmentVariable(k, null);
                else
                    Environment.SetEnvironmentVariable(k, v);
            }
        }
        public void Dispose()
        {
            foreach (var (k, prev) in _pairs)
                Environment.SetEnvironmentVariable(k, prev);
        }
    }

    private sealed class TempSettingsFiles : IDisposable
    {
        private readonly string _baseDir = AppContext.BaseDirectory;
        private readonly string[] _created = Array.Empty<string>();

        public static TempSettingsFiles Create(object? appsettings = null, object? appsettingsEnv = null, string? envName = null)
        {
            var tsf = new TempSettingsFiles();
            var created = new System.Collections.Generic.List<string>();
            if (appsettings is not null)
            {
                var p = Path.Combine(tsf._baseDir, "appsettings.json");
                File.WriteAllText(p, JsonSerializer.Serialize(appsettings));
                created.Add(p);
            }
            if (appsettingsEnv is not null && !string.IsNullOrWhiteSpace(envName))
            {
                var p = Path.Combine(tsf._baseDir, $"appsettings.{envName}.json");
                File.WriteAllText(p, JsonSerializer.Serialize(appsettingsEnv));
                created.Add(p);
            }
            tsf._createdField = created.ToArray();
            return tsf;
        }

        private string[] _createdField = Array.Empty<string>();
        public void Dispose()
        {
            foreach (var p in _createdField)
            {
                try { if (File.Exists(p)) File.Delete(p); } catch { /* ignore */ }
            }
        }
    }

    [Fact]
    public void Defaults_Are_Applied_When_No_Files_Or_Env()
    {
        using var _env = new EnvVarScope(("DOTNET_ENVIRONMENT", null), ("ASPNETCORE_ENVIRONMENT", null),
                                         ("App__Environment", null), ("App__HttpTimeoutSeconds", null), ("App__LogLevel", null),
                                         ("Environment", null), ("HttpTimeoutSeconds", null), ("LogLevel", null));
        using var _files = TempSettingsFiles.Create();

        var cfg = ConfigBuilder.Build();

        cfg.Environment.Should().Be("Production");
        cfg.HttpTimeoutSeconds.Should().Be(30);
        cfg.LogLevel.Should().Be("Information");
    }

    [Fact]
    public void Appsettings_File_Values_Are_Read()
    {
        using var _env = new EnvVarScope(("DOTNET_ENVIRONMENT", null), ("ASPNETCORE_ENVIRONMENT", null));
        using var _files = TempSettingsFiles.Create(new
        {
            Environment = "Development",
            HttpTimeoutSeconds = 10,
            LogLevel = "Debug"
        });

        var cfg = ConfigBuilder.Build();

        cfg.Environment.Should().Be("Development");
        cfg.HttpTimeoutSeconds.Should().Be(10);
        cfg.LogLevel.Should().Be("Debug");
    }

    [Fact]
    public void Environment_Specific_File_Overrides_Base()
    {
        using var _env = new EnvVarScope(("DOTNET_ENVIRONMENT", "Development"), ("ASPNETCORE_ENVIRONMENT", null));
        using var _files = TempSettingsFiles.Create(
          appsettings: new { HttpTimeoutSeconds = 10 },
          appsettingsEnv: new { HttpTimeoutSeconds = 25 },
          envName: "Development");

        var cfg = ConfigBuilder.Build();

        cfg.HttpTimeoutSeconds.Should().Be(25);
    }

    [Fact]
    public void Env_Variables_Override_Files_With_Prefix()
    {
        using var _env = new EnvVarScope(("DOTNET_ENVIRONMENT", "Development"), ("App__HttpTimeoutSeconds", "45"));
        using var _files = TempSettingsFiles.Create(
          appsettings: new { HttpTimeoutSeconds = 10 },
          appsettingsEnv: new { HttpTimeoutSeconds = 25 },
          envName: "Development");

        var cfg = ConfigBuilder.Build();
        cfg.HttpTimeoutSeconds.Should().Be(45);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    [InlineData(999)]
    public void Invalid_HttpTimeout_Is_Clamped_To_Default(int invalid)
    {
        using var _env = new EnvVarScope(("DOTNET_ENVIRONMENT", null));
        using var _files = TempSettingsFiles.Create(new { HttpTimeoutSeconds = invalid });

        var cfg = ConfigBuilder.Build();
        cfg.HttpTimeoutSeconds.Should().Be(30);
    }
}
