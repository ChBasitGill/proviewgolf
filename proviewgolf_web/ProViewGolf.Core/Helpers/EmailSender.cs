using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace ProViewGolf.Core.Helpers
{
    public class EmailSender
    {
        public static void SendEmail(string name, string email, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("ProViewGolf", "noreply@proviewgolf.com"));
            message.To.Add(new MailboxAddress(name, email));
            message.Subject = subject;
            message.Body = new TextPart(TextFormat.Html) {Text = body};

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("mail.supremecluster.com", 465);
                client.Authenticate("noreply@proviewgolf.com", "Xlf0Du27N!");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}