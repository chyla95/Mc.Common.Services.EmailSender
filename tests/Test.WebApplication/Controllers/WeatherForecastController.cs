using Mc.Common.Services.EmailSender.Abstractions.Builders;
using Mc.Common.Services.EmailSender.Abstractions.Enums;
using Mc.Common.Services.EmailSender.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;

namespace Test.WebApplication.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IDefaultEmailSenderService _defaultEmailSenderService;
    //private readonly IEmailSenderService<CustomEmailSenderClient2> _emailSenderService2;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,
        IDefaultEmailSenderService defaultEmailSenderService)
    {
        _logger = logger;
        _defaultEmailSenderService = defaultEmailSenderService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<ActionResult> Get()
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

        using FileStream fileStream = new($@"D:\ToSave\x.jpg", FileMode.Open);
        EmailMessage mailMessage = EmailMessageBuilder.Create()
            .AddSender("outtest0001@outlook.com", "Matee")
            .AddRecipient("chyla95@gmail.com", "R1")
            .AddRecipient("chyla.c1@gmail.com", "R2")
            .AddRecipient("chyla.c2@gmail.com", "R3")
            .SetSubject("Test subjest")
            .AddAttachment(fileStream, "x.jpg")
            .SetBody(messageBody, EmailBodyType.Html)
            .Build();

        await _defaultEmailSenderService.SendMessageAsync(x => x
            .AddSender("outtest0001@outlook.com", "Matee")
            .AddRecipient("chyla95@gmail.com", "R1")
            .AddRecipient("chyla.c1@gmail.com", "R2")
            .SetSubject("Test subjest")
            .AddAttachment(fileStream, "x.jpg")
            .SetBody(messageBody, EmailBodyType.Html));

        await _defaultEmailSenderService.SendMessageAsync(x => x
            .AddSender("outtest0001@outlook.com", "Matee")
            .AddRecipient("chyla95@gmail.com", "R1")
            .AddRecipient("chyla.c1@gmail.com", "R2")
            .SetSubject("Test subjest")
            .AddAttachment(fileStream, "x.jpg")
            .SetBody(messageBody, EmailBodyType.Html).Build());

        return Ok();
    }
}
