using EMS.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.UseCases
{

    public class SendEmailUseCase
    {
        private readonly IEmailService _emailService;

        public SendEmailUseCase(IEmailService emailService)
        {
            _emailService = emailService;
        }
        // Main business logic is to send the email to a user, no matter it's implementation
        public async Task Execute(string to, string subject, string body)
        {
            await _emailService.SendEmailAsync(to, subject, body);
        }
    }
}
