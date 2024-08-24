using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Shing.Common.Architecture.Mailing.DevelopmentTests.DependencyInjection;

internal static class DependencyInjectionBuilderExtensions
{
    public static IConfigurationBuilder AddDefaultConfiguration(this IConfigurationBuilder configuration, HostBuilderContext hostBuilderContext)
    {
        return configuration;
    }

    public static IServiceCollection AddDefaultServices(this IServiceCollection services, HostBuilderContext hostBuilderContext)
    {
        return services;
    }
}
