using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArchetypeCSharpCLI.Configuration.Binding;

/// <summary>
/// Minimal bootstrapper to host Options and Configuration without adopting full DI yet.
/// Builds a <see cref="ServiceProvider"/> with Options services and stores it for access.
/// </summary>
public static class OptionsBootstrap
{
    private static IServiceProvider? _serviceProvider;

    /// <summary>
    /// Initializes the options container using the provided configuration.
    /// You can extend registrations via the optional configure callback.
    /// </summary>
    public static IServiceProvider Init(IConfiguration configuration, Action<IServiceCollection>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var services = new ServiceCollection();
        services.AddSingleton(configuration);
        services.AddOptions();

        configure?.Invoke(services);

        _serviceProvider = services.BuildServiceProvider();
        OptionsBootstrapAccessor.ServiceProvider = _serviceProvider;
        return _serviceProvider;
    }

    internal static class OptionsBootstrapAccessor
    {
        internal static IServiceProvider? ServiceProvider { get; set; }
    }
}
