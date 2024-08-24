using Mc.Common.Services.EmailSender.Abstractions.Builders;
using Mc.Common.Services.EmailSender.Abstractions.Dtos;
using Mc.Common.Services.EmailSender.Abstractions.Enums;
using Shing.Common.Architecture.Mailing.DevelopmentTests.Fixtures;

namespace Shing.Common.Architecture.Mailing.DevelopmentTests.Tests.Services;
public class MailSenderServiceTests(DependencyInjectionFixture dependencyInjectionFixture) : IClassFixture<DependencyInjectionFixture>
{
    private readonly ICustomEmailSenderClient _customEmailSenderClient = dependencyInjectionFixture.GetRequiredService<ICustomEmailSenderClient>();
    private readonly ICustomEmailSenderClient2 _customEmailSenderClient2 = dependencyInjectionFixture.GetRequiredService<ICustomEmailSenderClient2>();

    [Fact]
    public async Task SendMessageAsync_ShouldSendAnEmail()
    {
        const string messageBody = @"<!DOCTYPE html>
        <html lang=""en"">
        <head>
            <meta charset=""UTF-8"">
            <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
            <title>SMTP Server Test Email</title>
        </head>
        <body>
            <div style=""max-width: 600px; margin: 0 auto; padding: 20px; font-family: Arial, sans-serif; background-color: #f4f4f4;"">
                <h2 style=""color: #333;"">SMTP Server Test Email</h2>
                <p>This is a test email sent to check the functionality of your SMTP server.</p>
                <p>If you can read this, your server is properly configured to send HTML emails.</p>
                <hr>
                <p style=""font-size: 0.8em; color: #666;"">This email was sent for testing purposes.</p>
            </div>
        </body>
        </html>";

        FileStream fileStream = new($@"D:\ToSave\x.jpg", FileMode.OpenOrCreate);
        EmailMessageDto mailMessage = new EmailMessageBuilder()
            .AddSender("outtest0001@outlook.com", "Matee")
            .AddRecipient("chyla95@gmail.com", "R1")
            .AddRecipient("chyla.c1@gmail.com", "R2")
            .SetSubject("Test subjest")
            .AddAttachment(fileStream, "x.jpg")
            .SetBody(messageBody, EmailBodyType.Html)
            .CreateEmailMessage();

        await _customEmailSenderClient.SendMessageAsync(mailMessage);
        await _customEmailSenderClient2.SendMessageAsync(mailMessage);
    }
}