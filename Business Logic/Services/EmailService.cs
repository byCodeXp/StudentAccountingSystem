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
        private readonly IConfiguration configuration;
        private readonly SendGridClient client;
        private readonly EmailAddress sender;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            this.logger = logger;
            this.configuration = configuration;

            client = new SendGridClient(this.configuration["SendGrid:ApiKey"]);
            sender = new EmailAddress(this.configuration["SendGrid:Sender:Email"], this.configuration["SendGrid:Sender:Name"]);
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
