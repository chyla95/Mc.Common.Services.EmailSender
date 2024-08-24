using Mc.Common.Services.EmailSender.Abstractions.Enums;

namespace Mc.Common.Services.EmailSender.Abstractions.Dtos;
public sealed record EmailBodyDto(
    string Content,
    EmailBodyType Type
);
