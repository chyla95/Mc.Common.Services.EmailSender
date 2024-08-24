using Mc.Common.Services.EmailSender.Abstractions.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace Mc.Common.Services.EmailSender.DependencyInjection.Extensions.ServiceCollection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddEmailSenderClient<TEmailSenderClientInterface, TEmailSenderClientClass>(this IServiceCollection services)
        where TEmailSenderClientInterface : class, IEmailSenderClient
        where TEmailSenderClientClass : class, TEmailSenderClientInterface
    {
        services.AddScoped<TEmailSenderClientInterface, TEmailSenderClientClass>();
        return services;
    }
}
