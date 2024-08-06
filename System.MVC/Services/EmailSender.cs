using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace System.MVC.Services
{
    public class EmailSender : IEmailSender
    {

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var fromMail = "Sender Mail";
            var fromPassword = "Sender Mail password";

            var message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(email);
            message.Body = htmlMessage;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(message);

        }
    }
}
