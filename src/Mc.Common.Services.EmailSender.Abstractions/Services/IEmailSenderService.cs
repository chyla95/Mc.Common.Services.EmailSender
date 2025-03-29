using Mc.Common.Services.EmailSender.Abstractions.Builders;
using Mc.Common.Services.EmailSender.Abstractions.Models;

namespace Mc.Common.Services.EmailSender.Abstractions.Services;
public interface IEmailSenderService
{
    Task SendMessageAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default);
    Task SendMessageAsync(Action<EmailMessageBuilder> buildEmailMessage, CancellationToken cancellationToken = default);
}
