using Mc.Common.Services.EmailSender.Abstractions.Enums;

namespace Mc.Common.Services.EmailSender.Abstractions.Settings;
public sealed record EmailSenderClientSettings
{
    public required string Address { get; init; }
    public required int PortNumber { get; init; }
    public string? Username { get; init; }
    public string? Password { get; init; }
    public required EmailEncryptionType EncryptionType { get; init; }
}
