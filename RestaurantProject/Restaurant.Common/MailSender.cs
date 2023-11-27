using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Common
{
    public class MailSender
    {
        public static void SendEmail(string email, string subject, string body)
        {
            try
            {
                MailMessage sender = new MailMessage();
                sender.From = new MailAddress("myrestaurant3434@gmail.com", "MyRestaurant");
                sender.Subject = subject;
                sender.Body = body;
                sender.BodyEncoding = Encoding.UTF8;
                sender.To.Add(email);

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"); //gmail ile mail gönderiyoruz
                //smtpClient.Credentials = new NetworkCredential("MyRestaurant34@outlook.com", "*****");
                //smtpClient.Port = 587;
                //smtpClient.Host = "smtp-mail.outlook.com";
                //smtpClient.EnableSsl = true;
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential("myrestaurant3434@gmail.com", "mmdi wcjv zvvv xbjm");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;

                smtpClient.Send(sender);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
