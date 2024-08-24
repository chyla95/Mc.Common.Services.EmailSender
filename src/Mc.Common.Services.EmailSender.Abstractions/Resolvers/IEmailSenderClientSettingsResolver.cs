using Mc.Common.Services.EmailSender.Abstractions.Settings;

namespace Mc.Common.Services.EmailSender.Abstractions.Resolvers;
public interface IEmailSenderClientSettingsResolver
{
    Task<EmailSenderClientSettings> ResolveAsync(CancellationToken cancellationToken = default);
}
