using Microsoft.Extensions.Options;
using NovaLite.ToDo.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NovaLite.ToDo.Services
{
    public class EmailService
    {
        private readonly IOptions<EmailConfiguration> options;

        public EmailService(IOptions<EmailConfiguration> options)
        {
            this.options = options;
        }

        public void SendEmail(string email, int assignmentId)
        {
            Execute(email, assignmentId).Wait();
        }
        
        private async Task Execute(string email, int assignmentId)
        {
            var client = new SendGridClient(options.Value.ApiKeyEmail);
            var from = new EmailAddress(options.Value.Email, "ToDo");
            var subject = "Reminder from ToDo";
            var to = new EmailAddress(email, "Mila Bjelogrlic");
            var plainTextContent = "You set reminder for this assignment: " + "http://localhost:4200/" + assignmentId;
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
