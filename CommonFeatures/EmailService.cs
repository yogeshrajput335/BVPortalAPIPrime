using BVPortalApi.CommonFeatures.Contracts;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace BVPortalApi.CommonFeatures
{
    public class EmailService : IEmailService
    {

        public EmailService()
        {
        }

        public void Send(string to="", string subject="", string html="", string from = null)
        {
            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("hr@blueversesystems.com"));
            email.To.Add(MailboxAddress.Parse("yogeshrajput335@gmail.com"));
            email.Subject = "USER DATA - Test Email Subject";
            email.Body = new TextPart(TextFormat.Html) { Text = "<h1>User DATA - Example HTML Message Body</h1>" };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("jedidiah.hamill63@ethereal.email", "uq1xRSC6FzCehSU1dA");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}