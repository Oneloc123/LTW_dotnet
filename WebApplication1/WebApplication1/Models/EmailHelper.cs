using System.Net;
using System.Net.Mail;

namespace WebApplication1.Models
{
    public class EmailHelper
    {
        public static void Send(string to, string subject, string body)
        {
            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(
                    "Oneloc123@gmail.com",
                    "otpz fmhw usnm dzum"
                ),
                EnableSsl = true
            };

            var message = new MailMessage(
                "Oneloc123@gmail.com",
                to,
                subject,
                body
            );

            smtp.Send(message);
        }
    }
}
