using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Business_Logic.Services
{
    public class EmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IConfiguration _configuration;
        private readonly SendGridClient _client;
        private readonly EmailAddress _sender;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _logger = logger;
            _configuration = configuration;

            _client = new SendGridClient(_configuration["SendGrid:ApiKey"]);
            _sender = new EmailAddress(_configuration["SendGrid:Sender:Email"], _configuration["SendGrid:Sender:Name"]);
        }

        public async Task<Response> SendMailAsync(string subject, EmailAddress to, string htmlContent)
        {
            var msg = MailHelper.CreateSingleEmail(_sender, to, subject, "", htmlContent);
            var response = await _client.SendEmailAsync(msg);

            _logger.LogInformation($"Email was sended, on email address: {to.Email}");

            return response;
        }
    }
}
