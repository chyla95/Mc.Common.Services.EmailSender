using Mc.Common.Services.EmailSender.Abstractions.Models;
using MimeKit;

namespace Mc.Common.Services.EmailSender.Extensions.Models;
internal static partial class EmailAddressExtensions
{
    public static MailboxAddress ToMailboxAddress(this EmailAddress emailAddress)
    {
        MailboxAddress mailboxAddress = new(emailAddress.Name, emailAddress.Address);
        return mailboxAddress;
    }
}
