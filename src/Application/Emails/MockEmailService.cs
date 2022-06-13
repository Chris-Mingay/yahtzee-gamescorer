using Microsoft.Extensions.Logging;
using Application.Interfaces;

namespace Application.Emails
{
    public class MockEmailerService : IEmailerService
    {

        private readonly ILogger<MockEmailerService> _logger;
        public MockEmailerService(ILogger<MockEmailerService> logger)
        {
            _logger = logger;
            
        }

        public async Task<EmailerServiceResponse> SendEmailAsync(EmailOptions options, CancellationToken cancellationToken)
        {

            _logger.LogInformation(
$@"Email sent via MockTextMessageService
Recipient: {options.RecipientName} <{options.RecipientAddress}>
Sender: {options.FromName} <{options.FromAddress}>
Subject: {options.Subject}
PlainTextBody: 
{(string.IsNullOrEmpty(options.PlainTextBody) ? "UNSET": options.PlainTextBody)}
HtmlBody: 
{(string.IsNullOrEmpty(options.HtmlBody) ? "UNSET" : options.HtmlBody)}");
            await Task.Delay(0);

            return new EmailerServiceResponse()
            {
                Successful = true
            };

        }

    }
}

