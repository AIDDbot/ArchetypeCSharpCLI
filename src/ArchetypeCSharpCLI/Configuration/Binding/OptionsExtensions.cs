using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ArchetypeCSharpCLI.Configuration.Binding;

/// <summary>
/// Extensions to register bound and validated options.
/// </summary>
public static class OptionsExtensions
{
    /// <summary>
    /// Binds a configuration section to an options type, adds DataAnnotations validation,
    /// and allows optional post-configuration and custom validation predicate.
    /// </summary>
    public static IServiceCollection AddBoundOptions<T>(this IServiceCollection services, IConfiguration configuration, string sectionName,
        Action<T>? postConfigure = null, Func<T, bool>? validate = null, string? validateError = null)
        where T : class, new()
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);
        if (string.IsNullOrWhiteSpace(sectionName)) throw new ArgumentException("Section name is required", nameof(sectionName));

        services
            .AddOptions<T>()
            .Bind(configuration.GetSection(sectionName))
            .ValidateDataAnnotations();

        if (postConfigure is not null)
        {
            services.PostConfigure(postConfigure);
        }

        if (validate is not null)
        {
            services.PostConfigure<T>(o =>
            {
                if (!validate(o))
                {
                    throw new OptionsValidationException(typeof(T).Name, typeof(T), new[] { validateError ?? "custom validation failed" });
                }
            });
        }

        return services;
    }
}
