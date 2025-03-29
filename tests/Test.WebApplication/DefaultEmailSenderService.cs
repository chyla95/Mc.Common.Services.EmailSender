using Mc.Common.Services.EmailSender.Abstractions.Clients;
using Mc.Common.Services.EmailSender.Abstractions.Services;
using Mc.Common.Services.EmailSender.Services;

namespace Test.WebApplication;

public interface IDefaultEmailSenderService : IEmailSenderService;

public sealed class DefaultEmailSenderService : EmailSenderService, IDefaultEmailSenderService
{
    public DefaultEmailSenderService(IEmailSenderClient emailSenderClient) : base(emailSenderClient)
    { }
}
