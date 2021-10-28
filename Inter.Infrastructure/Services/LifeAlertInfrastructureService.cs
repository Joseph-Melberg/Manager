using Inter.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using Inter.Infrastructure.Corral;
using Inter.Domain;
using System.Threading.Tasks;
using Inter.Common.Configuration;

namespace Inter.Infrastructure.Services
{
    public class LifeAlertInfrastructureService : ILifeAlertInfrastructureService
    {
        private readonly IEmailConfiguration _emailConfig;
        private readonly IHeartbeatRepository _heartbeatRepository;
        public LifeAlertInfrastructureService(
            IHeartbeatRepository heartbeatRepository,
             IEmailConfiguration emailConfiguration)
        {
            _heartbeatRepository = heartbeatRepository;
            _emailConfig = emailConfiguration;
        }

        public async Task<IList<Heartbeat>> GetStatusesAsync()
        {
            return await _heartbeatRepository.GetStatusesAsync();
        }

        public Task UpdateNode(Heartbeat model)
        {
            return _heartbeatRepository.UpdateAsync(model);
        }

        public void SendMessage(string recipient,string subject, string message)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(_emailConfig.Email);
                mail.To.Add(recipient);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(_emailConfig.Email, _emailConfig.Password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            Console.WriteLine("Done");
        }
    }
}