using Mc.Common.Services.EmailSender.Abstractions.Models;
using MimeKit;

namespace Mc.Common.Services.EmailSender.Extensions.Models;
internal static partial class EmailMessageExtensions
{
    public async static Task<MimeMessage> ToMimeMessageAsync(this EmailMessage emailMessage)
    {
        MimeMessage mimeMessage = new();

        // Add message senders
        IEnumerable<MailboxAddress> sendersAddresses = emailMessage.Senders.Select(s => s.ToMailboxAddress());
        mimeMessage.From.AddRange(sendersAddresses);

        // Add message recipients
        IEnumerable<MailboxAddress> recipientsAddresses = emailMessage.Recipients.Select(s => s.ToMailboxAddress());
        mimeMessage.To.AddRange(recipientsAddresses);

        // Add message Cc
        IEnumerable<MailboxAddress> ccRecipients = emailMessage.CcRecipients.Select(s => s.ToMailboxAddress());
        mimeMessage.Cc.AddRange(ccRecipients);

        // Add message Bcc
        IEnumerable<MailboxAddress> bccRecipients = emailMessage.BccRecipients.Select(s => s.ToMailboxAddress());
        mimeMessage.Bcc.AddRange(bccRecipients);

        // Add message subject
        mimeMessage.Subject = emailMessage.Subject;

        // Add message body
        if (emailMessage.Body is not null) mimeMessage.Body = await emailMessage.Body.ToMimeEntityAsync(emailMessage.Attachments);
        else if (emailMessage.Attachments.Count > 0) mimeMessage.Body = await emailMessage.Attachments.ToMimeEntitiesAsync();

        return mimeMessage;
    }
}
