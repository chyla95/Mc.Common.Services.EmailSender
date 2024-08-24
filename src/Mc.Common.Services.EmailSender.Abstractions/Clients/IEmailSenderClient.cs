using Mc.Common.Services.EmailSender.Abstractions.Dtos;

namespace Mc.Common.Services.EmailSender.Abstractions.Clients;
public interface IEmailSenderClient
{
    Task SendMessageAsync(EmailMessageDto emailMessage, CancellationToken cancellationToken = default);
}
