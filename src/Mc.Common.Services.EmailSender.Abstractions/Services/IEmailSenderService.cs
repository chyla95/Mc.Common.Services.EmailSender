using Mc.Common.Services.EmailSender.Abstractions.Builders;
using Mc.Common.Services.EmailSender.Abstractions.Clients;
using Mc.Common.Services.EmailSender.Abstractions.Dtos;

namespace Mc.Common.Services.EmailSender.Abstractions.Services;
public interface IEmailSenderService<TEmailSenderClient>
    where TEmailSenderClient : class, IEmailSenderClient
{
    Task SendMessageAsync(EmailMessageDto emailMessage, CancellationToken cancellationToken = default);
    Task SendMessageAsync(Action<EmailMessageBuilder> buildEmailMessage, CancellationToken cancellationToken = default);
}
