using EMS.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Insfrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;

        public EmailService(IConfiguration configuration)
        {
            // Fetch sensitive configuration from .env file
            var senderMail = Environment.GetEnvironmentVariable("SMTP_EMAIL");
            var senderPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD");


            // ********* Fetch SMTP Settings from appsettings.json *********
            var emailSettings = configuration.GetSection("SmtpSettings");
            _smtpClient = new SmtpClient
            {
                Host = emailSettings["Server"],
                Port = int.Parse(emailSettings["Port"]),
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential(senderMail, senderPassword),
            };
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            // Get the mail address from .env file
            var senderMail = Environment.GetEnvironmentVariable("SMTP_EMAIL");

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderMail??"giverkhadka13@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(to);

            await _smtpClient.SendMailAsync(mailMessage);
            Console.WriteLine("Email sent successfully");
        }
    }
}
