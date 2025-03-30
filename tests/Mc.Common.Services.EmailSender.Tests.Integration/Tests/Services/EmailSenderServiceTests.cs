using Mc.Common.Services.EmailSender.Abstractions.Builders;
using Mc.Common.Services.EmailSender.Abstractions.Enums;
using Mc.Common.Services.EmailSender.Abstractions.Models;
using Mc.Common.Services.EmailSender.Tests.Integration.Infrastructure.Fixtures;
using Mc.Common.Services.EmailSender.Tests.Integration.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Mc.Common.Services.EmailSender.Tests.Integration.Tests.Services;
public class EmailSenderServiceTests : IClassFixture<ServiceProviderFixture>
{
    private readonly IPrimaryEmailSenderService _primaryEmailSenderService;
    private readonly ITestDataGeneratorService _testDataGeneratorService;

    public EmailSenderServiceTests(ServiceProviderFixture serviceProviderFixture)
    {
        _primaryEmailSenderService = serviceProviderFixture.ServiceProvider.GetRequiredService<IPrimaryEmailSenderService>();
        _testDataGeneratorService = serviceProviderFixture.ServiceProvider.GetRequiredService<ITestDataGeneratorService>();
    }

    [Fact]
    public async Task __()
    {
        EmailMessage message = EmailMessageBuilder
            .Create()
            .AddSender(_testDataGeneratorService.GenerateEmail(), _testDataGeneratorService.GenerateUsername())
            .AddSender(_testDataGeneratorService.GenerateEmail(), _testDataGeneratorService.GenerateUsername())
            .AddRecipient(_testDataGeneratorService.GenerateEmail(), _testDataGeneratorService.GenerateUsername())
            .AddRecipient(_testDataGeneratorService.GenerateEmail(), _testDataGeneratorService.GenerateUsername())
            .SetSubject(_testDataGeneratorService.GenerateText())
            .SetBody(HtmlMessageBody, EmailBodyType.Html).Build();

        await _primaryEmailSenderService.SendMessageAsync(message);

        //
        HttpService httpService = new HttpService();
        ICollection<Email> emails = [.. await httpService.GetEmails()];

        foreach (EmailAddress recipient in message.Recipients)
        {
            Assert.Contains(emails, x => x.DeliveredTo == recipient.Address);
        }

        Assert.Equal(2, emails.Count(e => e.Subject == message.Subject));
    }

    private const string HtmlMessageBody = @"<!DOCTYPE html>
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
}

internal sealed class HttpService
{
    public async Task<IEnumerable<Email>> GetEmails()
    {
        using HttpClient client = new();

        var url = "https://fakemail.stream/api/mail/list";
        var requestData = new
        {
            userId = "cc984e11-68e8-b0d2-1c96-759b3315db0a",
            page = 1,
            pageSize = 100
        };

        var jsonContent = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync(url, jsonContent);
        ApiResponse? responseBody = await response.Content.ReadFromJsonAsync<ApiResponse>();
        return responseBody?.Emails ?? [];
    }
}

public record class ApiResponse(
    bool Success,
    string Username,
    int Page,
    int PageSize,
    int MaxPage,
    List<SmtpUser> SmtpUsers,
    List<Email> Emails
);

public record class SmtpUser(
    string SmtpUsername,
    string SmtpPassword,
    int CurrentEmailCount
);

public record class Email(
    string EmailId,
    int SequenceNumber,
    string SmtpUsername,
    DateTime TimestampUtc,
    string From,
    string Subject,
    string DeliveredTo,
    string BodySummary,
    List<Attachment> Attachments
);

public record class Attachment(); // Empty for now, but expandable


internal interface ITestDataGeneratorService
{
    string GenerateUsername();
    string GenerateText();
    string GenerateEmail();
}
internal class TestDataGeneratorService : ITestDataGeneratorService
{
    public string GenerateUsername() => Guid.NewGuid().ToString();
    public string GenerateText() => Guid.NewGuid().ToString();
    public string GenerateEmail() => Guid.NewGuid().ToString() + "@fakemail.com";

}