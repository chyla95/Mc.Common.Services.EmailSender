namespace Mc.Common.Services.EmailSender.Abstractions.Models;
public sealed record EmailMessage
{
    public string? Subject { get; set; }
    public EmailBody? Body { get; set; }
    public ICollection<EmailAttachment> Attachments { get; set; } = [];

    public ICollection<EmailAddress> Senders { get; init; } = [];
    public ICollection<EmailAddress> Recipients { get; set; } = [];
    public ICollection<EmailAddress> BccRecipients { get; set; } = [];
    public ICollection<EmailAddress> CcRecipients { get; set; } = [];
}