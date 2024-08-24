namespace Mc.Common.Services.EmailSender.Abstractions.Dtos;
public sealed record EmailMessageDto
{
    public string? Subject { get; set; }
    public EmailBodyDto? Body { get; set; }
    public ICollection<EmailAttachmentDto> Attachments { get; set; } = [];

    public ICollection<EmailAddressDto> Senders { get; init; } = [];
    public ICollection<EmailAddressDto> Recipients { get; set; } = [];
    public ICollection<EmailAddressDto> BccRecipients { get; set; } = [];
    public ICollection<EmailAddressDto> CcRecipients { get; set; } = [];
}