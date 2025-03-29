namespace Mc.Common.Services.EmailSender.Abstractions.Models;
public sealed record EmailAttachment(
    Stream FileStream,
    string Name
);