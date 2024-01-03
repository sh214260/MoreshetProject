using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Services.Interfaces;

namespace Services
{
    public class EmailService:IEmailService
    {
        public bool SendContactFormEmail(string name, string email, string phone, string message)
        {
            using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("sh214260@gmail.com", "ozfr jckc fiah acwz");
                smtpClient.EnableSsl = true;

                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("sh214260@gmail.com");
                mailMessage.To.Add("sh214260@gmail.com");
                mailMessage.Subject = "צור קשר-הודעה חדשה";
                mailMessage.Body = $"שם: {name}\nמייל: {email}\nטלפון: {phone}\nהודעה: {message}";

                smtpClient.Send(mailMessage);
                return true;
            }
        }
    }
}
