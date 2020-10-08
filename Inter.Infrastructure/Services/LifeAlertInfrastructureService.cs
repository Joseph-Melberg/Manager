using Inter.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using Inter.Infrastructure.Corral;
using Inter.Domain;
using System.Threading.Tasks;

namespace Inter.Infrastructure.Services
{
    public class LifeAlertInfrastructureService : ILifeAlertInfrastructureService
    {
        private readonly IHeartbeatRepository _heartbeatRepository;
        public LifeAlertInfrastructureService(IHeartbeatRepository heartbeatRepository)
        {
            _heartbeatRepository = heartbeatRepository;
        }

        public HeartbeatModel[] GetStatuses()
        {
            return _heartbeatRepository.GetStatuses();
        }

        public Task UpdateNode(HeartbeatModel model)
        {
            return _heartbeatRepository.UpdateAsync(model);
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
                    smtp.Credentials = new NetworkCredential("InterEmailService@gmail.com", "*Quba572nWSt");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            Console.WriteLine("Done");
        }
    }
}