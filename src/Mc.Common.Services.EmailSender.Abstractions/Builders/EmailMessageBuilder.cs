using Mc.Common.Services.EmailSender.Abstractions.Enums;
using Mc.Common.Services.EmailSender.Abstractions.Models;

namespace Mc.Common.Services.EmailSender.Abstractions.Builders;
public sealed class EmailMessageBuilder
{
    private readonly EmailMessage _emailMessage = new();

    private EmailMessageBuilder() { }

    public static EmailMessageBuilder Create()
    {
        return new();
    }

    public EmailMessageBuilder SetSubject(string subject)
    {
        if (!string.IsNullOrWhiteSpace(_emailMessage.Subject)) throw new InvalidOperationException($"Field: '{nameof(_emailMessage.Subject)}' cannot be set twice");

        _emailMessage.Subject = subject;
        return this;
    }

    public EmailMessageBuilder SetBody(string value, EmailBodyType emailContentType = EmailBodyType.Text)
    {
        if (_emailMessage.Body is not null) throw new InvalidOperationException($"Field: '{nameof(_emailMessage.Body)}' cannot be set twice");

        _emailMessage.Body = new EmailBody(value, emailContentType);
        return this;
    }

    public EmailMessageBuilder AddSender(string address, string? name = null)
    {
        _emailMessage.Senders.Add(new EmailAddress(name ?? address, address));
        return this;
    }

    public EmailMessageBuilder AddRecipient(string address, string? name = null)
    {
        _emailMessage.Recipients.Add(new EmailAddress(name ?? address, address));
        return this;
    }

    public EmailMessageBuilder AddCcRecipient(string address, string? name = null)
    {
        _emailMessage.CcRecipients.Add(new EmailAddress(name ?? address, address));
        return this;
    }

    public EmailMessageBuilder AddBccRecipient(string address, string? name = null)
    {
        _emailMessage.BccRecipients.Add(new EmailAddress(name ?? address, address));
        return this;
    }

    public EmailMessageBuilder AddAttachment(Stream fileStream, string name)
    {
        _emailMessage.Attachments.Add(new EmailAttachment(fileStream, name));
        return this;
    }

    public EmailMessage Build()
    {
        if (_emailMessage.Senders.Count < 1) throw new InvalidOperationException("There should be at least one message sender defined");
        if (_emailMessage.Recipients.Count < 1) throw new InvalidOperationException("There should be at least one message recipent defined");

        return _emailMessage;
    }
}
