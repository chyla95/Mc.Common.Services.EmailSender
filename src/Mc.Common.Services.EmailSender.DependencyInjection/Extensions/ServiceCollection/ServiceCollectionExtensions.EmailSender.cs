using Mc.Common.Services.EmailSender.Abstractions.Services;
using Mc.Common.Services.EmailSender.DependencyInjection.ServiceCollectionConfigurators;
using Mc.Common.Services.EmailSender.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Mc.Common.Services.EmailSender.DependencyInjection.Extensions.ServiceCollection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddEmailSender<TAbstraction, TImplementation>(this IServiceCollection serviceCollection, Action<EmailSenderServiceCollectionConfigurator> configureEmailSender)
        where TAbstraction : class, IEmailSenderService
        where TImplementation : EmailSenderService, TAbstraction
    {
        EmailSenderServiceCollectionConfigurator emailSenderServiceCollectionConfigurator = EmailSenderServiceCollectionConfigurator.Create(serviceCollection);
        configureEmailSender(emailSenderServiceCollectionConfigurator);
        emailSenderServiceCollectionConfigurator.ConfigureAs<TAbstraction, TImplementation>();

        return serviceCollection;
    }
}
