using Application.Emails;

namespace Application.Interfaces;

public interface IEmailerService
{
    Task<EmailerServiceResponse> SendEmailAsync(EmailOptions options, CancellationToken cancellationToken);
}