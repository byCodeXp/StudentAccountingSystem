using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Business_Logic.Services
{
    public class EmailService
    {
        private readonly ILogger<EmailService> logger;
        private readonly SendGridClient client;
        private readonly EmailAddress sender;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            this.logger = logger;

            client = new SendGridClient(configuration["SendGrid:ApiKey"]);
            sender = new EmailAddress(configuration["SendGrid:Sender:Email"], configuration["SendGrid:Sender:Name"]);
        }

        public async Task<Response> SendMailAsync(string subject, EmailAddress to, string htmlContent)
        {
            var msg = MailHelper.CreateSingleEmail(sender, to, subject, "", htmlContent);
            var response = await client.SendEmailAsync(msg);

            logger.LogInformation($"Email was sended, on email address: {to.Email}");

            return response;
        }
    }
}
