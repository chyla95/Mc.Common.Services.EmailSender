using MailKit;
using MailKit.Net.Smtp;
using Mc.Common.Services.EmailSender.Abstractions.Clients;
using Mc.Common.Services.EmailSender.Abstractions.Dtos;
using Mc.Common.Services.EmailSender.Abstractions.Enums;
using Mc.Common.Services.EmailSender.Abstractions.Resolvers;
using MimeKit;

namespace Mc.Common.Services.EmailSender;
public abstract partial class EmailSenderClient : IEmailSenderClient, IDisposable
{
    protected readonly ISmtpClient _smtpClient;
    protected readonly IEmailSenderClientSettingsResolver _emailSenderClientSettingsResolver;

    public EmailSenderClient(IEmailSenderClientSettingsResolver emailSenderClientSettingsResolver)
    {
        _smtpClient = new SmtpClient();
        _emailSenderClientSettingsResolver = emailSenderClientSettingsResolver;
    }

    public virtual async Task SendMessageAsync(EmailMessageDto emailMessage, CancellationToken cancellationToken = default)
    {
        using MimeMessage mimeMessage = await MapMimeMessage(emailMessage);

        if (!_shouldExpectCreatedSession) await CreateSessionAsync(cancellationToken);

        try
        {
            _ = await _smtpClient.SendAsync(mimeMessage, cancellationToken);
        }
        catch (Exception exception) when (exception is ServiceNotConnectedException || exception is ServiceNotAuthenticatedException)
        {
            await CreateSessionAsync(cancellationToken);
            _ = await _smtpClient.SendAsync(mimeMessage, cancellationToken);
        }
    }

    private async static Task<MimeMessage> MapMimeMessage(EmailMessageDto emailMessage)
    {
        MimeMessage mimeMessage = new();

        // Add message senders
        IEnumerable<MailboxAddress> sendersAddresses = emailMessage.Senders.Select(s => new MailboxAddress(s.Name, s.Address));
        mimeMessage.From.AddRange(sendersAddresses);

        // Add message recipients
        IEnumerable<MailboxAddress> recipientsAddresses = emailMessage.Recipients.Select(s => new MailboxAddress(s.Name, s.Address));
        mimeMessage.To.AddRange(recipientsAddresses);

        // Add message Cc
        IEnumerable<MailboxAddress> ccRecipients = emailMessage.CcRecipients.Select(s => new MailboxAddress(s.Name, s.Address));
        mimeMessage.Cc.AddRange(ccRecipients);

        // Add message Bcc
        IEnumerable<MailboxAddress> bccRecipients = emailMessage.BccRecipients.Select(s => new MailboxAddress(s.Name, s.Address));
        mimeMessage.Bcc.AddRange(bccRecipients);

        // Add message subject
        mimeMessage.Subject = emailMessage.Subject;

        BodyBuilder messageBodyBuilder = new();

        // Add message content
        switch (emailMessage.Body?.Type)
        {
            case EmailBodyType.Text:
                messageBodyBuilder.TextBody = emailMessage.Body.Content;
                break;

            case EmailBodyType.Html:
                messageBodyBuilder.HtmlBody = emailMessage.Body.Content;
                break;

            default:
                throw new InvalidOperationException($"Unsupported {nameof(EmailBodyType)}");
        }

        // Add message attachments
        if (emailMessage.Attachments.Count > 0)
        {
            foreach (EmailAttachmentDto attachment in emailMessage.Attachments)
            {
                await messageBodyBuilder.Attachments.AddAsync(attachment.Name, attachment.FileStream);
            }
        }

        mimeMessage.Body = messageBodyBuilder.ToMessageBody();
        return mimeMessage;
    }

    public void Dispose()
    {
        _smtpClient.Dispose();
    }
}