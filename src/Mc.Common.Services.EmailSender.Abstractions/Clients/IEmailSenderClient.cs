using Mc.Common.Services.EmailSender.Abstractions.Models;

namespace Mc.Common.Services.EmailSender.Abstractions.Clients;
public interface IEmailSenderClient
{
    Task SendMessageAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default);
}
