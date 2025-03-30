using Microsoft.Extensions.DependencyInjection;
using Mc.Common.Services.EmailSender.DependencyInjection.Extensions.ServiceCollection;
using Mc.Common.Services.EmailSender.Tests.Integration.Infrastructure.Strategies;
using Mc.Common.Services.EmailSender.Tests.Integration.Infrastructure.Services;
using Mc.Common.Services.EmailSender.Tests.Integration.Tests.Services;

namespace Mc.Common.Services.EmailSender.Tests.Integration.Infrastructure.Fixtures;
public class ServiceProviderFixture : FixtureBase
{
    public IServiceProvider ServiceProvider { get; }

    public ServiceProviderFixture()
    {
        ServiceCollection serviceCollection = new();

        serviceCollection.AddEmailSender<IPrimaryEmailSenderService, PrimaryEmailSenderService>(configureEmailSender => configureEmailSender
            .WithEmailSenderClientSettingsResolvingStrategy<PrimaryEmailSenderClientSettingsResolvingStrategy>());

        serviceCollection.AddTransient<ITestDataGeneratorService, TestDataGeneratorService>();

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }
}
