using System;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;

namespace Inter.DomainServices
{
    public class LogListenerService : ILogListenerService
    {
        private readonly ILogListenerInfrastructureService _infrastructureService;
        public LogListenerService(ILogListenerInfrastructureService infrastructureService)
        {
            _infrastructureService = infrastructureService;
        }

        public Task Process(LogMessage message)
        {
            LogModel logModel = new LogModel
            {
                DeviceName = message.DeviceName,
                FormattedMessage = message.FormattedMessage,
                MAC = message.MAC,
                ProcessName = message.ProcessName,
                Severity = message.Severity,
                Timestamp = DateTime.UtcNow,
                Title = message.Title.ToString()
            };
            return _infrastructureService.AddLog(logModel);
        }
    }
}
