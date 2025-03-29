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

    private Type? _emailSenderSettingsResolvingStrategyType;

    private EmailSenderServiceCollectionConfigurator(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    internal static EmailSenderServiceCollectionConfigurator Create(IServiceCollection serviceCollection)
    {
        EmailSenderServiceCollectionConfigurator emailSenderServiceCollectionConfigurator = new(serviceCollection);
        return emailSenderServiceCollectionConfigurator;
    }

    public EmailSenderServiceCollectionConfigurator WithEmailSenderSettingsResolvingStrategy<TEmailSenderClientSettingsResolver>()
        where TEmailSenderClientSettingsResolver : class, IEmailSenderClientSettingsResolvingStrategy
    {
        _emailSenderSettingsResolvingStrategyType = typeof(TEmailSenderClientSettingsResolver);
        return this;
    }

    internal void ConfigureAs<TAbstraction, TImplementation>()
        where TAbstraction : class, IEmailSenderService
        where TImplementation : EmailSenderService, TAbstraction
    {
        if (_emailSenderSettingsResolvingStrategyType is null)
            throw new NullReferenceException($"{nameof(IEmailSenderClientSettingsResolvingStrategy)} is required");

        string serviceKey = typeof(TImplementation).Name;

        _serviceCollection.AddTransientResolvedFromKeyedServices(typeof(IEmailSenderClientSettingsResolvingStrategy), serviceKey, _emailSenderSettingsResolvingStrategyType);
        _serviceCollection.AddKeyedTransientResolvedFromKeyedServices<IEmailSenderClient, EmailSenderClient>(serviceKey);
        _serviceCollection.AddTransientResolvedFromKeyedServices<TAbstraction, TImplementation>(serviceKey);
    }
}
