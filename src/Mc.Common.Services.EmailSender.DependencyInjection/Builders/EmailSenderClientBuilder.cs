using Mc.Common.Services.EmailSender.Abstractions.Clients;
using Mc.Common.Services.EmailSender.Abstractions.Resolvers;
using Microsoft.Extensions.DependencyInjection;

namespace Mc.Common.Services.EmailSender.DependencyInjection.Builders;
public sealed class EmailSenderClientBuilder
{
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

    internal TEmailSenderClient Build<TEmailSenderClient>(IServiceCollection services, object servicesKey)
        where TEmailSenderClient : EmailSenderClient, IEmailSenderClient
    {
        // Validate
        if (_emailSenderClientSettingsResolverType is null) throw new NullReferenceException(nameof(_emailSenderClientSettingsResolverType));

        // Register dependencies
        services.AddKeyedTransient(typeof(IEmailSenderClientSettingsResolver), servicesKey, _emailSenderClientSettingsResolverType);

        // build the MailSenderClient
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        IEmailSenderClientSettingsResolver emailSenderClientSettingsResolver = serviceProvider.GetRequiredKeyedService<IEmailSenderClientSettingsResolver>(servicesKey);
        
        TEmailSenderClient? mailSenderClient = ActivatorUtilities.CreateInstance<TEmailSenderClient>(serviceProvider, emailSenderClientSettingsResolver);
        if (mailSenderClient is null) throw new NullReferenceException(nameof(mailSenderClient));

        return mailSenderClient;
    }
}