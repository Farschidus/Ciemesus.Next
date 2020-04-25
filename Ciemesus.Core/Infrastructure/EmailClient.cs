using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Threading.Tasks;

namespace Ciemesus.Core.Infrastructure
{
    public class EmailClient : IEmailClient
    {
        private readonly EmailClientSettings _emailClientSettings;

        public EmailClient(EmailClientSettings emailClientSettings)
        {
            _emailClientSettings = emailClientSettings;
        }

        public async Task Send(string emailTo, string subject, string body)
        {
            await SendAsync(emailTo, subject, body);
        }

        private async Task SendAsync(string to, string subject, string body)
        {
            var client = new SendGridClient(_emailClientSettings.ApiKey);
            var emailFrom = new EmailAddress(_emailClientSettings.SenderEmail);
            var emailTo = new EmailAddress(to);
            var message = MailHelper.CreateSingleEmail(emailFrom, emailTo, subject, body, body);
            message.AddCategory("Ciemesus");
            message.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(message);

            if (response.StatusCode != HttpStatusCode.Accepted)
            {
                throw new System.Exception(response.StatusCode.ToString());
            }
        }
    }
}
