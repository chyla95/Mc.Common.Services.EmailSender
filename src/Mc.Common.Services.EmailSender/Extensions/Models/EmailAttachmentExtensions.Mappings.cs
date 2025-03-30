using Mc.Common.Services.EmailSender.Abstractions.Models;
using MimeKit;

namespace Mc.Common.Services.EmailSender.Extensions.Models;
internal static partial class EmailAttachmentExtensions
{
    public async static Task<MimeEntity> ToMimeEntityAsync(this EmailAttachment emailAttachment)
    {
        BodyBuilder messageBodyBuilder = new();
        await messageBodyBuilder.Attachments.AddAsync(emailAttachment.Name, emailAttachment.FileStream);

        MimeEntity mimeEntity = messageBodyBuilder.ToMessageBody();
        return mimeEntity;
    }

    public async static Task<MimeEntity> ToMimeEntitiesAsync(this IEnumerable<EmailAttachment> emailAttachments)
    {
        BodyBuilder messageBodyBuilder = new();

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
