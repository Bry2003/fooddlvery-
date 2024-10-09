
using System.Net;
using System.Net.Mail;


namespace Smtp

{

    public class EmailService
    {
        private readonly SmtpClient _smtpClient;

        public EmailService()
        {
            _smtpClient = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("d61e501be8c047", "57d85861a5eff0"),
                EnableSsl = true
            };
        }

        public void SendEmail(string toEmail, string subject, string body)
        {
            var mailMessage = new MailMessage("b49194158@gmail.com", toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            _smtpClient.Send(mailMessage);
            Console.WriteLine("Email sent to: " + toEmail);
        }
    }
}