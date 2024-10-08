﻿using Mc.Common.Services.EmailSender.Abstractions.Builders;
using Mc.Common.Services.EmailSender.Abstractions.Clients;
using Mc.Common.Services.EmailSender.Abstractions.Dtos;
using Mc.Common.Services.EmailSender.Abstractions.Services;

namespace Mc.Common.Services.EmailSender.Services;
public sealed class EmailSenderService<TEmailSenderClient> : IEmailSenderService<TEmailSenderClient>
    where TEmailSenderClient : class, IEmailSenderClient
{
    private readonly TEmailSenderClient _emailSenderClient;

    public EmailSenderService(TEmailSenderClient emailSenderClient)
    {
        _emailSenderClient = emailSenderClient;
    }

    public async Task SendMessageAsync(EmailMessageDto emailMessage, CancellationToken cancellationToken = default)
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
