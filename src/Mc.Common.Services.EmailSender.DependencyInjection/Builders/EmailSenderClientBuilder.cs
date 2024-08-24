using Mc.Common.Services.EmailSender.Abstractions.Clients;
using Mc.Common.Services.EmailSender.Abstractions.Resolvers;
using Microsoft.Extensions.DependencyInjection;

namespace Mc.Common.Services.EmailSender.DependencyInjection.Builders;
public sealed class EmailSenderClientBuilder
{
    private object? _servicesKey;
    private Type? _emailSenderClientSettingsResolverType;

    private EmailSenderClientBuilder() { }

    internal static EmailSenderClientBuilder Create()
    {
        return new();
    }

    public EmailSenderClientBuilder WithEmailSenderClientSettingsResolver<TEmailSenderClientSettingsResolver>()
        where TEmailSenderClientSettingsResolver : IEmailSenderClientSettingsResolver
    {
        _emailSenderClientSettingsResolverType = typeof(TEmailSenderClientSettingsResolver);
        return this;
    }

    internal void RegisterServices(IServiceCollection services, object servicesKey)
    {
        // Validate dependencies
        if (_emailSenderClientSettingsResolverType is null) throw new NullReferenceException(nameof(_emailSenderClientSettingsResolverType));

        // Register dependencies
        services.AddKeyedTransient(typeof(IEmailSenderClientSettingsResolver), servicesKey, _emailSenderClientSettingsResolverType);

        // Set services key
        _servicesKey = servicesKey;
    }

    internal TEmailSenderClient Build<TEmailSenderClient>(IServiceProvider serviceProvider)
        where TEmailSenderClient : EmailSenderClient, IEmailSenderClient
    {
        // Validate
        if (_servicesKey is null) throw new NullReferenceException(nameof(_servicesKey));

        // build the MailSenderClient
        IEmailSenderClientSettingsResolver emailSenderClientSettingsResolver = serviceProvider.GetRequiredKeyedService<IEmailSenderClientSettingsResolver>(_servicesKey);
        
        TEmailSenderClient? mailSenderClient = ActivatorUtilities.CreateInstance<TEmailSenderClient>(serviceProvider, emailSenderClientSettingsResolver);
        if (mailSenderClient is null) throw new NullReferenceException(nameof(mailSenderClient));

        return mailSenderClient;
    }
}