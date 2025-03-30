using Mc.Common.Services.EmailSender.Abstractions.Enums;
using Mc.Common.Services.EmailSender.Abstractions.Models;
using MimeKit;

namespace Mc.Common.Services.EmailSender.Extensions.Models;
internal static partial class EmailBodyExtensions
{
    public async static Task<MimeEntity> ToMimeEntityAsync(this EmailBody emailBody, IEnumerable<EmailAttachment> emailAttachments)
    {
        BodyBuilder messageBodyBuilder = new();

        // Add message content
        switch (emailBody.Type)
        {
            case EmailBodyType.Text:
                messageBodyBuilder.TextBody = emailBody.Content;
                break;

            case EmailBodyType.Html:
                messageBodyBuilder.HtmlBody = emailBody.Content;
                break;

            default:
                throw new InvalidOperationException($"Unsupported {nameof(EmailBodyType)}");
        }

        // Add message attachments
        ICollection<EmailAttachment> materializedEmailAttachments = [.. emailAttachments];

        if (materializedEmailAttachments.Count > 0)
        {
            foreach (EmailAttachment attachment in materializedEmailAttachments)
            {
                await messageBodyBuilder.Attachments.AddAsync(attachment.Name, attachment.FileStream);
            }
        }

        MimeEntity mimeEntity = messageBodyBuilder.ToMessageBody();
        return mimeEntity;
    }
}
