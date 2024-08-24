using Mc.Common.Services.EmailSender.Abstractions.Clients;
using Mc.Common.Services.EmailSender.Abstractions.Services;
using Mc.Common.Services.EmailSender.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Mc.Common.Services.EmailSender.DependencyInjection.Extensions.ServiceCollection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddEmailSenderClient<TEmailSenderClient>(this IServiceCollection services)
        where TEmailSenderClient : EmailSenderClient, IEmailSenderClient
    {
        bool isEmailSenderClientRegistered = services.Any(serviceDescriptor => serviceDescriptor.ServiceType == typeof(TEmailSenderClient));
        if (isEmailSenderClientRegistered) throw new InvalidOperationException($"{nameof(EmailSenderClient)} of type '{typeof(TEmailSenderClient)}' is already registered.");

        services.AddScoped<TEmailSenderClient>();
        services.AddScoped<IEmailSenderService<TEmailSenderClient>, EmailSenderService<TEmailSenderClient>>();

        return services;
    }
}
