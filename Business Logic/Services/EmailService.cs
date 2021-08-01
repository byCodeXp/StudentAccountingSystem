using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Threading.Tasks;

namespace Business_Logic.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        private readonly SendGridClient _client;
        private readonly EmailAddress _sender;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;

            var apiKey = _configuration["SendGrid:ApiKey"];
            
            _client = new SendGridClient(apiKey);
            _sender = new EmailAddress("bycodexp@gmail.com", "StudentProgress");
        }

        public async Task<SendGrid.Response> SendMailAsync(string subject, EmailAddress to, string htmlContent)
        {
            var msg = MailHelper.CreateSingleEmail(_sender, to, subject, "", htmlContent);
            return await _client.SendEmailAsync(msg);
        }
    }
}
