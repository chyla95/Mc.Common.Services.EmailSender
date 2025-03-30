using MailKit;
using MailKit.Net.Smtp;
using Mc.Common.Services.EmailSender.Abstractions.Clients;
using Mc.Common.Services.EmailSender.Abstractions.Models;
using Mc.Common.Services.EmailSender.Abstractions.Strategies;
using Mc.Common.Services.EmailSender.Extensions.Models;
using MimeKit;

namespace Mc.Common.Services.EmailSender;
public partial class EmailSenderClient : IEmailSenderClient, IDisposable
{
    private bool _disposedValue;

    protected readonly ISmtpClient _smtpClient;
    protected readonly IEmailSenderClientSettingsResolvingStrategy _emailSenderClientSettingsResolvingStrategy;

    public EmailSenderClient(IEmailSenderClientSettingsResolvingStrategy emailSenderClientSettingsResolvingStrategy)
    {
        _smtpClient = new SmtpClient();
        _emailSenderClientSettingsResolvingStrategy = emailSenderClientSettingsResolvingStrategy;
    }

    public async Task SendMessageAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        using MimeMessage mimeMessage = await emailMessage.ToMimeMessageAsync();

        if (!_shouldExpectCreatedSession) await CreateSessionAsync(cancellationToken);

        try
        {
            _ = await _smtpClient.SendAsync(mimeMessage, cancellationToken);
        }
        catch (Exception exception) when (exception is ServiceNotConnectedException || exception is ServiceNotAuthenticatedException)
        {
            await CreateSessionAsync(cancellationToken);
            _ = await _smtpClient.SendAsync(mimeMessage, cancellationToken);
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // Dispose managed state (managed objects)
                _smtpClient.Dispose();
            }

            // Free unmanaged resources (unmanaged objects) and override finalizer
            // Set large fields to null
            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}