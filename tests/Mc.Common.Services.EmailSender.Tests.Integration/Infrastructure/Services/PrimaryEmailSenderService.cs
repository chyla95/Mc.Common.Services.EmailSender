using Mc.Common.Services.EmailSender.Abstractions.Clients;
using Mc.Common.Services.EmailSender.Services;

namespace Mc.Common.Services.EmailSender.Tests.Integration.Infrastructure.Services;
internal class PrimaryEmailSenderService : EmailSenderService, IPrimaryEmailSenderService
{
    public PrimaryEmailSenderService(IEmailSenderClient emailSenderClient) : base(emailSenderClient)
    { }
}
