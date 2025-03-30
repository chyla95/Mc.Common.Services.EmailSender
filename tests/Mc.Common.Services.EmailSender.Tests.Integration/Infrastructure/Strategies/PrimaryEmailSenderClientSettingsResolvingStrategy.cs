using Mc.Common.Services.EmailSender.Abstractions.Enums;
using Mc.Common.Services.EmailSender.Abstractions.Settings;
using Mc.Common.Services.EmailSender.Abstractions.Strategies;

namespace Mc.Common.Services.EmailSender.Tests.Integration.Infrastructure.Strategies;
internal class PrimaryEmailSenderClientSettingsResolvingStrategy : IEmailSenderClientSettingsResolvingStrategy
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
