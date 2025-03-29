using Mc.Common.Services.EmailSender.Abstractions.Builders;
using Mc.Common.Services.EmailSender.Abstractions.Clients;
using Mc.Common.Services.EmailSender.Abstractions.Models;
using Mc.Common.Services.EmailSender.Abstractions.Services;

namespace Mc.Common.Services.EmailSender.Services;
public abstract class EmailSenderService : IEmailSenderService
{
    private readonly IEmailSenderClient _emailSenderClient;

    public EmailSenderService(IEmailSenderClient emailSenderClient)
    {
        _emailSenderClient = emailSenderClient;
    }

    public async Task SendMessageAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        await _emailSenderClient.SendMessageAsync(emailMessage, cancellationToken);
    }

    public async Task SendMessageAsync(Action<EmailMessageBuilder> buildEmailMessage, CancellationToken cancellationToken = default)
    {
        EmailMessageBuilder emailMessageBuilder = EmailMessageBuilder.Create();
        buildEmailMessage(emailMessageBuilder);

        await _emailSenderClient.SendMessageAsync(emailMessageBuilder.Build(), cancellationToken);
    }
}
