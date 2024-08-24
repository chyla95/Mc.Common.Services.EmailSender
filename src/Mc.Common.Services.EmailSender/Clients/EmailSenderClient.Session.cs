using MailKit.Security;
using Mc.Common.Services.EmailSender.Abstractions.Enums;
using Mc.Common.Services.EmailSender.Abstractions.Settings;
using System.Net;

namespace Mc.Common.Services.EmailSender;
public abstract partial class EmailSenderClient
{
    protected bool _shouldExpectCreatedSession = false;

    protected internal async Task CreateSessionAsync(CancellationToken cancellationToken = default)
    {
        EmailSenderClientSettings emailSenderClientSettings = await _emailSenderClientSettingsResolver.ResolveAsync(cancellationToken);

        SecureSocketOptions secureSocketOptions = ConfigureSecureSocketOptions(emailSenderClientSettings.EncryptionType);
        await _smtpClient.ConnectAsync(
            emailSenderClientSettings.Address,
            emailSenderClientSettings.PortNumber,
            secureSocketOptions,
            cancellationToken
        );

        ICredentials? credentials = ConfigureCredentials(
            emailSenderClientSettings.Username,
            emailSenderClientSettings.Password
        );
        if (credentials is not null) await _smtpClient.AuthenticateAsync(credentials, cancellationToken);

        _shouldExpectCreatedSession = true;
    }

    protected internal async Task ClearSessionAsync(CancellationToken cancellationToken = default)
    {
        await _smtpClient.DisconnectAsync(true, cancellationToken);

        _shouldExpectCreatedSession = false;
    }

    private static SecureSocketOptions ConfigureSecureSocketOptions(EmailEncryptionType emailEncryptionType)
    {
        SecureSocketOptions secureSocketOptions = emailEncryptionType switch
        {
            EmailEncryptionType.None => SecureSocketOptions.None,
            EmailEncryptionType.OptionalTls => SecureSocketOptions.StartTls,
            EmailEncryptionType.MandatoryTls => SecureSocketOptions.SslOnConnect,
            _ => throw new InvalidOperationException($"Unsupported {nameof(EmailEncryptionType)}")
        };

        return secureSocketOptions;
    }

    private static ICredentials? ConfigureCredentials(string? username, string? password)
    {
        if (string.IsNullOrWhiteSpace(password) && string.IsNullOrWhiteSpace(username)) return null;

        ICredentials credentials = new NetworkCredential
        {
            UserName = username,
            Password = password
        };

        return credentials;
    }
}
