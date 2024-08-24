using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shing.Common.Architecture.Mailing.DevelopmentTests.DependencyInjection;
using Mc.Common.Services.EmailSender.DependencyInjection.Extensions.ServiceCollection;
using Mc.Common.Services.EmailSender;
using Mc.Common.Services.EmailSender.Abstractions.Resolvers;
using Mc.Common.Services.EmailSender.Abstractions.Settings;
using Mc.Common.Services.EmailSender.Abstractions.Enums;
using Mc.Common.Services.EmailSender.Abstractions.Clients;

namespace Shing.Common.Architecture.Mailing.DevelopmentTests.Fixtures;
public class DependencyInjectionFixture : IDisposable
{
    private readonly IServiceScope _serviceScope;

    public DependencyInjectionFixture()
    {
        IHost host = CreateHost();
        _serviceScope = host.Services.CreateScope();
    }

    protected virtual IHost CreateHost()
    {
        DependencyInjectionBuilder builder = new();

        builder.Services.AddEmailSenderClient<ICustomEmailSenderClient, CustomEmailSenderClient>();
        builder.Services.AddEmailSenderClient<ICustomEmailSenderClient2, CustomEmailSenderClient2>();

        IHost host = builder.Build();
        return host;
    }

    public T GetRequiredService<T>() where T : notnull
        => _serviceScope.ServiceProvider.GetRequiredService<T>();

    public T? GetService<T>()
        => _serviceScope.ServiceProvider.GetService<T>();

    public void Dispose()
    {
        _serviceScope.Dispose();
        GC.SuppressFinalize(this);
    }
}

public interface ICustomEmailSenderClient : IEmailSenderClient;

public sealed class CustomEmailSenderClient : EmailSenderClient, ICustomEmailSenderClient
{
    public override IEmailSenderClientSettingsResolver EmailSenderClientSettingsResolver => new DefaultEmailSenderClientSettingsResolver();

    private sealed class DefaultEmailSenderClientSettingsResolver : IEmailSenderClientSettingsResolver
    {
        public Task<EmailSenderClientSettings> ResolveAsync(CancellationToken cancellationToken = default)
        {
            EmailSenderClientSettings mailSenderClientSettings = new()
            {
                Address = "sandbox.smtp.mailtrap.io",
                PortNumber = 2525,
                Username = "c0723041b7926d",
                Password = "28e28f4fb92cd2",
                EncryptionType = EmailEncryptionType.OptionalTls
            };

            return Task.FromResult(mailSenderClientSettings);
        }
    }
}

public interface ICustomEmailSenderClient2 : IEmailSenderClient;

public sealed class CustomEmailSenderClient2 : EmailSenderClient, ICustomEmailSenderClient2
{
    public override IEmailSenderClientSettingsResolver EmailSenderClientSettingsResolver => new DefaultEmailSenderClientSettingsResolver2();

    private sealed class DefaultEmailSenderClientSettingsResolver2 : IEmailSenderClientSettingsResolver
    {
        public Task<EmailSenderClientSettings> ResolveAsync(CancellationToken cancellationToken = default)
        {
            EmailSenderClientSettings mailSenderClientSettings = new()
            {
                Address = "fakemail.stream",
                PortNumber = 587,
                Username = "2orvxe",
                Password = "gTE0wXAfpUd",
                EncryptionType = EmailEncryptionType.OptionalTls
            };

            return Task.FromResult(mailSenderClientSettings);
        }
    }
}