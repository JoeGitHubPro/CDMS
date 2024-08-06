using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace System.MVC.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string _senderMail;
        private readonly string _senderMailpassword;
        public EmailSender(string senderMail, string senderMailpassword)
        {
            _senderMail = senderMail;
            _senderMailpassword = senderMailpassword;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            var message = new MailMessage();
            message.From = new MailAddress(_senderMail);
            message.Subject = subject;
            message.To.Add(email);
            message.Body = htmlMessage;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(_senderMail, _senderMailpassword),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(message);

        }
    }
}
