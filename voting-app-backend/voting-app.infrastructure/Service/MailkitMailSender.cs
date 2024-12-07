using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using voting_app.core.Contract;
using voting_app.share.Config;

namespace voting_app.infrastructure.Service
{
    public class MailkitMailSender : IMailSender
    {

        private readonly EmailServiceConfig _emailServiceConfig;


        public MailkitMailSender(EmailServiceConfig emailServiceConfig)
        {
            _emailServiceConfig = emailServiceConfig;
        }

        public async Task SendAsync(string receiverEmail, string subject, string body)
        {
            try
            {
                using var smtpClient = new SmtpClient(_emailServiceConfig.SMTPServer, _emailServiceConfig.SMTPPort)
                {
                    Credentials = new NetworkCredential(_emailServiceConfig.SenderEmail, _emailServiceConfig.SenderPassword),
                    EnableSsl = _emailServiceConfig.EnableSsl,
                };

                using var mailMessage = new MailMessage(_emailServiceConfig.SenderEmail, receiverEmail, subject, body) {
                    IsBodyHtml = true,
                };

                await smtpClient.SendMailAsync(mailMessage);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }
}
