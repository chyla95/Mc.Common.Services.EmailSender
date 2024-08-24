namespace Mc.Common.Services.EmailSender.Abstractions.Dtos;
public sealed record EmailAttachmentDto(
    Stream FileStream,
    string Name
);