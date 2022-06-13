using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Application.Interfaces;

namespace Application.Emails
{
    public class SmtpEmailService : IEmailerService
    {

        private readonly ILogger<SmtpEmailService> _logger;
        private SmtpConfiguration _smtpConfiguration;
        public SmtpEmailService(ILogger<SmtpEmailService> logger, IOptions<SmtpConfiguration> smtpConfiguration)
        {
            _logger = logger;
            _smtpConfiguration = smtpConfiguration.Value;

            if (_smtpConfiguration is null)
            {
                string error = "SmtpConfiguration section of appSettings.json required by SmtpEmailService";
                _logger.LogError(error);
                throw new Exception(error);
            }

            if(_smtpConfiguration.AuthenticationRequired && (string.IsNullOrEmpty(_smtpConfiguration.Login) || string.IsNullOrEmpty(_smtpConfiguration.Password)))
            {
                string error = "Login and Password properties are required in SmtpConfiguration when AuthenticationRequired is true";
                _logger.LogError(error);
                throw new Exception(error);
            }


        }

        public async Task<EmailerServiceResponse> SendEmailAsync(EmailOptions options, CancellationToken cancellationToken)
        {

            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress(options.FromName, options.FromAddress));
            mailMessage.To.Add(new MailboxAddress(options.RecipientName, options.RecipientAddress));
            mailMessage.Subject = options.Subject;

            var bodyBuilder = new BodyBuilder();
            if (options.HtmlBody is not null) bodyBuilder.HtmlBody = options.HtmlBody;
            if (options.PlainTextBody is not null) bodyBuilder.TextBody = options.PlainTextBody;
            mailMessage.Body = bodyBuilder.ToMessageBody();

            // Sending the email
            using (var smtpClient = new SmtpClient())
            {
                try
                {
                    await smtpClient.ConnectAsync(_smtpConfiguration.Url, _smtpConfiguration.Port, SecureSocketOptions.Auto, cancellationToken);
                    if (_smtpConfiguration.AuthenticationRequired)
                    {
                        await smtpClient.AuthenticateAsync(_smtpConfiguration.Login, _smtpConfiguration.Password, cancellationToken);
                    }
                    await smtpClient.SendAsync(mailMessage, cancellationToken);
                    return new EmailerServiceResponse()
                    {
                        Successful = true
                    };

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);

                    return new EmailerServiceResponse()
                    {
                        Successful = false,
                        ErrorDetail = ex.Message
                    };
                }

            }

        }

    }
}

