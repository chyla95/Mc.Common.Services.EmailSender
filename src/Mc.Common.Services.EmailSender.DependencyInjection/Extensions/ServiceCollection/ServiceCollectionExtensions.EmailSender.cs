using Mc.Common.Services.EmailSender.Abstractions.Clients;
using Mc.Common.Services.EmailSender.Abstractions.Services;
using Mc.Common.Services.EmailSender.DependencyInjection.Builders;
using Mc.Common.Services.EmailSender.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Mc.Common.Services.EmailSender.DependencyInjection.Extensions.ServiceCollection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddEmailSenderClient<TEmailSenderClient>(this IServiceCollection services, 
        Action<EmailSenderClientBuilder> buildEmailSenderClient)
        where TEmailSenderClient : EmailSenderClient, IEmailSenderClient
    {
        // Validate TEmailSenderClient uniqueness 
        bool isEmailSenderClientRegistered = services.Any(serviceDescriptor => serviceDescriptor.ServiceType == typeof(TEmailSenderClient));
        if (isEmailSenderClientRegistered) throw new InvalidOperationException($"{nameof(EmailSenderClient)} of type '{typeof(TEmailSenderClient)}' is already registered.");

        // Register services
        var servicesKey = typeof(TEmailSenderClient);

        EmailSenderClientBuilder emailSenderClientBuilder = EmailSenderClientBuilder.Create();
        buildEmailSenderClient(emailSenderClientBuilder);
        services.AddTransient(_ => emailSenderClientBuilder.Build<TEmailSenderClient>(services, servicesKey));
        services.AddScoped<IEmailSenderService<TEmailSenderClient>, EmailSenderService<TEmailSenderClient>>();

        return services;
    }
}
