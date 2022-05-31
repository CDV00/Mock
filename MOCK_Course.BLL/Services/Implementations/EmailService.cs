using Course.BLL.DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<BaseResponse> SendEmailAsync(string fromAddress, string toAddress, string subjects, string message)
        {
            try
            {
                var mailMessage = new MailMessage(fromAddress, toAddress, subjects, message)
                {
                    IsBodyHtml = true
                };
                using var client = new SmtpClient(_config["SMTP:Host"], int.Parse(_config["SMTP:Port"]))
                {
                    Credentials = new NetworkCredential(_config["SMTP:Sender"], _config["SMTP:Password"])
                };

                client.EnableSsl = true;
                await client.SendMailAsync(mailMessage);
                return new BaseResponse(true);
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message, null);
            }
        }
        
    }
}
