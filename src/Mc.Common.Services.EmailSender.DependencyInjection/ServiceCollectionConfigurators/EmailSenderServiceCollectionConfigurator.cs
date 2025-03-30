using Mc.Common.DependencyInjection.Extensions.ServiceCollectionExtensions;
using Mc.Common.Services.EmailSender.Abstractions.Clients;
using Mc.Common.Services.EmailSender.Abstractions.Services;
using Mc.Common.Services.EmailSender.Abstractions.Strategies;
using Mc.Common.Services.EmailSender.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Mc.Common.Services.EmailSender.DependencyInjection.ServiceCollectionConfigurators;
public sealed class EmailSenderServiceCollectionConfigurator
{
    private readonly IServiceCollection _serviceCollection;

    private Type? _emailSenderClientSettingsResolvingStrategy;

    private EmailSenderServiceCollectionConfigurator(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    internal static EmailSenderServiceCollectionConfigurator Create(IServiceCollection serviceCollection)
    {
        EmailSenderServiceCollectionConfigurator emailSenderServiceCollectionConfigurator = new(serviceCollection);
        return emailSenderServiceCollectionConfigurator;
    }

    public EmailSenderServiceCollectionConfigurator WithEmailSenderClientSettingsResolvingStrategy<TEmailSenderClientSettingsResolvingStrategy>()
        where TEmailSenderClientSettingsResolvingStrategy : class, IEmailSenderClientSettingsResolvingStrategy
    {
        _emailSenderClientSettingsResolvingStrategy = typeof(TEmailSenderClientSettingsResolvingStrategy);
        return this;
    }

    internal void ConfigureAs<TAbstraction, TImplementation>()
        where TAbstraction : class, IEmailSenderService
        where TImplementation : EmailSenderService, TAbstraction
    {
        if (_emailSenderClientSettingsResolvingStrategy is null)
            throw new NullReferenceException($"{nameof(IEmailSenderClientSettingsResolvingStrategy)} is required");

        string serviceKey = typeof(TImplementation).Name;

        _serviceCollection.AddTransientResolvedFromKeyedServices(typeof(IEmailSenderClientSettingsResolvingStrategy), serviceKey, _emailSenderClientSettingsResolvingStrategy);
        _serviceCollection.AddKeyedTransientResolvedFromKeyedServices<IEmailSenderClient, EmailSenderClient>(serviceKey);
        _serviceCollection.AddTransientResolvedFromKeyedServices<TAbstraction, TImplementation>(serviceKey);
    }
}
