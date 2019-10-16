using System.Net;
using System.Net.Mail;
using System.Text;

namespace CESP.Core.Utils
{
    public class EmailSender: IEmailSender
    {
        private readonly string _adminEmail;
        private readonly string _adminEmailPassword;
        private readonly string _adminToEmail;
        public EmailSender(string fromEmail, string fromPassword, string toEmail)
        {
            _adminEmail = fromEmail;
            _adminEmailPassword = fromPassword;
            _adminToEmail = toEmail;
        }

        public void SendMail(string subject, string body)
        {
            SendMail(subject, body, _adminToEmail);
        }

        public void SendMail(string subject, string body, string to)
        {
            var result = string.Empty;
            var mailMessage = new MailMessage
            {
                Subject = subject, Body = body
            };
            mailMessage.To.Add(to);
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.SubjectEncoding = Encoding.UTF8;
            mailMessage.From = new MailAddress(_adminEmail);
            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_adminEmail, _adminEmailPassword)
            };

            smtpClient.Send(mailMessage);
        }
    }
}