using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Shing.Common.Architecture.Mailing.DevelopmentTests.DependencyInjection;

internal sealed class DependencyInjectionBuilder
{
    private readonly IHostBuilder _hostBuilder = Host.CreateDefaultBuilder();

    public IConfigurationBuilder Configuration { get; } = new ConfigurationBuilder();
    public IServiceCollection Services { get; } = new ServiceCollection();

    public IHost Build()
    {
        DependencyInjectionBuilderOptions defaultDependencyInjectionBuilderOptions = new();

        IHost host = Build(defaultDependencyInjectionBuilderOptions);
        return host;
    }

    public IHost Build(DependencyInjectionBuilderOptions options)
    {
        _hostBuilder.ConfigureAppConfiguration((hostBuilderContext, configuration) =>
        {
            configuration.AddDefaultConfiguration(hostBuilderContext);
            foreach (var configurationSource in Configuration.Sources) configuration.Add(configurationSource);
        });

        _hostBuilder.ConfigureServices((hostBuilderContext, services) =>
        {
            services.AddDefaultServices(hostBuilderContext);
            foreach (var service in Services) services.Add(service);
        });

        string? environmentName = options.EnvironmentName ?? Environment.GetEnvironmentVariable(DependencyInjectionConstants.Environment.EnvironmentName);
        if (environmentName is not null) _hostBuilder.UseEnvironment(environmentName);

        IHost host = _hostBuilder.Build();
        return host;
    }
}
