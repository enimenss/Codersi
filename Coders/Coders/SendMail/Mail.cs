using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Coders.SendMail
{
    public static class Mail
    {
        public static void SendMail(string target,string Mesage)
        {
            using (MailMessage mail = new MailMessage())
            {

                mail.From = new MailAddress("jocaenimen@gmail.com");
                mail.To.Add(target);
                mail.Subject = "WOM";
                mail.Body = Mesage;
                mail.IsBodyHtml = true;
              

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("jocaenimen@gmail.com", "jocaenimen229542");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(mail);
                }
            }

        }

    }
}