using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Auth.Infrastructure.Mail;
using Microsoft.Extensions.Options;

namespace Auth.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _config;

        public EmailService(IOptions<SmtpSettings> config)
        {
            _config = config.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using var client = new SmtpClient
            {
                Host = _config.Server,
                Port = _config.Port,
                Credentials = new NetworkCredential(_config.Username, _config.Password),
                EnableSsl = _config.EnableSsl
            };

            var mail = new MailMessage(_config.SenderEmail, toEmail, subject, body)
            {
                IsBodyHtml = true
            };

            await client.SendMailAsync(mail);
        }
    }
}
