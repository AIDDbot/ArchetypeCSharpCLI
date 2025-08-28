using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ArchetypeCSharpCLI.Configuration.Binding;

/// <summary>
/// Static access to options services backed by the bootstrap service provider.
/// </summary>
public static class OptionsAccess
{
  private static IServiceProvider? ServiceProvider => OptionsBootstrap.OptionsBootstrapAccessor.ServiceProvider;

  public static IOptions<T> Get<T>() where T : class
  {
      if (ServiceProvider is null)
          throw new InvalidOperationException("OptionsBootstrap.Init must be called before accessing options.");
      return ServiceProvider.GetRequiredService<IOptions<T>>();
  }

  public static IOptionsMonitor<T> GetMonitor<T>() where T : class
  {
      if (ServiceProvider is null)
          throw new InvalidOperationException("OptionsBootstrap.Init must be called before accessing options.");
      return ServiceProvider.GetRequiredService<IOptionsMonitor<T>>();
  }
}
