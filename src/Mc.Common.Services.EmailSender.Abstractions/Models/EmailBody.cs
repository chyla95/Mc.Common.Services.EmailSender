using Mc.Common.Services.EmailSender.Abstractions.Enums;

namespace Mc.Common.Services.EmailSender.Abstractions.Models;
public sealed record EmailBody(
    string Content,
    EmailBodyType Type
);
