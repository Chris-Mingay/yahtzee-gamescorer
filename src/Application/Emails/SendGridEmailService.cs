
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Application.Interfaces;

namespace Application.Emails
{
    public class SendGridEmailService : IEmailerService
    {

        private readonly ILogger<SendGridEmailService> _logger;
        private SendGridConfiguration _sendGridConfiguration;
        private readonly IApplicationDbContext _context;
        public SendGridEmailService(ILogger<SendGridEmailService> logger, IOptions<SendGridConfiguration> sendGridConfiguration, IApplicationDbContext context)
        {
            _logger = logger;
            _sendGridConfiguration = sendGridConfiguration.Value;
            _context = context;

            if(_sendGridConfiguration is null)
            {
                string error = "SendGridConfiguration section of appSettings.json is required by SendGridEmailService";
                _logger.LogError(error);
                throw new Exception(error);
            }

            if (!_sendGridConfiguration.ProductionMode && (string.IsNullOrEmpty(_sendGridConfiguration.AuthorizedSender) || string.IsNullOrEmpty(_sendGridConfiguration.AuthorizedRecipient)))
            {
                string error = "When not in ProductionMode both the AuthorizedRecipient and AuthorizedSender properties of the SendGridConfiguration section of appSettings.json must be set";
                _logger.LogError(error);
                throw new Exception(error);
            }

        }

        public async Task<EmailerServiceResponse> SendEmailAsync(EmailOptions options, CancellationToken cancellationToken)
        {

            try
            {
                var client = new SendGridClient(_sendGridConfiguration.ApiKey);
                var from = new EmailAddress(_sendGridConfiguration.ProductionMode ? options.FromAddress : _sendGridConfiguration.AuthorizedSender, options.FromName);
                var to = new EmailAddress(_sendGridConfiguration.ProductionMode ? options.RecipientAddress : _sendGridConfiguration.AuthorizedRecipient, options.RecipientName);
                var msg = MailHelper.CreateSingleEmail(from, to, options.Subject, options.PlainTextBody, options.HtmlBody);
                Response response = await client.SendEmailAsync(msg);

                var emailerServiceResponse = new EmailerServiceResponse()
                {
                    Successful = response.StatusCode == System.Net.HttpStatusCode.Accepted,
                    ErrorDetail = response.Body.ToString()
                };

                return emailerServiceResponse;
            }
            catch (Exception ex)
            {
                var emailerServiceResponse = new EmailerServiceResponse()
                {
                    Successful = false,
                    ErrorDetail = ex.Message
                };

                _logger.LogError(ex.Message);

                return emailerServiceResponse;
            }


        }

    }
}

