using Inter.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace Inter.Infrastructure.Services
{
    public class LifeAlertInfrastructureService : ILifeAlertInfrastructureService
    {

        public LifeAlertInfrastructureService()
        {
        }

        public void SendMessage(string recipient,string subject, string message)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("InterEmailService@gmail.com");
                mail.To.Add(recipient);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("InterEmailService@gmail.com", "");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            Console.WriteLine("Done");
        }
    }
}