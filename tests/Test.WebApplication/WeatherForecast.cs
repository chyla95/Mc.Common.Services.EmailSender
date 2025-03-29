using Mc.Common.Services.EmailSender.Abstractions.Enums;
using Mc.Common.Services.EmailSender.Abstractions.Settings;
using Mc.Common.Services.EmailSender.Abstractions.Strategies;

namespace Test.WebApplication;
public sealed class DefaultEmailSenderClientSettingsResolver : IEmailSenderClientSettingsResolvingStrategy
{
    public Task<EmailSenderClientSettings> ResolveEmailSenderClientSettingsAsync(CancellationToken cancellationToken = default)
    {
        //EmailSenderClientSettings mailSenderClientSettings = new()
        //{
        //    Address = "sandbox.smtp.mailtrap.io",
        //    PortNumber = 2525,
        //    Username = "c0723041b7926d",
        //    Password = "28e28f4fb92cd2",
        //    EncryptionType = EmailEncryptionType.OptionalTls
        //};

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

public sealed class DefaultEmailSenderClientSettingsResolver2 : IEmailSenderClientSettingsResolvingStrategy
{
    public Task<EmailSenderClientSettings> ResolveEmailSenderClientSettingsAsync(CancellationToken cancellationToken = default)
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