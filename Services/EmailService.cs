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
                smtpClient.Credentials = new NetworkCredential("moreshet4964@gmail.com", "piyz dbgq bkyc vjks");
                smtpClient.EnableSsl = true;

                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("moreshet4964@gmail.com");
                mailMessage.To.Add("sh214260@gmail.com");
                mailMessage.Subject = "צור קשר-הודעה חדשה";
                mailMessage.Body = $"שם: {name}\nמייל: {email}\nטלפון: {phone}\nהודעה: {message}";

                smtpClient.Send(mailMessage);
                return true;
            }
        }
        public bool SendConfirmOrderToClient(DTO.Order order, DTO.User user)
        {
            using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("moreshet4964@gmail.com", "piyz dbgq bkyc vjks");
                smtpClient.EnableSsl = true;

                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("moreshet4964@gmail.com");
                mailMessage.To.Add(user.Email);
                mailMessage.Subject = $"אישור הזמנה: {order.Id}";
                mailMessage.Body = $"הזמנה מספר: {order.Id} אושרה\nלתאריך: {order.FromDate}-{order.ToDate}\n";

                smtpClient.Send(mailMessage);
                return true;
            }
        }
    }
}
