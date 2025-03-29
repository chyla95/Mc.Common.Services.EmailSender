using Mc.Common.Services.EmailSender.Abstractions.Settings;

namespace Mc.Common.Services.EmailSender.Abstractions.Strategies;
public interface IEmailSenderClientSettingsResolvingStrategy
{
    Task<EmailSenderClientSettings> ResolveEmailSenderClientSettingsAsync(CancellationToken cancellationToken = default);
}
